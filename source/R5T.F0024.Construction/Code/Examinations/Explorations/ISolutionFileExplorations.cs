using System;
using System.IO;
using System.Linq;

using R5T.T0141;


namespace R5T.F0024.Construction
{
	[ExplorationsMarker]
	public partial interface ISolutionFileExplorations : IExplorationsMarker
	{
		public void FindVersionBytesAtFileBeginning()
        {
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0034\source\R5T.S0034.sln";

			var asString = File.ReadAllText(solutionFilePath);
			var bytes = File.ReadAllBytes(solutionFilePath);

			var asChars = bytes
				.Select(x => Convert.ToChar(x))
				.ToArray();
        }
	}
}