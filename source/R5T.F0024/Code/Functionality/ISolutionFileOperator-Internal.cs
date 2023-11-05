using System;
using System.Collections.Generic;
using System.Linq;

using R5T.N0000;

using R5T.F0024.T001;


namespace R5T.F0024.Internal
{
    public partial interface ISolutionFileOperator
    {
		public void AddGlobalSection(
			SolutionFile solutionFile,
            IGlobalSection globalSection)
        {
			solutionFile.GlobalSections.Add(globalSection);
        }

		/// <summary>
		/// Simply adds the project file reference.
		/// </summary>
		public void AddProjectReference(
			SolutionFile solutionFile,
			ProjectFileReference projectFileReference)
        {
			solutionFile.ProjectFileReferences.Add(projectFileReference);
        }

		public WasFound<ProjectFileReference> HasProject(
			SolutionFile solutionFile,
			string solutionFilePath,
			string projectFilePath)
        {
			var projectRelativeFilePath = Instances.PathOperator.GetProjectRelativeFilePath(
				solutionFilePath,
				projectFilePath);

			var output = this.HasProject(
				solutionFile,
				projectRelativeFilePath);

			return output;
        }

		public Dictionary<string, WasFound<ProjectFileReference>> HasProjects(
			SolutionFile solutionFile,
			string solutionFilePath,
			IEnumerable<string> projectFilePaths)
		{
			var projectRelativeFilePathsByFilePath = Instances.PathOperator.GetProjectRelativeFilePathsByFilePath(
				solutionFilePath,
				projectFilePaths);

			var projectFileReferencesByRelativeFilePath = solutionFile.GetProjectFileReferences()
				.ToDictionary(
					projectFileReference => projectFileReference.ProjectRelativeFilePath,
					projectFileReference => projectFileReference);

			var join =
				from xPair in projectRelativeFilePathsByFilePath
				join yPair in projectFileReferencesByRelativeFilePath on xPair.Value equals yPair.Key into groupJoin
				from joinPair in groupJoin.DefaultIfEmpty()
				select new { ProjectFilePath = xPair.Key, ProjectFileReference = joinPair.Value };

			var output = join
				.ToDictionary(
					x => x.ProjectFilePath,
					x => WasFound.From(x.ProjectFileReference));

			return output;
		}

		/// <summary>
		/// Chooses <see cref="HasProject_ByRelativeProjectFilePath(SolutionFile, string)"/> as the default.
		/// </summary>
		public WasFound<ProjectFileReference> HasProject(
			SolutionFile solutionFile,
			string projectRelativeFilePath)
		{
			return this.HasProject_ByRelativeProjectFilePath(
				solutionFile,
				projectRelativeFilePath);
		}

        public WasFound<ProjectFileReference> HasProject_ByRelativeProjectFilePath(
            SolutionFile solutionFile,
            string projectRelativeFilePath)
        {
			var projectOrDefault = solutionFile.ProjectFileReferences
				.Where(x => x.ProjectRelativeFilePath == projectRelativeFilePath)
				// Use robust First() even though there should not be multiple.
				.FirstOrDefault();

			var output = WasFound.From(projectOrDefault);
			return output;
        }

		public WasFound<ProjectFileReference> HasProject_ByProjectFilePath(
			SolutionFile solutionFile,
			string solutionFilePath,
			string projectFilePath)
		{
            // Check to see if the project even exists.
            var projectRelativeFilePath = Instances.PathOperator.GetProjectRelativeFilePath(
                solutionFilePath,
                projectFilePath);

            var output = this.HasProject(
                solutionFile,
                projectRelativeFilePath);

			return output;
        }

		public ProjectFileReference Verify_HasProject_ByProjectFilePath(
            SolutionFile solutionFile,
            string solutionFilePath,
            string projectFilePath)
		{
            var hasProject = this.HasProject_ByProjectFilePath(
                solutionFile,
				solutionFilePath,
                projectFilePath);

            if (!hasProject)
            {
                throw new InvalidOperationException($"Solution did have have project: '{projectFilePath}'.");
            }

			return hasProject.Result;
        }
	}
}