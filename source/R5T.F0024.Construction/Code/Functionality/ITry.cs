using System;

using R5T.T0132;


namespace R5T.F0024.Construction
{
	[FunctionalityMarker]
	public partial interface ITry : IFunctionalityMarker
	{
		/// <summary>
		/// Useful during construction of the <see cref="T001.SolutionFile"/> deserialization logic.
		/// </summary>
		public void SolutionFileRoundTrip()
		{
			/// Inputs.
			var initialSolutionFilePath =
				//Z0008.ExampleSolutionFilePaths.Instance.ExampleWithEmptyNestedProjetsGlobalSection
				Z0008.ExampleSolutionFilePaths.Instance.ExampleWithSolutionItemsProject
				;
			var outputSolutionFilePath = Z0015.FilePaths.Instance.OutputTextFilePath;

			/// Run.
			var solutionFile = SolutionFileSerializer.Instance.Deserialize(initialSolutionFilePath);

			SolutionFileSerializer.Instance.Serialize(
				outputSolutionFilePath,
				solutionFile);

			F0033.NotepadPlusPlusOperator.Instance.Open(
				initialSolutionFilePath,
				outputSolutionFilePath);

            Instances.FileEqualityVerifier.VerifyFileByteLevelEquality(
                initialSolutionFilePath,
                outputSolutionFilePath);
        }
	}
}