using System;


namespace R5T.F0024
{
	public class GlobalSectionNames : IGlobalSectionNames
	{
		#region Infrastructure

	    public static IGlobalSectionNames Instance { get; } = new GlobalSectionNames();

	    private GlobalSectionNames()
	    {
        }

	    #endregion
	}
}