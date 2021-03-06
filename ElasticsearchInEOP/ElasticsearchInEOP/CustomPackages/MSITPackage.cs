﻿using System;
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
                this.Id = Guid.Parse(strs[0]);
            }
            
            if (!strs[1].Equals(""))
            {
                this.TimeStamp = DateTime.Parse(strs[1]);
            }
            this.TenantId = strs[2];
            this.NetworkMessageId = strs[3];
            this.MessageId = strs[4];
            this.Source = strs[5];
            this.TransportDirectionality = strs[6];
            this.Directionality = strs[7];
            if (!strs[8].Equals(""))
            {
                this.RecipientCount = int.Parse(strs[8]);
            }
            this.Recipient = strs[9];
            this.RecipientDomain = strs[10];
            this.RecipientType = strs[11];
            this.P1Sender = strs[12];
            this.P1Domain = strs[13];
            this.P2Sender = strs[14];
            this.P2Domain = strs[15];


            this.OriginalIP = strs[16];
            this.Slash24Subnet = strs[17];
            this.FilterControl = strs[18];
            this.FilterSubControl = strs[19];
            this.VerdictEDGE = strs[20];
            this.VerdictCF = strs[21];
            this.VerdictMBX = strs[22];
            this.Verdict = strs[23];
            this.VerdictSubType = strs[24];
            this.MessageType = strs[25];
            this.FingerPrintBody = strs[26];
            this.FingerPrintRaw = strs[27];
            this.BodyBin1 = strs[28];
            this.RawBin1 = strs[29];
            this.Category = strs[30];
            this.SubCategory = strs[31];
            this.CustomData = strs[32];
            this.FileName = strs[33];
            this.FileType = strs[34];

            if (!strs[35].Equals(""))
            {
                this.FileVerdict = int.Parse(strs[35]);
            }
            if (!strs[36].Equals(""))
            {
                this.FileSize = int.Parse(strs[36]);
            }

            this.FileHFH = strs[37];
            this.FileFilterSubControl = strs[38];
            this.EventualVerdict = strs[39];
            this.MicrosoftVirusFamily = strs[40];
            this.CyrenVirusFamily = strs[41];
            this.KasperskyVirusFamily = strs[42];
            this.SonarVerdict = strs[43];
            this.CHLVerdict = strs[44];
            this.MalwareInfo = strs[45];
            if (!strs[46].Equals(""))
            {
                this.PartitionId = int.Parse(strs[46]);
            }
        }

        public MSITPackage() { }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
        public Guid Id { get; set; }

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
