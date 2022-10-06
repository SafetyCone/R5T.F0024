using System;


namespace R5T.F0024
{
	public class SolutionFileFormatVersionStrings : ISolutionFileFormatVersionStrings
	{
		#region Infrastructure

	    public static SolutionFileFormatVersionStrings Instance { get; } = new();

	    private SolutionFileFormatVersionStrings()
	    {
        }

	    #endregion
	}
}