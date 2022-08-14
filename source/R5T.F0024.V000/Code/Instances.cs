using System;

using R5T.F0002;
using R5T.Z0008;


namespace R5T.F0024.V000
{
    public static class Instances
    {
        public static IExampleFilePaths ExampleFilePaths { get; } = Z0008.ExampleFilePaths.Instance;
        public static IFileEqualityVerifier FileEqualityVerifier { get; } = F0002.FileEqualityVerifier.Instance;
        public static ISolutionFileGenerator SolutionFileGenerator { get; } = F0024.SolutionFileGenerator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = F0024.SolutionFileOperator.Instance;
        public static ISolutionFilePaths SolutionFilePaths { get; } = V000.SolutionFilePaths.Instance;
    }
}