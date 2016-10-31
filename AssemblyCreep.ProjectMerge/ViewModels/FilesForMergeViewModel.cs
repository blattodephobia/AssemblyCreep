using AssemblyCreep.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;

namespace AssemblyCreep.ViewModels
{
    public class FilesForMergeViewModel : BindableBase
    {
        private ObservableCollection<SelectedDependentItemModel<FileInfo, XmlDocument>> discoveredFiles;
        public ObservableCollection<SelectedDependentItemModel<FileInfo, XmlDocument>> DiscoveredFiles
        {
            get
            {
                return discoveredFiles;
            }

            private set
            {
                SetProperty(ref discoveredFiles, value);
            }
        }

        private bool useNewFolderStructure;
        public bool UseNewFolderStructure
        {
            get
            {
                return useNewFolderStructure;
            }

            set
            {
                SetProperty(ref useNewFolderStructure, value);
            }
        }

        public IEnumerable<SelectedDependentItemModel<FileInfo, XmlDocument>> GetSelectedFiles() => DiscoveredFiles.Where(f => f.IsSelected);
    }
}
