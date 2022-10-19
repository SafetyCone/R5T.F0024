using System;
using System.Collections.Generic;

using R5T.F0024.T001;

using Instances = R5T.F0024.Instances;


namespace System.Linq
{
    public static class ProjectFileReferenceExtensions
    {
		public static IEnumerable<ProjectFileReference> WhereIsNotSolutionFolder(this IEnumerable<ProjectFileReference> projectFileReferences)
		{
			var output = Instances.ProjectFileReferenceOperator.WhereIsNotSolutionFolder(projectFileReferences);
			return output;
		}
	}
}
