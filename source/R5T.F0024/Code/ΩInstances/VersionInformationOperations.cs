using System;


namespace R5T.F0024
{
	public class VersionInformationOperations : IVersionInformationOperations
	{
		#region Infrastructure

	    public static IVersionInformationOperations Instance { get; } = new VersionInformationOperations();

	    private VersionInformationOperations()
	    {
        }

	    #endregion
	}
}