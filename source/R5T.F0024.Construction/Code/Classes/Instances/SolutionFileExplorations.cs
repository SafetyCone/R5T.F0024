using System;


namespace R5T.F0024.Construction
{
	public class SolutionFileExplorations : ISolutionFileExplorations
	{
		#region Infrastructure

	    public static SolutionFileExplorations Instance { get; } = new();

	    private SolutionFileExplorations()
	    {
        }

	    #endregion
	}
}