using System;


namespace R5T.F0024.T001
{
    /// <summary>
    /// Defines a common abstraction for Visual Studio solution file global sections.
    /// </summary>
    public interface ISection
    {
        string Name { get; }
        public string PreOrPost { get; set; }
    }
}
