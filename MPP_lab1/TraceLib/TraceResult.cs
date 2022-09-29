using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TraceLib
{
    [Serializable]
    public class MethodTraceResult
    {
        [XmlAttribute("name")]
        [JsonProperty("name")]
        public string methodName;
        [XmlAttribute("class")]
        [JsonProperty("class")]
        public string className;
        [XmlAttribute("time")]
        [JsonProperty("time")]
        public string msTime;
        [XmlIgnore]
        [JsonIgnore]
        public long time;
        
        [XmlElement("method")]
        [JsonProperty("methods")]
        public readonly List<MethodTraceResult> childMethods = new List<MethodTraceResult>();

        public void SetTime(long time)
        {
            this.time = time;
            msTime = time + "ms";
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
    [Serializable]
    [XmlRoot("thread")]
    public class ThreadTraceResult
    {
        [XmlAttribute]
        public int id;
        [XmlIgnore]
        [JsonIgnore]
        public long time = 0;
        [XmlAttribute("time")]
        [JsonProperty("time")]
        public string msTime;
        [XmlElement("method")]
        public readonly List<MethodTraceResult> methods = new List<MethodTraceResult>();

        public void AddTime(long time)
        {
            this.time += time;
            msTime = time + "ms";
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
    [Serializable]
    public class TraceResult
    {
        public readonly Dictionary<int, ThreadTraceResult> threads = new Dictionary<int, ThreadTraceResult>();
        public TraceResult(Dictionary<int, ThreadTraceResult> threads)
        {
            this.threads = threads;
        }
        public TraceResult()
        {
            threads = new Dictionary<int, ThreadTraceResult>();
        }
    }
}
