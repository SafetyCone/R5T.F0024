using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    /// <summary>
    /// A section with no structure other than a list of lines.
    /// </summary>
    public class LinesBasedSection : SectionBase
    {
        public List<string> Lines { get; set; }
    }
}
