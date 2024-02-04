using System;
using System.IO;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024.F001.Implementations
{
    [FunctionalityMarker]
    public partial interface ISolutionFileGenerator : IFunctionalityMarker
    {
#pragma warning disable IDE1006 // Naming Styles
        private F001.ISolutionFileGenerator _Main => F001.SolutionFileGenerator.Instance;
#pragma warning restore IDE1006 // Naming Styles



        /// <summary>
        /// Creates a new, empty solution file. (VS 2022)
        /// </summary>
        public void New_UsingTextTemplate_Synchronous(string solutionFilePath)
        {
            var solutionGuid = Instances.GuidOperator.New();

            var text =
$@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.2.32630.192
MinimumVisualStudioVersion = 10.0.40219.1
Global
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {Instances.GuidOperator.ToString_ForSolutionFile(solutionGuid)}
	EndGlobalSection
EndGlobal
";

            Instances.FileOperator.Write_Text_Synchronous(
                solutionFilePath,
                text);
        }

        /// <inheritdoc cref="F001.ISolutionFileGenerator.New()"/>
        public void New_UsingSolutionFileObject_Synchronous(string solutionFilePath)
        {
            var solutionFile = _Main.New();

            Instances.SolutionFileSerializer.Serialize_Synchronous(
                solutionFilePath,
                solutionFile);
        }
    }
}
