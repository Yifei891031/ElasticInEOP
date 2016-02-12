using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using System.Net;

namespace ElasticsearchInEOP
{
    class MSITPackage : Package
    {
        public void wrap(string[] strs)
        {
            if (!strs[0].Equals(""))
            {
                this.TimeStamp = DateTime.Parse(strs[0]);
            }
            this.TenantId = strs[1];
            this.NetworkMessageId = strs[2];
            this.MessageId = strs[3];
            this.Source = strs[4];
            this.TransportDirectionality = strs[5];
            this.Directionality = strs[6];
            if (!strs[7].Equals(""))
            {
                this.RecipientCount = int.Parse(strs[7]);
            }
            this.Recipient = strs[8];
            this.RecipientDomain = strs[9];
            this.RecipientType = strs[10];
            this.P1Sender = strs[11];
            this.P1Domain = strs[12];
            this.P2Sender = strs[13];
            this.P2Domain = strs[14];


            this.OriginalIP = strs[15];
            this.Slash24Subnet = strs[16];
            this.FilterControl = strs[17];
            this.FilterSubControl = strs[18];
            this.VerdictEDGE = strs[19];
            this.VerdictCF = strs[20];
            this.VerdictMBX = strs[21];
            this.Verdict = strs[22];
            this.VerdictSubType = strs[23];
            this.MessageType = strs[24];
            this.FingerPrintBody = strs[25];
            this.FingerPrintRaw = strs[26];
            this.BodyBin1 = strs[27];
            this.RawBin1 = strs[28];
            this.Category = strs[29];
            this.SubCategory = strs[30];
            this.CustomData = strs[31];
            this.FileName = strs[32];
            this.FileType = strs[33];

            if (!strs[34].Equals(""))
            {
                this.FileVerdict = int.Parse(strs[34]);
            }
            if (!strs[35].Equals(""))
            {
                this.FileSize = int.Parse(strs[35]);
            }

            this.FileHFH = strs[36];
            this.FileFilterSubControl = strs[37];
            this.EventualVerdict = strs[38];
            this.MicrosoftVirusFamily = strs[39];
            this.CyrenVirusFamily = strs[40];
            this.KasperskyVirusFamily = strs[41];
            this.SonarVerdict = strs[42];
            this.CHLVerdict = strs[43];
            this.MalwareInfo = strs[44];
            if (!strs[45].Equals(""))
            {
                this.PartitionId = int.Parse(strs[45]);
            }
        }
        public MSITPackage(string[] strs)
        {
            if (!strs[0].Equals(""))
            {
                this.TimeStamp = DateTime.Parse(strs[0]);
            }
            this.TenantId = strs[1];
            this.NetworkMessageId = strs[2];
            this.MessageId = strs[3];
            this.Source = strs[4];
            this.TransportDirectionality = strs[5];
            this.Directionality = strs[6];
            if (!strs[7].Equals(""))
            {
                this.RecipientCount = int.Parse(strs[7]);
            }
            this.Recipient = strs[8];
            this.RecipientDomain = strs[9];
            this.RecipientType = strs[10];
            this.P1Sender = strs[11];
            this.P1Domain = strs[12];
            this.P2Sender = strs[13];
            this.P2Domain = strs[14];
            
            this.OriginalIP = strs[15];
            this.Slash24Subnet = strs[16];
            this.FilterControl = strs[17];
            this.FilterSubControl = strs[18];
            this.VerdictEDGE = strs[19];
            this.VerdictCF = strs[20];
            this.VerdictMBX = strs[21];
            this.Verdict = strs[22];
            this.VerdictSubType = strs[23];
            this.MessageType = strs[24];
            this.FingerPrintBody = strs[25];
            this.FingerPrintRaw = strs[26];
            this.BodyBin1 = strs[27];
            this.RawBin1 = strs[28];
            this.Category = strs[29];
            this.SubCategory = strs[30];
            this.CustomData = strs[31];
            this.FileName = strs[32];
            this.FileType = strs[33];
            
            if (!strs[34].Equals(""))
            {
                this.FileVerdict = int.Parse(strs[34]);
            }
            if (!strs[35].Equals(""))
            {
                this.FileSize = int.Parse(strs[35]);
            }

            this.FileHFH = strs[36];
            this.FileFilterSubControl = strs[37];
            this.EventualVerdict = strs[38];
            this.MicrosoftVirusFamily = strs[39];
            this.CyrenVirusFamily = strs[40];
            this.KasperskyVirusFamily = strs[41];
            this.SonarVerdict = strs[42];
            this.CHLVerdict = strs[43];
            this.MalwareInfo = strs[44];
            if (!strs[45].Equals(""))
            {
                this.PartitionId = int.Parse(strs[45]);
            }
        }

        public MSITPackage() { }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public DateTime TimeStamp { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string TenantId { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string NetworkMessageId { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string MessageId { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string Source { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string TransportDirectionality { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string Directionality { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public int RecipientCount { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string Recipient { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string RecipientDomain { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string RecipientType { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string P1Sender { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string P1Domain { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string P2Sender { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string P2Domain { get; set; }

        
        /*
        [ElasticProperty(Type = FieldType.Ip)]
        public string OriginalIP { get; set; }
        */

        
        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string OriginalIP { get; set; }
        
        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string Slash24Subnet { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string FilterControl { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string FilterSubControl { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string VerdictEDGE { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string VerdictCF { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string VerdictMBX { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string Verdict { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string VerdictSubType { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string MessageType { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string FingerPrintBody { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string FingerPrintRaw { get; set; }
        
        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string BodyBin1 { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string RawBin1 { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string Category { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string SubCategory { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string CustomData { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string FileName { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string FileType { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public int? FileVerdict { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public int? FileSize { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string FileHFH { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string FileFilterSubControl { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string EventualVerdict { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string MicrosoftVirusFamily { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string CyrenVirusFamily { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string KasperskyVirusFamily { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string SonarVerdict { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string CHLVerdict { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public string MalwareInfo { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public int PartitionId { get; set; }
    }
}
