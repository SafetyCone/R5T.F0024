using System;


namespace R5T.F0024
{
	public class SolutionFileSerializer : ISolutionFileSerializer
	{
		#region Infrastructure

	    public static SolutionFileSerializer Instance { get; } = new();

	    private SolutionFileSerializer()
	    {
        }

	    #endregion
	}
}