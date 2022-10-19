using System;

using R5T.C0003.F001;
using R5T.F0000;
using R5T.F0033;
using R5T.F0063;
using R5T.Z0015;


namespace R5T.F0024.Construction
{
    public static class Instances
    {
        public static ISolutionFileExplorations SolutionFileExplorations { get; } = Construction.SolutionFileExplorations.Instance;

        public static ISolutionFileGeneratorDemonstrations SolutionFileGeneratorDemonstrations { get; } = Construction.SolutionFileGeneratorDemonstrations.Instance;
        public static ISolutionFileOperatorDemonstrations SolutionFileOperatorDemonstrations { get; } = Construction.SolutionFileOperatorDemonstrations.Instance;

        public static IEnumerableOperator EnumerableOperator { get; } = F0000.EnumerableOperator.Instance;
        public static IFileEqualityVerifier FileEqualityVerifier { get; } = Construction.FileEqualityVerifier.Instance;
        public static IFileOperator FileOperator { get; } = F0000.FileOperator.Instance;
        public static IFilePaths FilePaths { get; } = Z0015.FilePaths.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static F0000.IGuidOperator GuidOperator { get; } = F0000.GuidOperator.Instance;
        public static INotepadPlusPlusOperator NotepadPlusPlusOperator { get; } = F0033.NotepadPlusPlusOperator.Instance;
        public static IOperations Operations { get; } = Construction.Operations.Instance;
        public static N000.IPathOperator PathOperator { get; } = N000.PathOperator.Instance;
        public static IRandomOperator RandomOperator { get; } = F0000.RandomOperator.Instance;
        public static ISolutionFileGenerator SolutionFileGenerator { get; } = F0024.SolutionFileGenerator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = F0024.SolutionFileOperator.Instance;
        public static ISolutionFileOperations SolutionFileOperations { get; } = Construction.SolutionFileOperations.Instance;
        public static ISolutionOperations SolutionOperations { get; } = F0063.SolutionOperations.Instance;
        public static ITextOutputGenerator TextOutputGenerator { get; } = C0003.F001.TextOutputGenerator.Instance;
    }
}