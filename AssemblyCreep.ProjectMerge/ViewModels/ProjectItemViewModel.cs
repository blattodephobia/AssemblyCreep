using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyCreep.ViewModels
{
    public class ProjectItemViewModel : SelectedDependentItemViewModel<ProjectItem, CsProjFile>
    {
        public ProjectItemViewModel(ProjectItem item, CsProjFile csProjFile) :
            base(item, csProjFile)
        {
        }
    }
}
