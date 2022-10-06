using System;


namespace R5T.F0024
{
	public class VisualStudioVersions : IVisualStudioVersions
	{
		#region Infrastructure

	    public static VisualStudioVersions Instance { get; } = new();

	    private VisualStudioVersions()
	    {
        }

	    #endregion
	}
}