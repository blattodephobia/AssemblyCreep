using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AssemblyCreep
{
    [XmlRoot("Compile")]
    public class CompilationItem : ProjectItem
    {
        private string _filePath;
        [XmlAttribute("Include")]
        public string FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                _filePath = value;
                _file = new FileInfo(value);
            }
        }

        private FileInfo _file;
        [XmlIgnore]
        public FileInfo File
        {
            get
            {
                return _file;
            }

            set
            {
                _file = value;
                _filePath = value?.FullName;
            }
        }
    }
}
