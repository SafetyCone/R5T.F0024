using System;


namespace R5T.F0024
{
	public class SolutionFileGenerator : ISolutionFileGenerator
	{
		#region Infrastructure

	    public static SolutionFileGenerator Instance { get; } = new();

	    private SolutionFileGenerator()
	    {
        }

	    #endregion
	}
}