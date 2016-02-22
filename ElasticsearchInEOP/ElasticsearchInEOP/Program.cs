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

            Client = ElasticConnectionClient.GetClient(indexName);
            if (delFlag.Equals("t") || delFlag.Equals("T"))//T stands for delete index and create new index
            {
                DeleteIndexIfExists();
                CreateIndex();
            }
                IndexPackages(filePath, delFlag);

            
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

        static void IndexPackages(string directory, string delFlag)
        {

            string[] files = Directory.GetFiles(directory);
            if(delFlag.Equals("F") || delFlag.Equals("f") || delFlag.Equals("T") || delFlag.Equals("t"))
            {
                Console.WriteLine("Start Indexing...");
            }
            else
            {
                Console.WriteLine("Start Updating...");
            }
            
            for(int i = 0; i < Math.Min(files.Length,threadNum); ++i)
            {
                ReadFile rf = new ReadFile(files[i], Client, bulkSize, indexName, mapping, delFlag);
                Thread rfThread = new Thread(new ThreadStart(rf.index));
                rfThread.Start();

            }
        }
        
    }

    public class ReadFile
    {
        private string fileName;
        private ElasticClient Client;
        private int bulkSize;
        private string indexName;
        private string mapping;
        private string updateFlag;
        public ReadFile(string fileName, ElasticClient Client, int bulkSize, string indexname, string mapping, string updateFlag)
        {
            this.fileName = fileName;
            this.Client = Client;
            this.bulkSize = bulkSize;
            this.indexName = indexname;
            this.mapping = mapping;
            this.updateFlag = updateFlag;
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
                        if(updateFlag.Equals("F") || updateFlag.Equals("f") || updateFlag.Equals("T") || updateFlag.Equals("t")) {//Indexing
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
                        else//Updating
                        {
                            List<IBulkOperation> bulkOpt = new List<IBulkOperation>(bulkSize);

                            for(int i = 0; i < packages.Count; ++i)
                            {

                                MSITPackage p = packages[i];
                                /*
                                Client.Update<MSITPackage, object>(u => u
                                      .IdFrom(p)
                                      .Doc(new {CustomData = p.CustomData, VerdictCF = p.VerdictCF, OriginalIP = p.OriginalIP, Category = p.Category })
                                      .DocAsUpsert()
                                      );
                                */
                                BulkUpdateOperation<MSITPackage, object> bo = new BulkUpdateOperation<MSITPackage, object>(p, new { CustomData = "123435CustomData", VerdictCF = p.VerdictCF, OriginalIP = p.OriginalIP, Category = p.Category }, false);
                                bo.RetriesOnConflict = 3;
                                //BulkUpdateOperation<MSITPackage, object> bo = new BulkUpdateOperation<MSITPackage, object>(p, new { CustomData = p.CustomData, VerdictCF = p.VerdictCF, OriginalIP = p.OriginalIP, Category = p.Category });
                                bulkOpt.Add(bo);
                            }
                            var request = new BulkRequest()
                            {
                                Refresh = true,
                                Consistency = Elasticsearch.Net.Consistency.One,
                                Operations = bulkOpt
                            };
                            //Console.WriteLine("Bulk indexing");
                            var result = Client.Bulk(request);
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
                        
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
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
                        if(updateFlag.Equals("F") || updateFlag.Equals("f") || updateFlag.Equals("T") || updateFlag.Equals("t"))
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
                        else
                        {
                            /*
                            for (int i = 0; i < packages.Count; ++i)
                            {
                                FeedPackage p = packages[i];
                                Client.Update<FeedPackage, object>(u => u
                                      .IdFrom(p)
                                      .Doc(new { sequence_number = p.sequence_number })
                                      .DocAsUpsert()
                                      );
                            }
                            */

                            List<IBulkOperation> bulkOpt = new List<IBulkOperation>(bulkSize);

                            for (int i = 0; i < packages.Count; ++i)
                            {

                                FeedPackage p = packages[i];
                                /*
                                Client.Update<MSITPackage, object>(u => u
                                      .IdFrom(p)
                                      .Doc(new {CustomData = p.CustomData, VerdictCF = p.VerdictCF, OriginalIP = p.OriginalIP, Category = p.Category })
                                      .DocAsUpsert()
                                      );
                                */
                                BulkUpdateOperation<FeedPackage, object> bo = new BulkUpdateOperation<FeedPackage, object>(p, new { sequence_number = p.sequence_number });
                                bulkOpt.Add(bo);
                            }
                            var request = new BulkRequest()
                            {
                                Refresh = true,
                                Consistency = Elasticsearch.Net.Consistency.One,
                                Operations = bulkOpt
                            };
                            var result = Client.Bulk(request);
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
                        
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    packages = LogReaderInst.FeedPackagesWrapper(bulkSize);
                }
            }
            sw.Stop();
            Console.WriteLine("Time for indexing: {0}", sw.Elapsed);
        }
    }
    
}
