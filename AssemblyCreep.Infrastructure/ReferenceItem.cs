using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AssemblyCreep
{
    [XmlRoot("Reference")]
    public class ReferenceItem : ProjectItem, IXmlSerializable
    {
        [XmlAttribute("Include")]
        public AssemblyName Assembly { get; set; }

        public ReferenceItem()
        {
        }

        XmlSchema IXmlSerializable.GetSchema() => null;

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            string assemblyName = reader.GetAttribute(GetAttributeName(nameof(Assembly)));
            Assembly = new AssemblyName(assemblyName);
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(GetAttributeName(nameof(Assembly)), Assembly.FullName);
        }
    }
}