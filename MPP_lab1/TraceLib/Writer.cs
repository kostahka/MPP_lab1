using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceLib
{
    public class Writer
    {
        public void toConsole(string text)
        {
            Console.OpenStandardOutput();
            Console.WriteLine(text + "\n");
        }
    }
}
