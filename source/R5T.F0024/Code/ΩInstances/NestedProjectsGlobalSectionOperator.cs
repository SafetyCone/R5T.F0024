using System;


namespace R5T.F0024
{
	public class NestedProjectsGlobalSectionOperator : INestedProjectsGlobalSectionOperator
	{
		#region Infrastructure

	    public static INestedProjectsGlobalSectionOperator Instance { get; } = new NestedProjectsGlobalSectionOperator();

	    private NestedProjectsGlobalSectionOperator()
	    {
        }

	    #endregion
	}
}