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
            LogPackages = new List<Package>();
        }

        public List<Package> LogPackages { get; set; }

        public void insertValue(string line)
        {
            string[] strs = line.Split('\t');
            if(strs.Length == 46)
            {
                Package package = new MSITPackage();
                package.wrap(strs);
                LogPackages.Add(package);
            }else if(strs.Length == 32)
            {
                Package package = new FeedPackage();
                package.wrap(strs);
                LogPackages.Add(package);
            }
        }
    }
}
