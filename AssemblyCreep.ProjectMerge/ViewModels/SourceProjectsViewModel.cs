using AssemblyCreep.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Xml;

namespace AssemblyCreep.ViewModels
{
    public class SourceProjectsViewModel : BindableBase
    {
        private ObservableCollection<SelectedItemModel<XmlDocument>> projectFiles;
        public ObservableCollection<SelectedItemModel<XmlDocument>> ProjectFiles
        {
            get
            {
                return projectFiles;
            }

            set
            {
                SetProperty(ref projectFiles, value);
            }
        }
    }
}
