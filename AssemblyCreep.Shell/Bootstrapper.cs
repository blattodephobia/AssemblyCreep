using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using AssemblyCreep.ProjectMerge;

namespace AssemblyCreep
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog.AddModule(new ModuleInfo(nameof(ProjectMergeModule), typeof(ProjectMergeModule).FullName));
        }
    }
}
