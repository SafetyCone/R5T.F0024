using System;

using R5T.T0132;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface IProjectFileOperator : IFunctionalityMarker
    {
        public Guid GetProjectTypeIdentity_ForSolutionFile(string projectFile)
        {
            // TODO: Just always return C# for now.
            var output = Instances.ProjectTypeIdentities.CSharpProject;
            return output;
        }
    }
}
