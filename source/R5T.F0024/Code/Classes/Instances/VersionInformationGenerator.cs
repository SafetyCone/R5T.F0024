using System;


namespace R5T.F0024
{
	public class VersionInformationGenerator : IVersionInformationGenerator
	{
		#region Infrastructure

	    public static VersionInformationGenerator Instance { get; } = new();

	    private VersionInformationGenerator()
	    {
        }

	    #endregion
	}
}