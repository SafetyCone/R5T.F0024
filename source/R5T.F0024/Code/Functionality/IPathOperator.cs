using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;


namespace R5T.F0024.N000
{
	[FunctionalityMarker]
	public partial interface IPathOperator : IFunctionalityMarker
	{
		public string GetProjectFilePath(
			string solutionFilePath,
			string projectSolutionDirectoryRelativeFilePath)
        {
			var solutionDirectoryPath = Instances.PathOperator_Base.GetParentDirectoryPath(solutionFilePath);

			var projectFilePath = Instances.PathOperator_Base.Combine(
				solutionDirectoryPath,
				projectSolutionDirectoryRelativeFilePath);

			return projectFilePath;
		}

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

		public Dictionary<string, string> GetProjectRelativeFilePathsByFilePath(
			string solutionFilePath,
			IEnumerable<string> projectFilePaths)
		{
			var solutionDirectoryPath = Instances.PathOperator_Base.GetParentDirectoryPath(solutionFilePath);

			var projectRelativeFilePathsByFilePath = projectFilePaths
				.ToDictionary(
					projectFilePath => projectFilePath,
					projectFilePath => Instances.PathOperator_Base.GetRelativePath(
						solutionDirectoryPath,
						projectFilePath));

			return projectRelativeFilePathsByFilePath;
		}
	}
}