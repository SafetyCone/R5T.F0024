using System;


namespace R5T.F0024
{
	public class VersionInformationGenerator : IVersionInformationGenerator
	{
		#region Infrastructure

	    public static IVersionInformationGenerator Instance { get; } = new VersionInformationGenerator();

	    private VersionInformationGenerator()
	    {
        }

	    #endregion
	}
}