using AssemblyCreep.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace AssemblyCreep.ViewModels
{
    public class SourceProjectsViewModel : ProjectItemLoaderViewModel
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

        public ICommand DropFilesCommand { get; private set; }

        public SelectedItemModel<XmlDocument> GetXmlFileModel(Stream stream)
        {
            XmlDocument doc = null;
            string error = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(stream);
            }
            catch (Exception e)
            {
                doc = null;
                error = e.Message;
            }

            var result = new SelectedItemModel<XmlDocument>(doc) { Description = error };
            return result;
        }

        private void OnFilesDrop(DragEventArgs args)
        {
            IEnumerable<FileInfo> files = (args.Data.GetData("FileDrop") as string[]).Select(fn => new FileInfo(fn));
            foreach (FileInfo file in files)
            {
                if (ProjectFiles.Any(m => (m.Source as FileInfo)?.FullName == file.FullName)) continue;
                using (Stream fileStream = file.OpenRead())
                {
                    SelectedItemModel<XmlDocument> model = GetXmlFileModel(fileStream);
                    model.Source = file;
                    ProjectFiles.Add(model);
                }
            }
        }

        public SourceProjectsViewModel()
        {
            ProjectFiles = new ObservableCollection<SelectedItemModel<XmlDocument>>();
            DropFilesCommand = new DelegateCommand<DragEventArgs>(OnFilesDrop);
        }
    }
}
