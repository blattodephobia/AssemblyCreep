using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AssemblyCreep.Models
{
    public abstract class XmlMergeBase
    {
        public XmlMergeContext Context { get; set; }

        protected virtual XmlNode MergeNode(XmlNode parent, XmlNode child)
        {
            parent.AppendChild(child);
            return parent;
        }

        public virtual XmlNode Merge(XmlNode parent, IEnumerable<XmlNode> nodes)
        {
            foreach (XmlNode node in nodes)
            {
                MergeNode(parent, node);
            }

            return parent;
        }

        public abstract IEnumerable<XmlNode> SelectNodesForMerge();
    }
}
