using System;


namespace R5T.F0024
{
	public class SolutionFileOperator : ISolutionFileOperator
	{
		#region Infrastructure

	    public static SolutionFileOperator Instance { get; } = new();

	    private SolutionFileOperator()
	    {
        }

	    #endregion
	}


	namespace Internal
    {
		public class SolutionFileOperator : ISolutionFileOperator
		{
			#region Infrastructure

			public static SolutionFileOperator Instance { get; } = new();

			private SolutionFileOperator()
			{
			}

			#endregion
		}
	}
}