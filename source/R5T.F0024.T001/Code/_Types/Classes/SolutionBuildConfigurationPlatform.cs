using System;


namespace R5T.F0024.T001
{
    /// <summary>
    /// {Source} = {Destination}
    /// Example: Debug|Any CPU = Debug|Any CPU
    /// </summary>
    public class SolutionBuildConfigurationPlatform
    {
        public BuildConfigurationPlatform Source { get; set; }
        public BuildConfigurationPlatform Destination { get; set; }
    }
}
