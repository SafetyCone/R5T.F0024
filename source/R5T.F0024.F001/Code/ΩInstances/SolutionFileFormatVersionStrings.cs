using System;


namespace R5T.F0024.F001
{
    public class SolutionFileFormatVersionStrings : ISolutionFileFormatVersionStrings
    {
        #region Infrastructure

        public static ISolutionFileFormatVersionStrings Instance { get; } = new SolutionFileFormatVersionStrings();


        private SolutionFileFormatVersionStrings()
        {
        }

        #endregion
    }
}
