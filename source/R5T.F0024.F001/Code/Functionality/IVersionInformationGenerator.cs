using System;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface IVersionInformationGenerator : IFunctionalityMarker
    {
        public VersionInformation Get2019_Default()
        {
            var versionInformation = new VersionInformation()
            {
                FormatInformation = Instances.VersionInformationOperator.GetSolutionFileFormatInformation(
                    Instances.SolutionFileFormatVersionStrings.Version_12_00),
                VersionDescription = Instances.VersionInformationOperator.GetVisualStudioVersionDescription(
                    Instances.VisualStudioVersionStrings.Version_16),
                Version = Instances.VersionInformationOperator.GetVisualStudioVersionLine(
                    Instances.VisualStudioVersions.VisualStudio_2019),
                MinimumVersion = Instances.VersionInformationOperator.GetMinimumVisualStudioVersionLine(
                    Instances.VisualStudioVersions.MinimumVersion_Default),
            };

            return versionInformation;
        }

        public VersionInformation Get2022_Default()
        {
            var versionInformation = new VersionInformation()
            {
                FormatInformation = Instances.VersionInformationOperator.GetSolutionFileFormatInformation(
                    Instances.SolutionFileFormatVersionStrings.Version_12_00),
                VersionDescription = Instances.VersionInformationOperator.GetVisualStudioVersionDescription(
                    Instances.VisualStudioVersionStrings.Version_17),
                Version = Instances.VersionInformationOperator.GetVisualStudioVersionLine(
                    Instances.VisualStudioVersions.VisualStudio_2022),
                MinimumVersion = Instances.VersionInformationOperator.GetMinimumVisualStudioVersionLine(
                    Instances.VisualStudioVersions.MinimumVersion_Default),
            };

            return versionInformation;
        }
    }
}
