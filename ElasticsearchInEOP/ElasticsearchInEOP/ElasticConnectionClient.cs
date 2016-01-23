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
        private static readonly ConnectionSettings _connectionSettings;

        public static string IndexAlia { get { return "MessageEvent"; } }


        public static Uri CreateUri(int port)
        {
            var host = "localhost";
            return new Uri("http://" + host + ":" + port);
        }

        static ElasticConnectionClient()
        {
            _connectionSettings = new ConnectionSettings(CreateUri(9200)).SetDefaultIndex("MessageEvent");
        }

        public static ElasticClient GetClient()
        {
            return new ElasticClient(_connectionSettings);
        }

        public static string CreateIndexName()
        {
            return string.Format("{0}-{1:dd-MM-yyyy-HH-mm-ss}", IndexAlia, DateTime.UtcNow);
        }

    }
}
