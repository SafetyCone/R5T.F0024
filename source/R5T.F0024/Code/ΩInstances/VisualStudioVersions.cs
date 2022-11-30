using System;


namespace R5T.F0024
{
	public class VisualStudioVersions : IVisualStudioVersions
	{
		#region Infrastructure

	    public static IVisualStudioVersions Instance { get; } = new VisualStudioVersions();

	    private VisualStudioVersions()
	    {
        }

	    #endregion
	}
}