using System;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface IOperations : IFunctionalityMarker
    {
        public void AddGlobalSection(
            SolutionFile solutionFile,
            IGlobalSection globalSection)
        {
            solutionFile.GlobalSections.Add(globalSection);
        }

        public void AddGlobalSection(
            SolutionFile solutionFile,
            Func<IGlobalSection> globalSectionConstructor)
        {
            var globalSection = globalSectionConstructor();

            this.AddGlobalSection(solutionFile, globalSection);
        }

        public void WithVersionInformation(
            SolutionFile solutionFile,
            VersionInformation versionInformation)
        {
            solutionFile.VersionInformation = versionInformation;
        }

        public void WithVersionInformation(
            SolutionFile solutionFile,
            Func<VersionInformation> versionInformationConstructor)
        {
            var versionInformation = versionInformationConstructor();

            solutionFile.VersionInformation = versionInformation;
        }
    }
}
