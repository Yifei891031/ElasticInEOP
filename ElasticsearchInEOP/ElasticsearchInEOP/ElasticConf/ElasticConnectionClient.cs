using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ElasticsearchInEOP
{
    class ElasticConnectionClient
    {
        private static  ConnectionSettings _connectionSettings;
        public static string host { get; set; }
        public static int port { get; set; }

        public static string IndexAlia { get { return "mailflow"; } }

       

        public static Uri CreateUri()
        {
            Console.WriteLine("http://" + host + ":" + port);
            
            return new Uri("http://" + host + ":" + port);
        }

        /*
        static ElasticConnectionClient()
        {
            _connectionSettings = new ConnectionSettings(CreateUri()).SetDefaultIndex("MessageEvent");
        }
        */

        public static ElasticClient GetClient(string indexName)
        {
            _connectionSettings = new ConnectionSettings(CreateUri()).SetDefaultIndex(indexName);
            return new ElasticClient(_connectionSettings);
        }

        public static string CreateIndexName()
        {
            return string.Format("{0}-{1:dd-MM-yyyy-HH-mm-ss}", IndexAlia, DateTime.UtcNow);
        }

    }
}
