using System;


namespace R5T.F0024
{
	public class SolutionFileOperator : ISolutionFileOperator
	{
		#region Infrastructure

	    public static ISolutionFileOperator Instance { get; } = new SolutionFileOperator();

	    private SolutionFileOperator()
	    {
        }

	    #endregion
	}
}