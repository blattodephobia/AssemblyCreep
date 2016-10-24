using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace AssemblyCreep.ViewModels
{
    public class TargetProjectViewModel : BindableBase
    {
        private XmlDocument projectFile;
        public XmlDocument ProjectFile
        {
            get
            {
                return projectFile;
            }

            private set
            {
                SetProperty(ref projectFile, value);
            }
        }

        public DelegateCommand CsProjFileLoadCommand { get; private set; }

        public ICommand DropFileCommand { get; private set; }

        public ICommand BrowseFileCommand { get; private set; }

        private string projectFilePath;
        public string ProjectFilePath
        {
            get
            {
                return projectFilePath;
            }

            set
            {
                SetProperty(ref projectFilePath, value);
                CsProjFileLoadCommand.RaiseCanExecuteChanged();
            }
        }

        private void BrowseFile()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                DefaultExt = "Project files|*.csproj",
                CheckPathExists = true,
                Filter = "Project files|*.csproj"
            };

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                ProjectFilePath = dialog.FileName;
            }
        }

        private void LoadCsProjFile(string fileName)
        {
            if (CsProjFileLoadCommand.CanExecute())
            {
                try
                {
                    ProjectFile = new XmlDocument();
                    ProjectFile.Load(fileName);
                }
                catch (XmlException)
                {
                    MessageBox.Show("Invalid .csproj file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public TargetProjectViewModel()
        {
            BrowseFileCommand = new DelegateCommand(() => BrowseFile());
            CsProjFileLoadCommand = new DelegateCommand(() => LoadCsProjFile(ProjectFilePath), () => !string.IsNullOrEmpty(ProjectFilePath) && File.Exists(ProjectFilePath));
            DropFileCommand = new DelegateCommand<DragEventArgs>(args =>
            {
                string[] fileNames = (args.Data.GetData("FileNameW") ?? args.Data.GetData("FileName")) as string[];
                if (fileNames?.Length == 1)
                {
                    ProjectFilePath = fileNames[0];
                }
                else
                {
                    args.Effects = DragDropEffects.None;
                    args.Handled = true;
                }
            });
        }
    }
}
