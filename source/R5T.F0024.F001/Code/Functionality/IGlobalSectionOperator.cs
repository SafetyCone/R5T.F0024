using System;

using R5T.L0089.T000;
using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface IGlobalSectionOperator : IFunctionalityMarker
    {
#pragma warning disable IDE1006 // Naming Styles
        private static Internal.IGlobalSectionOperator _Internal { get; } = Internal.GlobalSectionOperator.Instance;
#pragma warning restore IDE1006 // Naming Styles


        #region Extensibility Globals

        public ExtensibilityGlobalsGlobalSection Get_ExtensibilityGlobals(SolutionFile solutionFile)
        {
            var hasExtensibilityGlobals = this.Has_ExtensibilityGlobals(solutionFile);
            if (!hasExtensibilityGlobals)
            {
                throw new Exception($"No extensibility globals section found.");
            }

            // Else.
            return hasExtensibilityGlobals;
        }

        public WasFound<ExtensibilityGlobalsGlobalSection> Has_ExtensibilityGlobals(SolutionFile solutionFile)
        {
            var output = _Internal.Has_GlobalSection<ExtensibilityGlobalsGlobalSection>(
                solutionFile,
                Instances.GlobalSectionNames.ExtensibilityGlobals);

            return output;
        }

        #endregion

        #region Nested Projects

        public NestedProjectsGlobalSection Acquire_NestedProjects(SolutionFile solutionFile)
        {
            var output = _Internal.Acquire_GlobalSection<NestedProjectsGlobalSection>(
                solutionFile,
                Instances.GlobalSectionNames.NestedProjects,
                this.New_NestedProjects);

            return output;
        }

        public NestedProjectsGlobalSection Get_NestedProjects(SolutionFile solutionFile)
        {
            var output = _Internal.Has_GlobalSection<NestedProjectsGlobalSection>(
                solutionFile,
                Instances.GlobalSectionNames.NestedProjects);

            if (!output)
            {
                throw new Exception("No nested projects global section found.");
            }

            return output;
        }

        public WasFound<NestedProjectsGlobalSection> Has_NestedProjects(SolutionFile solutionFile)
        {
            var output = _Internal.Has_GlobalSection<NestedProjectsGlobalSection>(
                solutionFile,
                Instances.GlobalSectionNames.NestedProjects);

            return output;
        }

        public NestedProjectsGlobalSection New_NestedProjects()
        {
            var output = new NestedProjectsGlobalSection
            {
                Name = Instances.GlobalSectionNames.NestedProjects,
                PreOrPost = SolutionFileStrings.Instance.PreSolution,
            };

            return output;
        }

        #endregion

        #region Project Configuration Platforms

        public ProjectConfigurationPlatformsGlobalSection Acquire_ProjectConfigurationPlatforms(SolutionFile solutionFile)
        {
            var output = _Internal.Acquire_GlobalSection(
                solutionFile,
                Instances.GlobalSectionNames.ProjectConfigurationPlatforms,
                this.New_ProjectConfigurationPlatforms);

            return output;
        }

        public ProjectConfigurationPlatformsGlobalSection Get_ProjectConfigurationPlatforms(SolutionFile solutionFile)
        {
            var hasProjectConfigurationPlatforms = this.Has_ProjectConfigurationPlatforms(solutionFile);
            if (!hasProjectConfigurationPlatforms)
            {
                throw new Exception("No project configuration platforms global section found.");
            }

            return hasProjectConfigurationPlatforms;
        }

        public WasFound<ProjectConfigurationPlatformsGlobalSection> Has_ProjectConfigurationPlatforms(SolutionFile solutionFile)
        {
            var output = _Internal.Has_GlobalSection<ProjectConfigurationPlatformsGlobalSection>(
                solutionFile,
                Instances.GlobalSectionNames.ProjectConfigurationPlatforms);

            return output;
        }

        /// <summary>
        /// Creates a new <see cref="ProjectConfigurationPlatformsGlobalSection"/> with the <see cref="IGlobalSectionNames.ProjectConfigurationPlatforms"/> and <see cref="ISolutionFileStrings.PostSolution"/>.
        /// </summary>
        public ProjectConfigurationPlatformsGlobalSection New_ProjectConfigurationPlatforms()
        {
            var output = new ProjectConfigurationPlatformsGlobalSection
            {
                Name = Instances.GlobalSectionNames.ProjectConfigurationPlatforms,
                PreOrPost = SolutionFileStrings.Instance.PostSolution,
            };

            return output;
        }

        #endregion

        #region Solution Configuration Platforms

        public SolutionConfigurationPlatformsGlobalSection Acquire_SolutionConfigurationPlatforms(SolutionFile solutionFile)
        {
            var output = _Internal.Acquire_GlobalSection(
                solutionFile,
                Instances.GlobalSectionNames.SolutionConfigurationPlatforms,
                this.NewDefault_SolutionConfigurationPlatforms);

            return output;
        }

        public void Add_DefaultSolutionBuildConfigurationPlatforms(SolutionConfigurationPlatformsGlobalSection solutionConfigurationPlatformsGlobalSection)
        {
            this.Add_AnyCpuSolutionBuildConfigurationPlatforms(solutionConfigurationPlatformsGlobalSection);
        }

        public void Add_AnyCpuSolutionBuildConfigurationPlatforms(SolutionConfigurationPlatformsGlobalSection solutionConfigurationPlatformsGlobalSection)
        {
            solutionConfigurationPlatformsGlobalSection.SolutionBuildConfigurationMappings.AddRange(new[]
            {
                new SolutionBuildConfigurationPlatform { Source = BuildConfigurationPlatform.DebugAnyCPU, Destination = BuildConfigurationPlatform.DebugAnyCPU },
                new SolutionBuildConfigurationPlatform { Source = BuildConfigurationPlatform.ReleaseAnyCPU, Destination = BuildConfigurationPlatform.ReleaseAnyCPU },
            });
        }

        public WasFound<SolutionConfigurationPlatformsGlobalSection> Has_SolutionConfigurationPlatforms(SolutionFile solutionFile)
        {
            var output = _Internal.Has_GlobalSection<SolutionConfigurationPlatformsGlobalSection>(
                solutionFile,
                Instances.GlobalSectionNames.SolutionConfigurationPlatforms);

            return output;
        }

        /// <summary>
        /// Creates a new <see cref="SolutionConfigurationPlatformsGlobalSection"/> with the <see cref="IGlobalSectionNames.SolutionConfigurationPlatforms"/> and <see cref="ISolutionFileStrings.PreSolution"/>.
        /// </summary>
        public SolutionConfigurationPlatformsGlobalSection New_SolutionConfigurationPlatforms()
        {
            var output = new SolutionConfigurationPlatformsGlobalSection
            {
                Name = Instances.GlobalSectionNames.SolutionConfigurationPlatforms,
                PreOrPost = SolutionFileStrings.Instance.PreSolution,
            };
            return output;
        }

        /// <summary>
        /// <inheritdoc cref="New_SolutionConfigurationPlatforms" path="/summary"/>
        /// Adds all default <see cref="SolutionBuildConfigurationPlatform"/> values.
        /// </summary>
        public SolutionConfigurationPlatformsGlobalSection NewDefault_SolutionConfigurationPlatforms()
        {
            var output = this.New_SolutionConfigurationPlatforms();

            this.Add_DefaultSolutionBuildConfigurationPlatforms(output);

            return output;
        }

        #endregion


        public void AddProjectConfigurations(
            ProjectConfigurationPlatformsGlobalSection projectConfigurationPlatforms,
            Guid projectGUID,
            SolutionConfigurationPlatformsGlobalSection solutionConfigurationPlatforms)
        {
            var indicators = new[]
            {
                ProjectConfigurationIndicator.ActiveCfg,
                ProjectConfigurationIndicator.Build0,
            };

            foreach (var solutionBuildConfigurationMapping in solutionConfigurationPlatforms.SolutionBuildConfigurationMappings)
            {
                var mappedSolutionBuildConfiguration = solutionBuildConfigurationMapping.Source.BuildConfiguration == BuildConfiguration.Debug
                    ? BuildConfigurationPlatform.DebugAnyCPU
                    : BuildConfigurationPlatform.ReleaseAnyCPU;

                foreach (var indicator in indicators)
                {
                    projectConfigurationPlatforms.ProjectBuildConfigurationMappings.Add(new ProjectBuildConfigurationMapping
                    {
                        ProjectIdentity = projectGUID,
                        BuildConfigurationPlatform = solutionBuildConfigurationMapping.Source,
                        MappedBuildConfigurationPlatform = mappedSolutionBuildConfiguration,
                        ProjectConfigurationIndicator = indicator,
                    });
                }
            }
        }
    }
}
