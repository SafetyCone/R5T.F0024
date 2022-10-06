using System;


namespace R5T.F0024
{
	public class Operations : IOperations
	{
		#region Infrastructure

	    public static Operations Instance { get; } = new();

	    private Operations()
	    {
        }

	    #endregion
	}
}