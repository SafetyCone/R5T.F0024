using System;


namespace R5T.F0024
{
	public class VisualStudioVersionStrings : IVisualStudioVersionStrings
	{
		#region Infrastructure

	    public static IVisualStudioVersionStrings Instance { get; } = new VisualStudioVersionStrings();

	    private VisualStudioVersionStrings()
	    {
        }

	    #endregion
	}
}