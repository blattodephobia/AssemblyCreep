using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AssemblyCreep.Models
{
    public class AssemblyReferenceMerge : XmlMergeBase
    {
        public override IEnumerable<XmlNode> SelectNodesForMerge()
        {
            return Enumerable.Empty<XmlNode>();
        }
    }
}
