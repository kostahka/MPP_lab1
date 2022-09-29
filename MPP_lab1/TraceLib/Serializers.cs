using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace TraceLib
{
    public class Serializers
    {
        [XmlRoot("root")]
        [XmlType("root")]
        public class Root
        {
            [XmlElement(ElementName = "thread")]
            [JsonProperty("threads")]
            public List<ThreadTraceResult> res = new List<ThreadTraceResult>();
            public Root() { }
            public Root(TraceResult traceResult)
            {
                foreach (int key in traceResult.threads.Keys)
                {
                    res.Add(traceResult.threads[key]);
                }
            }
        }

        public string toXML(TraceResult result)
        {
            var root = new Root(result);

            XmlSerializer formatter = new XmlSerializer(typeof(Root));
            var stringWriter = new StringWriter();
            formatter.Serialize(stringWriter, root);

            return stringWriter.ToString();
        }

        public string toJSON(TraceResult result)
        {
            var root = new Root(result);

            string json = JsonConvert.SerializeObject(root, Formatting.Indented);
            return json;       
        }
    }
}
