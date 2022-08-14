using System;
using System.IO;

using R5T.T0132;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface ISolutionFileGenerator : IFunctionalityMarker
	{
		/// <summary>
		/// Creates a new, empty solution file.
		/// </summary>
		/// <param name="projectFilePath"></param>
		public void CreateNew(string solutionFilePath)
		{
			var solutionGuid = Instances.GuidOperator.New();

			var text =
$@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 16
VisualStudioVersion = 16.0.32002.261
MinimumVisualStudioVersion = 10.0.40219.1
Global
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {Instances.GuidOperator.ToString_ForSolutionFile(solutionGuid)}
	EndGlobalSection
EndGlobal
";
			this.WriteText(
				solutionFilePath,
				text,
				// No trimming, somehow the Visual Studio blank solution template begins and ends with a new line.
				false);
		}

		public void WriteText(
			string filePath,
			string text,
			bool trim = true)
		{
			// Trim text.
			var outputText = trim
				? text.Trim()
				: text
				;

			// Write text synchronously.
			using var stream = FileStreamHelper.NewWrite(filePath);
			using var writer = StreamWriterHelper.NewLeaveOpenAddBOM(stream);

			writer.WriteLine(outputText);
		}
	}
}