using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TraceLib;
using ConsoleApp;
using System.Threading;

namespace TraceLibTests
{
    [TestClass]
    public class TracerTests
    {
        private Tracer tracer = new Tracer();
        private Foo foo;
        private int id;

        [TestInitialize]
        public void Initialize()
        {
            tracer = new Tracer();
            foo = new Foo(tracer);
            id = Thread.CurrentThread.ManagedThreadId;
        }

        [TestMethod]
        public void OneThread()
        {
            foo.MyMethod();
            var result = tracer.GetTraceResult();
            Assert.AreEqual(result.threads.Count, 1);
        }

        [TestMethod]
        public void ThreadID()
        {
            Thread secondThread = new Thread(foo.MyMethod);
            secondThread.Start();
            secondThread.Join();
            foo.MyMethod();

            var result = tracer.GetTraceResult();
            Assert.AreEqual(id, result.threads[id].id);
        }

        [TestMethod]
        public void TwoThread()
        {
            Thread secondThread = new Thread(foo.MyMethod);
            secondThread.Start();
            secondThread.Join();
            foo.MyMethod();

            var result = tracer.GetTraceResult();
            Assert.AreEqual(result.threads.Count, 2);
        }

        [TestMethod]
        public void MethodName()
        {
            foo.MyMethod();

            var result = tracer.GetTraceResult();
            Assert.AreEqual(nameof(foo.MyMethod), result.threads[id].methods[0].methodName);
            Assert.AreEqual(nameof(Foo), result.threads[id].methods[0].className);
        }
    }
}
