using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    public class SolutionConfigurationPlatformsGlobalSection : GlobalSectionBase
    {
        public List<SolutionBuildConfigurationPlatform> SolutionBuildConfigurationMappings { get; } = new List<SolutionBuildConfigurationPlatform>();
    }
}
