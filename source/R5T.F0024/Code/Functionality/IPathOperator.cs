using System;

using R5T.T0132;


namespace R5T.F0024.N000
{
	[FunctionalityMarker]
	public partial interface IPathOperator : IFunctionalityMarker
	{
		public string GetProjectRelativeFilePath(
			string solutionFilePath,
			string projectFilePath)
        {
			var solutionDirectoryPath = Instances.PathOperator_Base.GetParentDirectoryPath(solutionFilePath);

			var output = Instances.PathOperator_Base.GetRelativePath(
				solutionDirectoryPath,
				projectFilePath);

			return output;
        }
	}
}