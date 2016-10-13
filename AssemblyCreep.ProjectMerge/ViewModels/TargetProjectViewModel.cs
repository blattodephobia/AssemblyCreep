using Prism.Mvvm;
using System.Xml;

namespace AssemblyCreep.ViewModels
{
    public class TargetProjectViewModel : BindableBase
    {
        public TargetProjectViewModel()
        {
        }

        private XmlDocument projectFile;
        public XmlDocument ProjectFile
        {
            get
            {
                return projectFile;
            }

            set
            {
                SetProperty(ref projectFile, value);
            }
        }
    }
}
