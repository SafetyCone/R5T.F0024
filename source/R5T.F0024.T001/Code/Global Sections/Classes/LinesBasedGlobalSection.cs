using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    /// <summary>
    /// A global section with no structure other than a list of lines.
    /// </summary>
    public class LinesBasedGlobalSection : GlobalSectionBase
    {
        public List<string> Lines { get; set; }
    }
}
