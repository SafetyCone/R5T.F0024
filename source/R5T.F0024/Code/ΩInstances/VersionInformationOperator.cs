using System;


namespace R5T.F0024
{
	public class VersionInformationOperator : IVersionInformationOperator
	{
		#region Infrastructure

	    public static IVersionInformationOperator Instance { get; } = new VersionInformationOperator();

	    private VersionInformationOperator()
	    {
        }

	    #endregion
	}
}