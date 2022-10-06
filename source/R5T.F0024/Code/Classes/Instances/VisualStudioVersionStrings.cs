using System;


namespace R5T.F0024
{
	public class VisualStudioVersionStrings : IVisualStudioVersionStrings
	{
		#region Infrastructure

	    public static VisualStudioVersionStrings Instance { get; } = new();

	    private VisualStudioVersionStrings()
	    {
        }

	    #endregion
	}
}