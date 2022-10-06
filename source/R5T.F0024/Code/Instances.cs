using System;

using R5T.F0002;

using R5T.Z0000;
using R5T.Z0009;

using IGuidOperator_Base = R5T.F0000.IGuidOperator;
using IPathOperator_Base = R5T.F0002.IPathOperator;


namespace R5T.F0024
{
    public static class Instances
    {
        public static ICharacters Characters { get; } = Z0000.Characters.Instance;
        public static IEnumerationHelper EnumerationHelper { get; } = F0002.EnumerationHelper.Instance;
        public static IGlobalSectionGenerator GlobalSectionGenerator { get; } = F0024.GlobalSectionGenerator.Instance;
        public static IGlobalSectionNames GlobalSectionNames { get; } = F0024.GlobalSectionNames.Instance;
        public static IGlobalSectionOperator GlobalSectionOperator { get; } = F0024.GlobalSectionOperator.Instance;
        public static IGuidOperator GuidOperator { get; } = F0024.GuidOperator.Instance;
        public static IGuidOperator_Base GuidOperator_Base { get; } = F0000.GuidOperator.Instance;
        public static IOperations Operations { get; } = F0024.Operations.Instance;
        public static N000.IPathOperator PathOperator { get; } = N000.PathOperator.Instance;
        public static IPathOperator_Base PathOperator_Base { get; } = F0002.PathOperator.Instance;
        public static N000.IProjectFileOperator ProjectFileOperator { get; } = N000.ProjectFileOperator.Instance;
        public static IProjectTypeIdentities ProjectTypeIdentities { get; } = Z0009.ProjectTypeIdentities.Instance;
        public static ISolutionFileFormatVersionStrings SolutionFileFormatVersionStrings { get; } = F0024.SolutionFileFormatVersionStrings.Instance;
        public static Internal.ISolutionFileOperator SolutionFileOperator_Internal { get; } = Internal.SolutionFileOperator.Instance;
        public static ISolutionFileSerializer SolutionFileSerializer { get; } = F0024.SolutionFileSerializer.Instance;
        public static IVersionInformationGenerator VersionInformationGenerator { get; } = F0024.VersionInformationGenerator.Instance;
        public static IVersionInformationOperator VersionInformationOperator { get; } = F0024.VersionInformationOperator.Instance;
        public static IVisualStudioVersions VisualStudioVersions { get; } = F0024.VisualStudioVersions.Instance;
        public static IVisualStudioVersionStrings VisualStudioVersionStrings { get; } = F0024.VisualStudioVersionStrings.Instance;
    }
}