using System;


namespace R5T.F0024.T001
{
    public class BuildConfigurationPlatform
    {
        public static readonly BuildConfigurationPlatform DebugAnyCPU = new() { BuildConfiguration = BuildConfiguration.Debug, Platform = Platform.AnyCPU };
        public static readonly BuildConfigurationPlatform DebugX64 = new() { BuildConfiguration = BuildConfiguration.Debug, Platform = Platform.x64 };
        public static readonly BuildConfigurationPlatform DebugX86 = new() { BuildConfiguration = BuildConfiguration.Debug, Platform = Platform.x86 };
        public static readonly BuildConfigurationPlatform ReleaseAnyCPU = new() { BuildConfiguration = BuildConfiguration.Release, Platform = Platform.AnyCPU };
        public static readonly BuildConfigurationPlatform ReleaseX64 = new() { BuildConfiguration = BuildConfiguration.Release, Platform = Platform.x64 };
        public static readonly BuildConfigurationPlatform ReleaseX86 = new() { BuildConfiguration = BuildConfiguration.Release, Platform = Platform.x86 };


        public BuildConfiguration BuildConfiguration { get; set; }
        public Platform Platform { get; set; }
    }
}
