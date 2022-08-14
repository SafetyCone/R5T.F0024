using System;


namespace R5T.F0024
{
	public class GlobalSectionNames : IGlobalSectionNames
	{
		#region Infrastructure

	    public static GlobalSectionNames Instance { get; } = new();

	    private GlobalSectionNames()
	    {
        }

	    #endregion
	}
}