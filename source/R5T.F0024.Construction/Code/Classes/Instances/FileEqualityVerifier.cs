using System;


namespace R5T.F0024.Construction
{
	public class FileEqualityVerifier : IFileEqualityVerifier
	{
		#region Infrastructure

	    public static FileEqualityVerifier Instance { get; } = new();

	    private FileEqualityVerifier()
	    {
        }

	    #endregion
	}
}