using System;


namespace R5T.F0024
{
	public class GlobalSectionOperator : IGlobalSectionOperator
	{
		#region Infrastructure

	    public static IGlobalSectionOperator Instance { get; } = new GlobalSectionOperator();

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

			public static IGlobalSectionOperator Instance { get; } = new GlobalSectionOperator();

			private GlobalSectionOperator()
			{
			}

			#endregion
		}
	}
}