using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace TraceLib
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }
    class MethodTracer
    {
        private Stopwatch stopwatch;
        private MethodTraceResult methodTraceResult;
        public MethodTracer()
        {
            this.stopwatch = new Stopwatch();

            methodTraceResult = new MethodTraceResult();

            StackTrace stackTrace = new StackTrace();
            StackFrame methodFrame = stackTrace.GetFrame(3);
            string methodName = methodFrame.GetMethod().Name;
            string className = methodFrame.GetMethod().ReflectedType.Name;

            methodTraceResult.SetClassName(className);
            methodTraceResult.SetMethodName(methodName);
            methodTraceResult.SetTime(0);
        }
        public void StartTrace()
        {
            stopwatch.Start();
        }
        public void StopTrace()
        {
            stopwatch.Stop();
            long time = stopwatch.ElapsedMilliseconds;
            methodTraceResult.SetTime(time);
        }
        public void AddChildMethodTraceRes(MethodTraceResult methodTraceResult)
        {
            this.methodTraceResult.AddChildMethod(methodTraceResult);
        }
        public MethodTraceResult GetMethodTraceResult()
        {
            return methodTraceResult;
        }
    }
    class ThreadTracer
    {
        private ThreadTraceResult threadTraceResult;
        private Stack<MethodTracer> methodTracers = new Stack<MethodTracer>();
        public ThreadTracer(int id)
        {
            threadTraceResult = new ThreadTraceResult();
            threadTraceResult.SetId(id);
        }
        public void StartTrace()
        {
            MethodTracer methodTracer = new MethodTracer();
            methodTracers.Push(methodTracer);
            methodTracer.StartTrace();
        }
        public void StopTrace()
        {
            MethodTracer methodTracer = methodTracers.Pop();
            methodTracer.StopTrace();
            MethodTraceResult methodTraceResult = methodTracer.GetMethodTraceResult();

            if (methodTracers.Count > 0)
            {
                methodTracers.Peek().AddChildMethodTraceRes(methodTraceResult);
            }
            else
            {

                threadTraceResult.AddMethod(methodTraceResult);
                threadTraceResult.AddTime(methodTraceResult.time);
            }
        }
        public ThreadTraceResult GetThreadTraceResult()
        {
            return threadTraceResult;
        }
    }
    public class Tracer : ITracer
    {
        private Dictionary<int, ThreadTracer> threadTracers = new Dictionary<int, ThreadTracer>();
        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            ThreadTracer threadTracer;
            if (threadTracers.ContainsKey(threadId))
            {
                threadTracer = threadTracers[threadId];
            }
            else
            {
                threadTracer = new ThreadTracer(threadId);
                threadTracers[threadId] = threadTracer;
            }

            threadTracer.StartTrace();
        }
        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            if (threadTracers.ContainsKey(threadId))
            {
                threadTracers[threadId].StopTrace();
            }
        }
        public TraceResult GetTraceResult()
        {
            var results = new Dictionary<int, ThreadTraceResult>();

            foreach (var value in threadTracers.Values)
            {
                ThreadTraceResult threadResult = value.GetThreadTraceResult();
                results.Add(threadResult.id, threadResult);
            }

            var result = new TraceResult(results);

            return result;
        }
    }
}

