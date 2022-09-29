using System;
using System.Collections.Generic;
using System.Text;

namespace TraceLib
{
    public class MethodTraceResult
    {
        public string methodName;
        public string className;
        public long time;
        public readonly List<MethodTraceResult> childMethods = new List<MethodTraceResult>();

        public void SetTime(long time)
        {
            this.time = time;
        }
        public void SetMethodName(string methodName)
        {
            this.methodName = methodName;
        }
        public void SetClassName(string className)
        {
            this.className = className;
        }
        public void AddChildMethod(MethodTraceResult childMethodTraceRes)
        {
            this.childMethods.Add(childMethodTraceRes);
        }
    }
    public class ThreadTraceResult
    {
        public int id;
        public long time = 0;
        public readonly List<MethodTraceResult> methods = new List<MethodTraceResult>();

        public void AddTime(long time)
        {
            this.time += time;
        }
        public void SetId(int id)
        {
            this.id = id;
        }
        public void AddMethod(MethodTraceResult methodTraceRes)
        {
            this.methods.Add(methodTraceRes);
        }
    }
    public class TraceResult
    {
        public Dictionary<int, ThreadTraceResult> threads { get; private set; }
        public TraceResult()
        {
            threads = new Dictionary<int, ThreadTraceResult>();
        }
        public TraceResult(Dictionary<int, ThreadTraceResult> threads)
        {
            this.threads = threads;
        }
    }
}

