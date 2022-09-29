using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TraceLib;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracer = new Tracer();
            var foo = new Foo(tracer);

            var thread = new Thread(foo.MyMethod);
            thread.Start();
            thread.Join();

            thread = new Thread(foo.AnotherMyMethod);
            thread.Start();
            foo.AnotherMyMethod();
            thread.Join();

            var res = tracer.GetTraceResult();

            var writer = new Writer();
            var serialize = new Serializers();

            string outRes = serialize.toXML(res);
            writer.toConsole(outRes);

            Console.ReadLine();
        }
    }
}
