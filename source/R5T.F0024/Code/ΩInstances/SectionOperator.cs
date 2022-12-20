using System;


namespace R5T.F0024
{
	public class SectionOperator : ISectionOperator
	{
		#region Infrastructure

	    public static ISectionOperator Instance { get; } = new SectionOperator();

	    private SectionOperator()
	    {
        }

	    #endregion
	}
}