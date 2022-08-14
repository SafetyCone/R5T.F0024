using System;

using R5T.T0131;


namespace R5T.F0024
{
	[ValuesMarker]
	public partial interface IGlobalSectionNames : IValuesMarker
	{
		public string ExtensibilityGlobals => "ExtensibilityGlobals";
		public string NestedProjects => "NestedProjects";
		public string ProjectConfigurationPlatforms => "ProjectConfigurationPlatforms";
		public string SolutionConfigurationPlatforms => "SolutionConfigurationPlatforms";
		public string SolutionProperties => "SolutionProperties";
	}
}