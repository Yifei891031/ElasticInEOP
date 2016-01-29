using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ElasticsearchInEOP
{
    class Program
    {
        private static ElasticClient Client { get; set; }
        private static LogReader LogReaderInst { get; set; }
        private static string filePath { get; set; }
        private static string indexName {get; set;}
        private static string delFlag { get; set; }

        private static int bulkSize { get; set; }
        private static int shardNum { get; set; }
        static void Main(string[] args)
        {
            if (args.Count() != 7)
            {
                Console.WriteLine("Plese input index name, del flag, fileName, shardNum, bulkSize, host and port, thank you");
                Console.Read();
                return;
            }

            indexName = args[0];
            delFlag = args[1];
            filePath = args[2];
            shardNum = Int32.Parse(args[3]);
            bulkSize = Int32.Parse(args[4]);


            ElasticConnectionClient.host = args[5];
            ElasticConnectionClient.port = Int32.Parse(args[6]);

            //Console.WriteLine(ElasticConnectionClient.host + ":" + ElasticConnectionClient.port);

            Client = ElasticConnectionClient.GetClient();
            //path = "E:/TestData/EventMessageLog.tsv";       
            LogReaderInst = new LogReader(filePath);
            if (delFlag.Equals("t") || delFlag.Equals("T"))//D stands for delete index and create new index
            {
                DeleteIndexIfExists();
                CreateIndex();
            }
            IndexPackages();
            Console.WriteLine("Index Complete");
            Console.ReadKey();
        }
        
        static void CreateIndex()
        {
            Client.CreateIndex(indexName, i => i
                               .NumberOfReplicas(0)
                               .NumberOfShards(shardNum)
                               .Settings(s => s
                                          .Add("merge.policy.merge_factor", "10")
                                          .Add("search.slowlog.threshold.fetch.warn", "1s"))
                               .AddMapping<FeedPackage>(m => m.MapFromAttributes()));
            Console.WriteLine("Create index successfully");
        }

        static void DeleteIndexIfExists()
        {
            if (Client.IndexExists("eventmessage").Exists)
            {
                Client.DeleteIndex("eventmessage");
            }
        }

        static void IndexPackages()
        {
            Console.WriteLine("Start indexing packages");
            Stopwatch sw = new Stopwatch();
            long LineCount = 0;
            sw.Start();
            List<FeedPackage> packages = LogReaderInst.PackagesWrapper(bulkSize);
            while (packages != null && packages.Count >= 1)
            {
                //Index
                LineCount += packages.Count;
                Console.WriteLine("Current total lines: {0}", LineCount);
                var result = Client.IndexMany<FeedPackage>(packages, "eventmessage");
                if (!result.IsValid)
                {
                    foreach(var item in result.ItemsWithErrors)
                    {
                        Console.WriteLine("Failed to index document {0}:{1}", item.Id, item.Error);
                    }
                    Console.WriteLine(result.ConnectionStatus.OriginalException.Message);
                    //Console.Read();
                    Environment.Exit(1);
                }
                packages = LogReaderInst.PackagesWrapper(bulkSize);
            }
            sw.Stop();
            Console.WriteLine("Time for indexing: {0}", sw.Elapsed);

        }
        
    }
    
}
