using System;

using R5T.T0131;


namespace R5T.F0024.V000
{
	[ValuesMarker]
	public partial interface ISolutionFilePaths : IValuesMarker
	{
		public string ForTestingOutput => Environment.CurrentDirectory + "\\" + @"Files\Solution.sln";
	}
}