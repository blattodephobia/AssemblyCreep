using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AssemblyCreep.Models
{
    public class XmlMergeContext
    {
        public XmlDocument Document { get; private set; }

        public XmlMergeContext(XmlDocument document)
        {
            Document = document;
        }
    }
}
