using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chapter17
{
    public class Serializer
    {
        public static void Invoker()
        {
            {
                B();
            }



        }
        static void A()
        {
            Person person = new Person();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            var ds = new DataContractSerializer(typeof(Person));
            using (var s = File.Create(basePath + "person.xml"))
            {
                ds.WriteObject(s, person);
            }
        }
        static void B()
        {
            Person person = new Person() {  Age=1, Name="1121"};
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            var ds = new DataContractSerializer(typeof(Person));
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
            using (XmlWriter writer = XmlWriter.Create(basePath + "person3.xml", settings))
            {
                ds.WriteObject(writer, person);
            }
            //Process.Start(basePath + "person.xml");
        }
    }
    [DataContract( Name ="PersonTest")]
    public class Person
    {
       [DataMember(Name="AgeX")] public int Age { get; set; }
       [DataMember(Name= "Name")]  public string Name { get; set; }
    }
}
