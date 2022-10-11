using System;


namespace R5T.F0024
{
	public class GlobalSectionGenerator : IGlobalSectionGenerator
	{
		#region Infrastructure

	    public static IGlobalSectionGenerator Instance { get; } = new GlobalSectionGenerator();

	    private GlobalSectionGenerator()
	    {
        }

	    #endregion
	}
}