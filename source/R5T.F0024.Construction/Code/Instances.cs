using System;


namespace R5T.F0024.Construction
{
    public static class Instances
    {
        public static ISolutionFileGeneratorDemonstrations SolutionFileGeneratorDemonstrations { get; } = Construction.SolutionFileGeneratorDemonstrations.Instance;

        public static IFileEqualityVerifier FileEqualityVerifier { get; } = Construction.FileEqualityVerifier.Instance;
        public static ISolutionFileGenerator SolutionFileGenerator { get; } = F0024.SolutionFileGenerator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = F0024.SolutionFileOperator.Instance;
    }
}