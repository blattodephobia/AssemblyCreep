using AssemblyCreep.Models;
using AssemblyCreep.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AssemblyCreepProjectMergeTests
{
    [TestClass]
    public class LoadXmlTests
    {
        [TestMethod]
        public void EmitsErrorOnInvalidXml()
        {
            MergerViewModel vm = new MergerViewModel();
            var ms = new MemoryStream();
            ms.Write(Encoding.ASCII.GetBytes("lololo"), 0, 6);
            ms.Seek(0, SeekOrigin.Begin);
            SelectedItemViewModel<XmlDocument> sim = vm.GetXmlFileModel(ms);
            Assert.IsNull(sim.Item);
            Assert.IsNotNull(sim.Description);
        }

        [TestMethod]
        public void LoadsValidXmlCorrectly()
        {
            MergerViewModel vm = new MergerViewModel();
            using (MemoryStream ms = new MemoryStream())
            {
                var xml = XmlWriter.Create(ms);
                xml.WriteStartElement("root");
                    xml.WriteStartElement("child1");
                        xml.WriteStartElement("child2");
                        xml.WriteEndElement();
                        xml.WriteStartElement("child2");
                        xml.WriteEndElement();
                    xml.WriteEndElement();
                    xml.WriteStartElement("child1");
                    xml.WriteEndElement();
                xml.WriteEndElement();
                xml.Flush();
                ms.Position = 0;
                SelectedItemViewModel<XmlDocument> sim = vm.GetXmlFileModel(ms);
                Assert.AreEqual("root", sim.Item.DocumentElement.Name);
            }
        }
    }
}
