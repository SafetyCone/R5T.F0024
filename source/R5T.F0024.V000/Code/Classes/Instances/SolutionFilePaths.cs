using System;


namespace R5T.F0024.V000
{
	public class SolutionFilePaths : ISolutionFilePaths
	{
		#region Infrastructure

	    public static SolutionFilePaths Instance { get; } = new();

	    private SolutionFilePaths()
	    {
        }

	    #endregion
	}
}