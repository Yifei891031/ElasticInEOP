using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.IO;

namespace ElasticsearchInEOP
{
    /// <summary>
    /// This class opens the file and read the local log file 
    /// </summary>
    class LogReader
    {
        private StreamReader reader { get; set; }

        private Boolean endflag { get; set; }
        public LogReader(string fileDir)
        {
            reader = new StreamReader(fileDir);
        }

        public List<FeedPackage> PackagesWrapper(int bulkSize)
        {
            LogPackage lp = new LogPackage();

            for (int i = 0; !reader.EndOfStream && i < bulkSize; ++i)
            {
                string line = reader.ReadLine();
                lp.insertValue(line);
            }
            if (reader.EndOfStream)
            {
                this.endflag = true;
            }
            return lp.LogPackages;
        }
    }
}
