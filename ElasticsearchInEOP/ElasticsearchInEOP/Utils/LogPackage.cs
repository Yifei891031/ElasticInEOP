using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticsearchInEOP;

namespace ElasticsearchInEOP
{
    class LogPackage
    {
        public LogPackage()
        {
            MSITLogPackages = new List<MSITPackage>();
            FeedLogPackages = new List<FeedPackage>();
        }

        //public List<FeedPackage> LogPackages { get; set; }
        public List<MSITPackage> MSITLogPackages { get; set; }
        public List<FeedPackage> FeedLogPackages { get; set; }

        public void insertValue(string line)
        {
            string[] strs = line.Split('\t');
            if(strs.Length == 47)
            {
                MSITPackage package = new MSITPackage();
                package.wrap(strs);
                MSITLogPackages.Add(package);
            }else if(strs.Length == 32)
            {
                FeedPackage package = new FeedPackage();
                package.wrap(strs);
                FeedLogPackages.Add(package);
            }
        }
    }
}
