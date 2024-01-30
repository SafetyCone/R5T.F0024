using System;


namespace R5T.F0024.F001
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
