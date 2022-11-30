using System;


namespace R5T.F0024
{
	public class SolutionOperator : ISolutionOperator
	{
		#region Infrastructure

	    public static ISolutionOperator Instance { get; } = new SolutionOperator();

	    private SolutionOperator()
	    {
        }

	    #endregion
	}
}