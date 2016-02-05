using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticsearchInEOP
{
    class LogPackage
    {
        public LogPackage()
        {
            LogPackages = new List<FeedPackage>();
        }

        public List<FeedPackage> LogPackages { get; set; }

        public void insertValue(string line)
        {
            string[] strs = line.Split('\t');
            if(strs.Length == 32)
            {
                FeedPackage fp = new FeedPackage(strs);
                LogPackages.Add(fp);
            }
        }
    }
}
