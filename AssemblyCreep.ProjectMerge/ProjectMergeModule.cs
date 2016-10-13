﻿using System;
using Prism.Modularity;
using Prism.Regions;
using AssemblyCreep.ViewModels;
using AssemblyCreep.Views;

namespace AssemblyCreep.ProjectMerge
{
    [Module(ModuleName = nameof(ProjectMergeModule), OnDemand = true)]
    public class ProjectMergeModule : IModule
    {
        private readonly IRegionManager regionManager;

        public void Initialize()
        {
            this.regionManager.RegisterViewWithRegion(Regions.MainRegion, typeof(MergerView));
        }

        public ProjectMergeModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
    }
}