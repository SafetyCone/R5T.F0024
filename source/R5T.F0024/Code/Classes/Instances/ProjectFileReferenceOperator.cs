using System;


namespace R5T.F0024
{
	public class ProjectFileReferenceOperator : IProjectFileReferenceOperator
	{
		#region Infrastructure

	    public static IProjectFileReferenceOperator Instance { get; } = new ProjectFileReferenceOperator();

	    private ProjectFileReferenceOperator()
	    {
        }

	    #endregion
	}
}