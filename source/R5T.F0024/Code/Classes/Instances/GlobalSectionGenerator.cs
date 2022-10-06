using System;


namespace R5T.F0024
{
	public class GlobalSectionGenerator : IGlobalSectionGenerator
	{
		#region Infrastructure

	    public static GlobalSectionGenerator Instance { get; } = new();

	    private GlobalSectionGenerator()
	    {
        }

	    #endregion
	}
}