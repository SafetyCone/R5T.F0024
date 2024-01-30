using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface IPathOperator : IFunctionalityMarker
    {
#pragma warning disable IDE1006 // Naming Styles
        public L0066.IPathOperator _Base => L0066.PathOperator.Instance;
#pragma warning restore IDE1006 // Naming Styles


        public string GetProjectFilePath(
            string solutionFilePath,
            string projectSolutionDirectoryRelativeFilePath)
        {
            var solutionDirectoryPath = _Base.Get_ParentDirectoryPath_ForFile(solutionFilePath);

            var projectFilePath = _Base.Combine(
                solutionDirectoryPath,
                projectSolutionDirectoryRelativeFilePath);

            return projectFilePath;
        }

        public string GetProjectRelativeFilePath(
            string solutionFilePath,
            string projectFilePath)
        {
            var solutionDirectoryPath = _Base.Get_ParentDirectoryPath_ForFile(solutionFilePath);

            var output = _Base.Get_RelativePath(
                solutionDirectoryPath,
                projectFilePath);

            return output;
        }

        public Dictionary<string, string> GetProjectRelativeFilePathsByFilePath(
            string solutionFilePath,
            IEnumerable<string> projectFilePaths)
        {
            var solutionDirectoryPath = _Base.Get_ParentDirectoryPath_ForFile(solutionFilePath);

            var projectRelativeFilePathsByFilePath = projectFilePaths
                .ToDictionary(
                    projectFilePath => projectFilePath,
                    projectFilePath => _Base.Get_RelativePath(
                        solutionDirectoryPath,
                        projectFilePath));

            return projectRelativeFilePathsByFilePath;
        }
    }
}
