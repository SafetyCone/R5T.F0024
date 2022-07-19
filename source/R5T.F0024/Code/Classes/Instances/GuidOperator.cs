using System;


namespace R5T.F0024
{
	public class GuidOperator : IGuidOperator
	{
		#region Infrastructure

	    public static GuidOperator Instance { get; } = new();

	    private GuidOperator()
	    {
        }

	    #endregion
	}
}