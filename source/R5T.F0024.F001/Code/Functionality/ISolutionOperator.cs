using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface ISolutionOperator : IFunctionalityMarker
    {
        public Dictionary<string, Guid> AddProjects_Idempotent(
            string solutionFilePath,
            IEnumerable<string> projectReferenceFilePaths)
        {
            var projectsMissingFromSolution = this.GetProjectsMissingFromSolution(
                solutionFilePath,
                projectReferenceFilePaths);

            var output = Instances.SolutionFileOperator.AddProjects(
                solutionFilePath,
                projectsMissingFromSolution);

            return output;
        }

        public string[] GetProjects(string solutionFilePath)
        {
            var projects = Instances.SolutionFileOperator.Get_ProjectReferenceFilePaths(solutionFilePath);
            return projects;
        }

        public string[] GetProjectsMissingFromSolution(
            string solutionFilePath,
            IEnumerable<string> desiredProjectFilePaths)
        {
            var projectsInSolution = this.GetProjects(solutionFilePath);

            var projectsMissingFromSolution = desiredProjectFilePaths
                .Except(projectsInSolution)
                .Now();

            return projectsMissingFromSolution;
        }

        public IEnumerable<string> GetSolutionsContainingProject(
            IEnumerable<string> solutionFilePaths,
            string projectFilePath)
        {
            var output = solutionFilePaths
                .Where(solutionFilePath => this.HasProjectReference(
                    solutionFilePath,
                    projectFilePath))
                ;

            return output;
        }

        public bool HasProjectReference(
            string solutionFilePath,
            string projectFilePath)
        {
            var hasProjectReference = Instances.SolutionFileOperator.HasProject_ByFilePath(
                solutionFilePath,
                projectFilePath);

            return hasProjectReference;
        }

        public void Set_DefaultStartupProject(
            string solutionFilePath,
            string projectFilePath)
        {
            Instances.SolutionFileOperator.InModifyContext_Synchronous(
                solutionFilePath,
                solutionFile => Instances.SolutionFileOperator.Set_DefaultStartupProject(
                    solutionFile,
                    solutionFilePath,
                    projectFilePath));
        }
    }
}
