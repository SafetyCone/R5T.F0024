﻿using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    public class ProjectConfigurationPlatformsGlobalSection : SectionBase, IGlobalSection
    {
        public List<ProjectBuildConfigurationMapping> ProjectBuildConfigurationMappings { get; } = new List<ProjectBuildConfigurationMapping>();
    }
}
