using System;

using R5T.F0000;


namespace R5T.F0024.Construction
{
    public static class Instances
    {
        public static ISolutionFileExplorations SolutionFileExplorations { get; } = Construction.SolutionFileExplorations.Instance;

        public static ISolutionFileGeneratorDemonstrations SolutionFileGeneratorDemonstrations { get; } = Construction.SolutionFileGeneratorDemonstrations.Instance;
        public static ISolutionFileOperatorDemonstrations SolutionFileOperatorDemonstrations { get; } = Construction.SolutionFileOperatorDemonstrations.Instance;

        public static IFileEqualityVerifier FileEqualityVerifier { get; } = Construction.FileEqualityVerifier.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static F0000.IGuidOperator GuidOperator { get; } = F0000.GuidOperator.Instance;
        public static IRandomOperator RandomOperator { get; } = F0000.RandomOperator.Instance;
        public static ISolutionFileGenerator SolutionFileGenerator { get; } = F0024.SolutionFileGenerator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = F0024.SolutionFileOperator.Instance;
    }
}