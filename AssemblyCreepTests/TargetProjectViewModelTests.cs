using AssemblyCreep.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AssemblyCreepProjectMergeTests
{
    [TestClass]
    public class MergeTests
    {
        [TestMethod]
        public void CreatesCopyOfXmlProjectFile()
        {
            TargetProjectViewModel vm = new TargetProjectViewModel();
            vm.ProjectFile = new XmlDocument();
            vm.ProjectFile.LoadXml(@"
<Root>
    <Child>
    </Child>
</Root>
");
            XmlDocument merged = vm.Merge();
            vm.ProjectFile.DocumentElement.SetAttribute("test", "test");

            Assert.AreEqual(vm.ProjectFile.DocumentElement.Name, merged.DocumentElement.Name);
            Assert.AreNotEqual("test", merged.DocumentElement.GetAttribute("test"));
        }
    }
}
