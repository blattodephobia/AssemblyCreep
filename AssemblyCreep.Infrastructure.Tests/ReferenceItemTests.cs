using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using AssemblyCreep;
using System.IO;
using System.Text;

namespace AssemblyCreep.Infrastructure.Tests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void DeserializesCorrectly()
        {
            string xml = @"<Reference Include=""System.Core""/>";
            XmlSerializer s = new XmlSerializer(typeof(ReferenceItem));
            ReferenceItem item = s.Deserialize(new MemoryStream(Encoding.ASCII.GetBytes(xml))) as ReferenceItem;
            Assert.AreEqual("System.Core", item.Assembly.FullName);
        }
    }
}
