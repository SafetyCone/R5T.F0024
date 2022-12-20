using System;


namespace R5T.F0024
{
	public class SolutionFileStrings : ISolutionFileStrings
	{
		#region Infrastructure

	    public static ISolutionFileStrings Instance { get; } = new SolutionFileStrings();

	    private SolutionFileStrings()
	    {
        }

	    #endregion
	}
}