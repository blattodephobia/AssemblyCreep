using AssemblyCreep.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyCreep.Models
{
    public class FileLoadEventArgs
    {
        public IEnumerable<CsProjFile> LoadedFiles { get; private set; }

        public IEnumerable<CsProjFile> UnloadedFiles { get; private set; }

        public FileLoadEventArgs(IEnumerable<CsProjFile> loadedFiles, IEnumerable<CsProjFile> unloadedFiles)
        {
            LoadedFiles = loadedFiles ?? Enumerable.Empty<CsProjFile>();
            UnloadedFiles = unloadedFiles ?? Enumerable.Empty<CsProjFile>();
        }
    }
}
