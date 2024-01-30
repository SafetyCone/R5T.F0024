using System;

using R5T.T0131;


namespace R5T.F0024.F001
{
    [ValuesMarker]
    public partial interface IGlobalSectionNames : IValuesMarker
    {
        public const string ExtensibilityGlobals_Constant = "ExtensibilityGlobals";
        public const string NestedProjects_Constant = "NestedProjects";
        public const string ProjectConfigurationPlatforms_Constant = "ProjectConfigurationPlatforms";
        public const string SolutionConfigurationPlatforms_Constant = "SolutionConfigurationPlatforms";
        public const string SolutionProperties_Constant = "SolutionProperties";

        public string ExtensibilityGlobals => IGlobalSectionNames.ExtensibilityGlobals_Constant;
        public string NestedProjects => IGlobalSectionNames.NestedProjects_Constant;
        public string ProjectConfigurationPlatforms => IGlobalSectionNames.ProjectConfigurationPlatforms_Constant;
        public string SolutionConfigurationPlatforms => IGlobalSectionNames.SolutionConfigurationPlatforms_Constant;
        public string SolutionProperties => IGlobalSectionNames.SolutionProperties_Constant;
    }
}
