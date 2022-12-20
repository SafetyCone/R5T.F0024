using System;


namespace R5T.F0024
{
	public class SolutionFileSerializer : ISolutionFileSerializer
	{
		#region Infrastructure

	    public static ISolutionFileSerializer Instance { get; } = new SolutionFileSerializer();

	    private SolutionFileSerializer()
	    {
        }

	    #endregion
	}
}


namespace R5T.F0024.Internal
{
    public class SolutionFileSerializer : ISolutionFileSerializer
    {
        #region Infrastructure

        public static ISolutionFileSerializer Instance { get; } = new SolutionFileSerializer();

        private SolutionFileSerializer()
        {
        }

        #endregion
    }
}