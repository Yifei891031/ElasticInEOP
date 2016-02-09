using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticsearchInEOP
{
    class MSITPackage
    {
        public MSITPackage(string[] strs)
        {

        }

        public MSITPackage() { }
        public DateTime TimeStamp { get; set; }
        public string Forest { get; set; }
        public string MachineName { get; set; }
        public string TenantIdP { get; set; }
        public string NetworkMessageId { get; set; }
        public int? MEssageVerdict { get; set; }
        public string Source { get; set; }
        public string TransportDirectionality { get; set; }
        public string FilterControl { get; set; }
        public string FilterSubControl { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int? FileSize { get; set; }
        public string FileHFH { get; set; }
        public int? FileVerdict { get; set; }
        public string FileFilterSubControl { get; set; }
        public string FileScannedBy { get; set; }
        public string MessageType { get; set; }
        public string CustomProperties { get; set; }
    }
}
