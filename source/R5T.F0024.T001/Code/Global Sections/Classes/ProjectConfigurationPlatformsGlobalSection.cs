using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    public class ProjectConfigurationPlatformsGlobalSection : GlobalSectionBase
    {
        public List<ProjectBuildConfigurationMapping> ProjectBuildConfigurationMappings { get; } = new List<ProjectBuildConfigurationMapping>();
    }
}
