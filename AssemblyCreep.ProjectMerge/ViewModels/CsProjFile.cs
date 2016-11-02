using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AssemblyCreep.ViewModels
{
    public class CsProjFile : SelectedItemViewModel<XmlDocument>
    {
        public CsProjFile(XmlDocument contents) :
            base(contents)
        {
        }
    }
}
