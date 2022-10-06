using System;

using R5T.F0024.T001;

using Instances = R5T.F0024.Instances;


namespace System
{
    public static class SolutionFileExtensions
    {
        public static SolutionFile AddGlobalSection(this SolutionFile solutionFile,
            IGlobalSection globalSection)
        {
            Instances.Operations.AddGlobalSection(solutionFile, globalSection);

            return solutionFile;
        }

        public static SolutionFile AddGlobalSection(this SolutionFile solutionFile,
            Func<IGlobalSection> globalSectionConstructor)
        {
            Instances.Operations.AddGlobalSection(solutionFile, globalSectionConstructor);

            return solutionFile;
        }

        public static SolutionFile WithVersionInformation(this SolutionFile solutionFile,
            VersionInformation versionInformation)
        {
            Instances.Operations.WithVersionInformation(solutionFile, versionInformation);

            return solutionFile;
        }

        public static SolutionFile WithVersionInformation(this SolutionFile solutionFile,
            Func<VersionInformation> versionInformationConstructor)
        {
            Instances.Operations.WithVersionInformation(solutionFile, versionInformationConstructor);

            return solutionFile;
        }
    }
}
