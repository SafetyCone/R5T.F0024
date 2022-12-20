using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    public class SolutionConfigurationPlatformsGlobalSection : SectionBase, IGlobalSection
    {
        public List<SolutionBuildConfigurationPlatform> SolutionBuildConfigurationMappings { get; } = new List<SolutionBuildConfigurationPlatform>();
    }
}
