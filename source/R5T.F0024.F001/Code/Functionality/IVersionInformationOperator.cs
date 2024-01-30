using System;

using R5T.T0132;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface IVersionInformationOperator : IFunctionalityMarker
    {
        public string GetMinimumVisualStudioVersionLine(Version minimumVisualStudioVersion)
        {
            var output = $"MinimumVisualStudioVersion = {minimumVisualStudioVersion}";
            return output;
        }

        /// <summary>
        /// Example: "Microsoft Visual Studio Solution File, Format Version 12.00"
        /// </summary>
        public string GetSolutionFileFormatInformation(string solutionFileFormatVersionString)
        {
            var formatInformation = $"Microsoft Visual Studio Solution File, Format Version {solutionFileFormatVersionString}";
            return formatInformation;
        }

        public string GetVisualStudioVersionDescription(string visualStudioVersionString)
        {
            var output = $"# Visual Studio Version {visualStudioVersionString}";
            return output;
        }

        public string GetVisualStudioVersionLine(Version visualStudioVersion)
        {
            var output = $"VisualStudioVersion = {visualStudioVersion}";
            return output;
        }
    }
}
