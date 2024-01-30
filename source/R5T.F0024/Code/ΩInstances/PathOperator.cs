using System;


namespace R5T.F0024
{
	public class PathOperator : IPathOperator
	{
		#region Infrastructure

	    public static IPathOperator Instance { get; } = new PathOperator();

	    private PathOperator()
	    {
        }

	    #endregion
	}
}