using System;


namespace R5T.F0024
{
	public class SolutionFileGenerator : ISolutionFileGenerator
	{
		#region Infrastructure

	    public static ISolutionFileGenerator Instance { get; } = new SolutionFileGenerator();

	    private SolutionFileGenerator()
	    {
        }

	    #endregion
	}
}