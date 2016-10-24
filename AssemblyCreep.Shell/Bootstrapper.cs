using Prism.Unity;
using Prism.Modularity;

namespace AssemblyCreep
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(ProjectMergeModule));
        }
    }
}
