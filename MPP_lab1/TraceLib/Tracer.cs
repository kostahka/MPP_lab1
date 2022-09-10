using System;
using System.Collections.Generic;
using System.Text;

namespace TraceLib
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }
    public class Tracer:ITracer
    {
        public void StartTrace()
        {

        }
        public void StopTrace()
        {

        }
        public TraceResult GetTraceResult()
        {
            return null;
        }
    }
}
