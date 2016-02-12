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
        static void Main(string[] args)
        {

            if (args.Count() != 8)
            {
                Console.WriteLine("Plese input index name, del flag, fileName, shardNum, bulkSize, thread number,  host and port, thank you");
                Console.Read();
                return;
            }
            /*
            indexName = "test";
            delFlag = "T";
            filePath = "E:\\testsplit";
            shardNum = 6;
            bulkSize = 2500;
            threadNum = 1;
            ElasticConnectionClient.host = "10.121.130.110";
            ElasticConnectionClient.port = 9200;
            */

            
            indexName = args[0];
            delFlag = args[1];
            filePath = args[2];
            shardNum = Int32.Parse(args[3]);
            bulkSize = Int32.Parse(args[4]);
            threadNum = Int32.Parse(args[5]);

            ElasticConnectionClient.host = args[6];
            ElasticConnectionClient.port = Int32.Parse(args[7]);
            

            Client = ElasticConnectionClient.GetClient();
            if (delFlag.Equals("t") || delFlag.Equals("T"))//T stands for delete index and create new index
            {
                DeleteIndexIfExists();
                CreateIndex();
            }
            IndexPackages(filePath);
            Console.WriteLine("Index Complete");
            Console.ReadKey();
        }
        
        static void CreateIndex()
        {
            
            Client.CreateIndex(indexName, i => i
                               .NumberOfReplicas(0)
                               .NumberOfShards(shardNum)
                               .Settings(s => s.Add("search.slowlog.threshold.fetch.warn", "1s"))
                               .AddMapping<MSITPackage>(m => m.MapFromAttributes()));
                               
                               /*
           Client.CreateIndex(indexName, i => i
           .NumberOfReplicas(0)
           .NumberOfShards(shardNum)
           .Settings(s => s.Add("search.slowlog.threshold.fetch.warn", "1s")));
           */

            Console.WriteLine("Create index successfully");
        }

        static void DeleteIndexIfExists()
        {
            if (Client.IndexExists(indexName).Exists)
            {
                Client.DeleteIndex(indexName);
            }

        }

        static void IndexPackages(string directory)
        {

            string[] files = Directory.GetFiles(directory);
            Console.WriteLine("Start Indexing...");
            for(int i = 0; i < Math.Min(files.Length,threadNum); ++i)
            {
                ReadFile rf = new ReadFile(files[i], Client, bulkSize, indexName);
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
        public ReadFile(string fileName, ElasticClient Client, int bulkSize, string indexname)
        {
            this.fileName = fileName;
            this.Client = Client;
            this.bulkSize = bulkSize;
            this.indexName = indexname;
        }

        public void index()
        {
            LogReader LogReaderInst = new LogReader(fileName);
            Stopwatch sw = new Stopwatch();
            long LineCount = 0;
            sw.Start();
            List<Package> packages = LogReaderInst.PackagesWrapper(bulkSize);
            while (packages != null && packages.Count >= 1)
            {
                //Index
                LineCount += packages.Count;
                Console.WriteLine("Current total lines: {0}", LineCount);
                try { var  result = Client.IndexMany<Package>(packages, indexName);

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
                catch {
                
                }
                packages = LogReaderInst.PackagesWrapper(bulkSize);
            }
            sw.Stop();
            Console.WriteLine("Time for indexing: {0}", sw.Elapsed);
        }
    }
    
}
