using AssemblyCreep.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml;

namespace AssemblyCreep.ViewModels
{
    public partial class MergerViewModel : BindableBase
    {
        public MergerViewModel()
        {
            InitSourceFilesSelection();
            InitTemplateSelection();
        }
    }
}
