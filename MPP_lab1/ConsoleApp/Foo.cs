using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TraceLib;

namespace ConsoleApp
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        public Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();

            _bar.InnerMethod();

            _tracer.StopTrace();
        }

        public void AnotherMyMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(50);
            _bar.InnerMethod();

            _tracer.StopTrace();
        }
    }
}
