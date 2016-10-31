using Prism.Mvvm;
using System.IO;

namespace AssemblyCreep.ViewModels
{
    public class MergerViewModel : BindableBase
    {
        public FileInfo GetTargetFileInfo(FileInfo sourceFileInfo) =>
            new FileInfo(FileMergeViewModel.UseNewFolderStructure
                ? sourceFileInfo.FullName.Replace('.', '\\')
                : sourceFileInfo.FullName);

        private FilesForMergeViewModel fileMergeViewModel;
        public FilesForMergeViewModel FileMergeViewModel
        {
            get
            {
                return fileMergeViewModel;
            }

            private set
            {
                SetProperty(ref fileMergeViewModel, value);
            }
        }

        private ReferencesForMergeViewModel referencesViewModel;
        public ReferencesForMergeViewModel ReferencesViewModel
        {
            get
            {
                return referencesViewModel;
            }

            private set
            {
                SetProperty(ref referencesViewModel, value);
            }
        }

        private TargetProjectViewModel targetProjectViewModel;
        public TargetProjectViewModel TargetProjectViewModel
        {
            get
            {
                return targetProjectViewModel;
            }

            private set
            {
                SetProperty(ref targetProjectViewModel, value);
            }
        }
    }
}
