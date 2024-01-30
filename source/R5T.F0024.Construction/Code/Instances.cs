using System;


namespace R5T.F0024.Construction
{
    public static class Instances
    {
        public static ISolutionFileExplorations SolutionFileExplorations { get; } = Construction.SolutionFileExplorations.Instance;

        public static ISolutionFileGeneratorDemonstrations SolutionFileGeneratorDemonstrations { get; } = Construction.SolutionFileGeneratorDemonstrations.Instance;
        public static ISolutionFileOperatorDemonstrations SolutionFileOperatorDemonstrations { get; } = Construction.SolutionFileOperatorDemonstrations.Instance;

        public static F0000.IEnumerableOperator EnumerableOperator { get; } = F0000.EnumerableOperator.Instance;
        public static IFileEqualityVerifier FileEqualityVerifier { get; } = Construction.FileEqualityVerifier.Instance;
        public static F0000.IFileOperator FileOperator { get; } = F0000.FileOperator.Instance;
        public static Z0015.IFilePaths FilePaths { get; } = Z0015.FilePaths.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static F0000.IGuidOperator GuidOperator { get; } = F0000.GuidOperator.Instance;
        public static F0033.INotepadPlusPlusOperator NotepadPlusPlusOperator { get; } = F0033.NotepadPlusPlusOperator.Instance;
        public static IOperations Operations { get; } = Construction.Operations.Instance;
        public static IPathOperator PathOperator { get; } = F0024.PathOperator.Instance;
        public static F0000.IRandomOperator RandomOperator { get; } = F0000.RandomOperator.Instance;
        public static ISolutionFileGenerator SolutionFileGenerator { get; } = F0024.SolutionFileGenerator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = F0024.SolutionFileOperator.Instance;
        public static ISolutionFileOperations SolutionFileOperations { get; } = Construction.SolutionFileOperations.Instance;
        public static F0063.ISolutionOperations SolutionOperations { get; } = F0063.SolutionOperations.Instance;
        public static C0003.F001.ITextOutputGenerator TextOutputGenerator { get; } = C0003.F001.TextOutputGenerator.Instance;
    }
}