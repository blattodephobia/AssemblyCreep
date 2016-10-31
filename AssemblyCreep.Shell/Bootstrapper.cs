using Prism.Unity;
using Prism.Modularity;
using System;

namespace AssemblyCreep
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            Type projMergeType = typeof(ProjectMergeModule);
            ModuleCatalog.AddModule(new ModuleInfo(projMergeType.Name, projMergeType.AssemblyQualifiedName));
        }
    }
}
