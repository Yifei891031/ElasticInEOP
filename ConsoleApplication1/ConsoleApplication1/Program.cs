using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string aa = "\"rfgdgfgdfgfc\"\"dfgdgg\"";
            Console.WriteLine(aa);
            aa = aa.TrimEnd('"').TrimStart('"');
            Console.WriteLine(aa);
            Console.Read();
        }
    }
}
