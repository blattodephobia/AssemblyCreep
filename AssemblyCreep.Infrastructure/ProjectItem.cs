using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AssemblyCreep
{
    public class ProjectItem
    {
        protected string GetAttributeName(string propertyName) =>
            this.GetType().GetProperty(propertyName).GetCustomAttribute<XmlAttributeAttribute>()?.AttributeName ?? propertyName;
    }
}
