using System;


namespace R5T.F0024.T001
{
    public class ProjectBuildConfigurationMapping
    {
        public Guid ProjectIdentity { get; set; }
        public BuildConfigurationPlatform BuildConfigurationPlatform { get; set; }
        public ProjectConfigurationIndicator ProjectConfigurationIndicator { get; set; }
        public BuildConfigurationPlatform MappedBuildConfigurationPlatform { get; set; }
    }
}
