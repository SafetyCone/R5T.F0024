using System;

using R5T.T0131;


namespace R5T.F0024
{
	[ValuesMarker]
	public partial interface IStrings : IValuesMarker
	{
		public string FormatInformation_Unspecified => $"<{this.MicrosoftVisualStudioSolutionFile}, Format Version UNSPECIFIED>";
		public string MicrosoftVisualStudioSolutionFile => "Microsoft Visual Studio Solution File";
		public string MinimumVisualStudioVersion_Unspecified => "<MinimumVisualStudioVersion = UNSPECIFIED>";
		public string VersionDescription_Unspecified => "<# Visual Studio Version UNSPECIFIED>";
		public string VisualStudioVersion_Unspecified => "<VisualStudioVersion = UNSPECIFIED>";
    }
}