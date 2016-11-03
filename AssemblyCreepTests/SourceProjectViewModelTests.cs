using AssemblyCreep.Models;
using AssemblyCreep.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Events;
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
            SourceProjectsViewModel vm = new SourceProjectsViewModel(TestUtils.GetMockAggregator().Object);
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
            SourceProjectsViewModel vm = new SourceProjectsViewModel(TestUtils.GetMockAggregator().Object);
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

    [TestClass]
    public class FileSelectionTests
    {
        [TestMethod]
        public void RaisesEventOnFileUnload()
        {
            int invocationsCount = 0;
            Action<FileLoadEventArgs> callback = (FileLoadEventArgs args) =>
            {
                if (args.UnloadedFiles?.Any() ?? false)
                {
                    invocationsCount++;
                }
            };
            var @event = TestUtils.GetMockEvent<FileLoadEvent, FileLoadEventArgs>(callback);
            var aggregator = TestUtils.GetMockAggregator();
            aggregator.Setup(a => a.GetEvent<FileLoadEvent>()).Returns(@event.Object);
            SourceProjectsViewModel vm = new SourceProjectsViewModel(aggregator.Object);
            vm.ProjectFiles.Add(new CsProjFile(new XmlDocument()));
            vm.ProjectFiles.RemoveAt(0);
            Assert.AreEqual(1, invocationsCount);
        }

        [TestMethod]
        public void RaisesEventOnFileLoad()
        {
            int invocationsCount = 0;
            Action<FileLoadEventArgs> callback = (FileLoadEventArgs args) =>
            {
                if (args.LoadedFiles?.Any() ?? false)
                {
                    invocationsCount++;
                }
            };
            var @event = TestUtils.GetMockEvent<FileLoadEvent, FileLoadEventArgs>(callback);
            var aggregator = TestUtils.GetMockAggregator();
            aggregator.Setup(a => a.GetEvent<FileLoadEvent>()).Returns(@event.Object);
            SourceProjectsViewModel vm = new SourceProjectsViewModel(aggregator.Object);
            vm.ProjectFiles.Add(new CsProjFile(new XmlDocument()));
            Assert.AreEqual(1, invocationsCount);
        }

        [TestMethod]
        public void RaisesEventOnFileUnselected()
        {
            int invocationsCount = 0;
            Action<FileLoadEventArgs> callback = (FileLoadEventArgs args) =>
            {
                if (args.UnloadedFiles?.Any() ?? false)
                {
                    invocationsCount++;
                }
            };
            var @event = TestUtils.GetMockEvent<FileLoadEvent, FileLoadEventArgs>(callback);
            var aggregator = TestUtils.GetMockAggregator();
            aggregator.Setup(a => a.GetEvent<FileLoadEvent>()).Returns(@event.Object);
            SourceProjectsViewModel vm = new SourceProjectsViewModel(aggregator.Object);
            vm.ProjectFiles.Add(new CsProjFile(new XmlDocument()) { IsSelected = true });
            
            vm.ProjectFiles[0].IsSelected = false;
            Assert.AreEqual(1, invocationsCount);
        }

        [TestMethod]
        public void RaisesEventOnFileSelected()
        {
            int invocationsCount = 0;
            Action<FileLoadEventArgs> callback = (FileLoadEventArgs args) =>
            {
                if (args.LoadedFiles?.Any() ?? false)
                {
                    invocationsCount++;
                }
            };
            var @event = TestUtils.GetMockEvent<FileLoadEvent, FileLoadEventArgs>(callback);
            var aggregator = TestUtils.GetMockAggregator();
            aggregator.Setup(a => a.GetEvent<FileLoadEvent>()).Returns(@event.Object);
            SourceProjectsViewModel vm = new SourceProjectsViewModel(aggregator.Object);
            vm.ProjectFiles.Add(new CsProjFile(new XmlDocument()) { IsSelected = false });

            Assert.AreEqual(1, invocationsCount);
            vm.ProjectFiles[0].IsSelected = true;
            Assert.AreEqual(2, invocationsCount);
        }

        [TestMethod]
        public void DoesntRaiseEventOnUnloadedFileModified()
        {
            int invocationsCount = 0;
            Action<FileLoadEventArgs> callback = (FileLoadEventArgs args) =>
            {
                if (args.UnloadedFiles?.Any() ?? false)
                {
                    invocationsCount++;
                }
            };
            var @event = TestUtils.GetMockEvent<FileLoadEvent, FileLoadEventArgs>(callback);
            var aggregator = TestUtils.GetMockAggregator();
            aggregator.Setup(a => a.GetEvent<FileLoadEvent>()).Returns(@event.Object);
            SourceProjectsViewModel vm = new SourceProjectsViewModel(aggregator.Object);
            var projFile = new CsProjFile(new XmlDocument()) { IsSelected = false };
            vm.ProjectFiles.Add(projFile);
            vm.ProjectFiles.RemoveAt(0); // here invocationsCount has to be 1
            projFile.IsSelected = true; // no change in invocationsCount should occur here
            Assert.AreEqual(1, invocationsCount);
        }
    }
}
