using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;

namespace Everything
{
    class Program
    {
        static void Main(string[] args)
        {

            //string jsonText = @"{""input"" : ""value"", ""output"" : ""result""}";
            //JsonReader reader = new JsonTextReader(new StringReader(jsonText));
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            //}

            //StringWriter sw = new StringWriter();
            //JsonWriter writer = new JsonTextWriter(sw);
            //writer.WriteStartObject();
            //writer.WritePropertyName("input");
            //writer.WriteValue("value");
            //writer.WritePropertyName("output");
            //writer.WriteValue("result");
            //writer.WriteEndObject();
            //writer.Flush();
            //string jsonText = sw.GetStringBuilder().ToString();
            //Console.WriteLine(jsonText);

            //JObject jo = JObject.Parse(jsonText);
            //string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();


            //Project p = new Project() { Input = "stone", Output = "gold" };
            //JsonSerializer serializer = new JsonSerializer();
            //StringWriter sw = new StringWriter();
            //serializer.Serialize(new JsonTextWriter(sw), p);
            //Console.WriteLine(sw.GetStringBuilder().ToString());
            //StringReader sr = new StringReader(@"{""Input"":""stone"", ""Output"":""gold""}");
            //Project p1 = (Project)serializer.Deserialize(new JsonTextReader(sr), typeof(Project));
            //Console.WriteLine(p1.Input + "=>" + p1.Output);

            //Project p = new Project() { Input = "stone", Output = "gold" };
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //var json = serializer.Serialize(p);
            //Console.WriteLine(json);
            //var p1 = serializer.Deserialize<Project>(json);
            //Console.WriteLine(p1.Input + "=>" + p1.Output);
            //Console.WriteLine(ReferenceEquals(p,p1));


            Project p = new Project() { Input = "stone", Output = "gold" };
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(p.GetType());
            string jsonText;

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, p);
                jsonText = Encoding.UTF8.GetString(stream.ToArray());
                Console.WriteLine(jsonText);
            }

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonText)))
            {
                DataContractJsonSerializer serializer1 = new DataContractJsonSerializer(typeof(Project));
                Project p1 = (Project)serializer1.ReadObject(ms);
                Console.WriteLine(p1.Input + "=>" + p1.Output);
            }
            Console.ReadLine();

        }









    public class Project
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }
}
