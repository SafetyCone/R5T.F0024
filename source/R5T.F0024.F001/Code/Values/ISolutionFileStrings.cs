using System;

using R5T.T0131;


namespace R5T.F0024.F001
{
    [ValuesMarker]
    public partial interface ISolutionFileStrings : IValuesMarker
    {
        public string ActiveCfg => "ActiveCfg";
        public string Build_0 => "Build.0";

        public string AnyCpu => "Any CPU";
#pragma warning disable IDE1006 // Naming Styles
        public string x64 => "x64";
        public string x86 => "x86";
#pragma warning restore IDE1006 // Naming Styles

        public string SolutionGuid => "SolutionGuid";

        public string Global => "Global";
        public string EndGlobal => "EndGlobal";

        public string GlobalSection => "GlobalSection";
        public string EndGlobalSection => "EndGlobalSection";

        public string Project => "Project";
        public string EndProject => "EndProject";

        public string ProjectSection => "ProjectSection";
        public string EndProjectSection => "EndProjectSection";

        public string PreSolution => "preSolution";
        public string PostSolution => "postSolution";
        public string PreProject => "preProject";
        public string PostProject => "postProject";
    }
}
