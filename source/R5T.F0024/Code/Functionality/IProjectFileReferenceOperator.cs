using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface IProjectFileReferenceOperator : IFunctionalityMarker
	{
		public IEnumerable<ProjectFileReference> WhereIsNotSolutionFolder(IEnumerable<ProjectFileReference> projectFileReferences)
		{
			var output = projectFileReferences
				.Where(projectFileReference => projectFileReference.ProjectTypeIdentity != Instances.ProjectTypeIdentities.SolutionFolder)
				;

			return output;
		}

		public IEnumerable<ProjectFileReference> WhereIsSolutionFolder(IEnumerable<ProjectFileReference> projectFileReferences)
		{
			var output = projectFileReferences
				.Where(projectFileReference => projectFileReference.ProjectTypeIdentity == Instances.ProjectTypeIdentities.SolutionFolder)
				;

			return output;
		}
	}
}