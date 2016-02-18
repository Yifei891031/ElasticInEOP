using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ElasticsearchInEOP
{
    class Program
    {
        private static ElasticClient Client { get; set; }
        //private static LogReader LogReaderInst { get; set; }
        private static string filePath { get; set; }
        private static string indexName {get; set;}
        private static string delFlag { get; set; }

        private static int bulkSize { get; set; }
        private static int shardNum { get; set; }
        private static int threadNum { get; set; }
        private static string mapping { get; set; }
        static void Main(string[] args)
        {

            if (args.Count() != 9)
            {
                Console.WriteLine("Plese input index name, del flag, fileName, shardNum, bulkSize, thread number,  host, port and package type, thank you");
                Console.Read();
                return;
            }
            
            indexName = args[0];
            delFlag = args[1];
            filePath = args[2];
            shardNum = Int32.Parse(args[3]);
            bulkSize = Int32.Parse(args[4]);
            threadNum = Int32.Parse(args[5]);

            ElasticConnectionClient.host = args[6];
            ElasticConnectionClient.port = Int32.Parse(args[7]);
            mapping = args[8];

            Client = ElasticConnectionClient.GetClient();
            if (delFlag.Equals("t") || delFlag.Equals("T"))//T stands for delete index and create new index
            {
                DeleteIndexIfExists();
                CreateIndex();
            }
            IndexPackages(filePath);
            Console.ReadKey();
        }
        
        static void CreateIndex()
        {
            switch (mapping)
            {
                case "msit":
                    Client.CreateIndex(indexName, i => i
                               .NumberOfReplicas(0)
                               .NumberOfShards(shardNum)
                               .Settings(s => s.Add("search.slowlog.threshold.fetch.warn", "1s"))
                               .AddMapping<MSITPackage>(m => m.MapFromAttributes()));
                    break;
                case "feed":
                    Client.CreateIndex(indexName, i => i
                               .NumberOfReplicas(0)
                               .NumberOfShards(shardNum)
                               .Settings(s => s.Add("search.slowlog.threshold.fetch.warn", "1s"))
                               .AddMapping<FeedPackage>(m => m.MapFromAttributes()));
                    break;
                default:
                    Client.CreateIndex(indexName, i => i
                               .NumberOfReplicas(0)
                               .NumberOfShards(shardNum)
                               .Settings(s => s.Add("search.slowlog.threshold.fetch.warn", "1s"))
                               .AddMapping<Package>(m => m.MapFromAttributes()));
                    break;
            }
            Console.WriteLine("Create index successfully");
        }

        static void DeleteIndexIfExists()
        {
            Console.WriteLine("Deleting index: " + indexName);
            if (Client.IndexExists(indexName).Exists)
            {
                Client.DeleteIndex(indexName);
            }
            Console.WriteLine("Deleting index " + indexName + " successfully");

        }

        static void IndexPackages(string directory)
        {

            string[] files = Directory.GetFiles(directory);
            Console.WriteLine("Start Indexing...");
            for(int i = 0; i < Math.Min(files.Length,threadNum); ++i)
            {
                ReadFile rf = new ReadFile(files[i], Client, bulkSize, indexName, mapping);
                Thread rfThread = new Thread(new ThreadStart(rf.index));
                rfThread.Start();

            }
            Console.WriteLine("Indexing finished");
            //Console.Read();

        }
        
    }

    public class ReadFile
    {
        private string fileName;
        private ElasticClient Client;
        private int bulkSize;
        private string indexName;
        private string mapping;
        public ReadFile(string fileName, ElasticClient Client, int bulkSize, string indexname, string mapping )
        {
            this.fileName = fileName;
            this.Client = Client;
            this.bulkSize = bulkSize;
            this.indexName = indexname;
            this.mapping = mapping;
        }

        public void index()
        {
            LogReader LogReaderInst = new LogReader(fileName);
            Stopwatch sw = new Stopwatch();
            long LineCount = 0;
            sw.Start();


            if (mapping.Equals("msit"))
            {
                List<MSITPackage> packages = LogReaderInst.MSITPackagesWrapper(bulkSize);
                while (packages != null && packages.Count >= 1)
                {
                    //Index
                    LineCount += packages.Count;
                    Console.WriteLine("Current total lines: {0}", LineCount);
                    try
                    {
                        var result = Client.IndexMany<MSITPackage>(packages, indexName);
                        if (!result.IsValid)
                        {

                            foreach (var item in result.ItemsWithErrors)
                            {
                                Console.WriteLine("Failed to index document {0}:{1}", item.Id, item.Error);
                            }
                            Console.WriteLine(result.ConnectionStatus.OriginalException.Message);
                            //Console.Read();
                            Environment.Exit(1);
                        }
                    }
                    catch
                    {

                    }
                    packages = LogReaderInst.MSITPackagesWrapper(bulkSize);
                }
            }
            else if (mapping.Equals("feed"))
            {
                List<FeedPackage> packages = LogReaderInst.FeedPackagesWrapper(bulkSize);
                while (packages != null && packages.Count >= 1)
                {
                    //Index
                    LineCount += packages.Count;
                    Console.WriteLine("Current total lines: {0}", LineCount);
                    try
                    {
                        var result = Client.IndexMany<FeedPackage>(packages, indexName);
                        if (!result.IsValid)
                        {

                            foreach (var item in result.ItemsWithErrors)
                            {
                                Console.WriteLine("Failed to index document {0}:{1}", item.Id, item.Error);
                            }
                            Console.WriteLine(result.ConnectionStatus.OriginalException.Message);
                            //Console.Read();
                            Environment.Exit(1);
                        }
                    }
                    catch
                    {

                    }
                    packages = LogReaderInst.FeedPackagesWrapper(bulkSize);
                }
            }
            sw.Stop();
            Console.WriteLine("Time for indexing: {0}", sw.Elapsed);
        }
    }
    
}
