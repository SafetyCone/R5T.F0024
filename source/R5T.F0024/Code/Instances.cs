using System;


namespace R5T.F0024
{
    public static class Instances
    {
        public static L0066.IActionOperator ActionOperator => L0066.ActionOperator.Instance;
        public static L0066.ICharacters Characters => L0066.Characters.Instance;
        public static L0066.IEnumerableOperator EnumerableOperator => L0066.EnumerableOperator.Instance;
        public static L0066.IEnumerationOperator EnumerationOperator => L0066.EnumerationOperator.Instance;
        public static L0066.IFileOperator FileOperator => L0066.FileOperator.Instance;
        public static L0066.IFileStreamOperator FileStreamOperator => L0066.FileStreamOperator.Instance;
        public static L0066.IFileSystemOperator FileSystemOperator => L0066.FileSystemOperator.Instance;
        public static IGlobalSectionGenerator GlobalSectionGenerator => F0024.GlobalSectionGenerator.Instance;
        public static IGlobalSectionNames GlobalSectionNames => F0024.GlobalSectionNames.Instance;
        public static IGlobalSectionOperator GlobalSectionOperator => F0024.GlobalSectionOperator.Instance;
        public static IGuidOperator GuidOperator => F0024.GuidOperator.Instance;
        public static IOperations Operations => F0024.Operations.Instance;
        public static IPathOperator PathOperator => F0024.PathOperator.Instance;
        public static IProjectFileOperator ProjectFileOperator => F0024.ProjectFileOperator.Instance;
        public static IProjectFileReferenceOperator ProjectFileReferenceOperator => F0024.ProjectFileReferenceOperator.Instance;
        public static Z0009.IProjectTypeIdentities ProjectTypeIdentities => Z0009.ProjectTypeIdentities.Instance;
        public static ISolutionFileFormatVersionStrings SolutionFileFormatVersionStrings => F0024.SolutionFileFormatVersionStrings.Instance;
        public static ISolutionFileOperator SolutionFileOperator => F0024.SolutionFileOperator.Instance;
        public static Internal.ISolutionFileOperator SolutionFileOperator_Internal => Internal.SolutionFileOperator.Instance;
        public static ISolutionFileSerializer SolutionFileSerializer => F0024.SolutionFileSerializer.Instance;
        public static L0066.IStreamWriterOperator StreamWriterOperator => L0066.StreamWriterOperator.Instance;
        public static L0066.IStringOperator StringOperator => L0066.StringOperator.Instance;
        public static IStrings Strings => F0024.Strings.Instance;
        public static L0066.ISwitchOperator SwitchOperator => L0066.SwitchOperator.Instance;
        public static L0066.ITypeNameOperator TypeNameOperator => L0066.TypeNameOperator.Instance;
        public static IVersionInformationGenerator VersionInformationGenerator => F0024.VersionInformationGenerator.Instance;
        public static IVersionInformationOperator VersionInformationOperator => F0024.VersionInformationOperator.Instance;
        public static IVisualStudioVersions VisualStudioVersions => F0024.VisualStudioVersions.Instance;
        public static IVisualStudioVersionStrings VisualStudioVersionStrings => F0024.VisualStudioVersionStrings.Instance;
    }
}