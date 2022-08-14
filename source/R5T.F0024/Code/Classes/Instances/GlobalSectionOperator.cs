using System;


namespace R5T.F0024
{
	public class GlobalSectionOperator : IGlobalSectionOperator
	{
		#region Infrastructure

	    public static GlobalSectionOperator Instance { get; } = new();

	    private GlobalSectionOperator()
	    {
        }

	    #endregion
	}


	namespace Internal
    {
		public class GlobalSectionOperator : IGlobalSectionOperator
		{
			#region Infrastructure

			public static GlobalSectionOperator Instance { get; } = new();

			private GlobalSectionOperator()
			{
			}

			#endregion
		}
	}
}