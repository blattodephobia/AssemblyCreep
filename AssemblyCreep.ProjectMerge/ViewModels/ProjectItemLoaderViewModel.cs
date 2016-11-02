using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AssemblyCreep.ViewModels
{
    public class ProjectItemLoaderViewModel : BindableBase
    {
        private ObservableCollection<ProjectItemViewModel> _loadedItems;
        public ObservableCollection<ProjectItemViewModel> LoadedItems
        {
            get
            {
                return _loadedItems;
            }

            private set
            {
                SetProperty(ref _loadedItems, value);
            }
        }
    }
}
