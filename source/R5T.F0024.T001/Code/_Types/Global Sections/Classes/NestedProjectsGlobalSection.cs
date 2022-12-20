using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    public class NestedProjectsGlobalSection : SectionBase, IGlobalSection
    {
        public List<ProjectNesting> ProjectNestings { get; } = new List<ProjectNesting>();
    }
}
