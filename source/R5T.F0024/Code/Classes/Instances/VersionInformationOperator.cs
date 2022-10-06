using System;


namespace R5T.F0024
{
	public class VersionInformationOperator : IVersionInformationOperator
	{
		#region Infrastructure

	    public static VersionInformationOperator Instance { get; } = new();

	    private VersionInformationOperator()
	    {
        }

	    #endregion
	}
}