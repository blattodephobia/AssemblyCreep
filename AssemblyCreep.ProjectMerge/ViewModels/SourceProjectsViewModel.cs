using AssemblyCreep.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace AssemblyCreep.ViewModels
{
    public class SourceProjectsViewModel : ProjectItemLoaderViewModel
    {
        private ObservableCollection<CsProjFile> projectFiles;
        public ObservableCollection<CsProjFile> ProjectFiles
        {
            get
            {
                return projectFiles;
            }

            set
            {
                if (projectFiles != null)
                {
                    projectFiles.CollectionChanged -= ProjectFiles_CollectionChanged;
                    foreach (CsProjFile projFile in projectFiles)
                    {
                        projFile.PropertyChanged -= ProjFile_PropertyChanged;
                    }
                }

                SetProperty(ref projectFiles, value);

                if (value != null)
                {
                    value.CollectionChanged += ProjectFiles_CollectionChanged;

                    foreach (CsProjFile projFile in projectFiles)
                    {
                        projFile.PropertyChanged += ProjFile_PropertyChanged;
                    }
                }
            }
        }

        private void ProjFile_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CsProjFile.IsSelected))
            {
                var projFile = sender as CsProjFile;
                if (projFile.IsSelected)
                {
                    OnItemsSelected(new[] { projFile });
                }
                else
                {
                    OnItemsDeselected(new[] { projFile });
                }
            }
        }

        private void ProjectFiles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems.Count > 0)
            {
                OnItemsSelected(e.NewItems.Cast<CsProjFile>());
            }

            if (e.OldItems.Count > 0)
            {
                OnItemsDeselected(e.OldItems.Cast<CsProjFile>());
            }
        }

        public ICommand DropFilesCommand { get; private set; }

        public CsProjFile GetXmlFileModel(Stream stream)
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

            var result = new CsProjFile(doc) { Description = error };
            return result;
        }

        private void OnItemsSelected(IEnumerable<CsProjFile> items)
        {

        }

        private void OnItemsDeselected(IEnumerable<CsProjFile> items)
        {

        }

        private void OnFilesDrop(DragEventArgs args)
        {
            IEnumerable<FileInfo> files = (args.Data.GetData("FileDrop") as string[]).Select(fn => new FileInfo(fn));
            foreach (FileInfo file in files)
            {
                if (ProjectFiles.Any(m => (m.Source as FileInfo)?.FullName == file.FullName)) continue;
                using (Stream fileStream = file.OpenRead())
                {
                    CsProjFile model = GetXmlFileModel(fileStream);
                    model.Source = file;
                    ProjectFiles.Add(model);
                }
            }
        }

        public SourceProjectsViewModel()
        {
            ProjectFiles = new ObservableCollection<CsProjFile>();
            DropFilesCommand = new DelegateCommand<DragEventArgs>(OnFilesDrop);
        }
    }
}
