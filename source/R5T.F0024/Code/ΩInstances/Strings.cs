using System;


namespace R5T.F0024
{
	public class Strings : IStrings
	{
		#region Infrastructure

	    public static IStrings Instance { get; } = new Strings();

	    private Strings()
	    {
        }

	    #endregion
	}
}