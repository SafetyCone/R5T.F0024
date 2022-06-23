using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    public class SolutionFile
    {
        public VersionInformation VersionInformation { get; set; }
        public List<ProjectFileReference> ProjectFileReferences { get; set; }
        public List<IGlobalSection> GlobalSections { get; set; }
    }
}
