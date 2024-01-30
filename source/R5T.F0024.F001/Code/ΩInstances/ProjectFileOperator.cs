using System;


namespace R5T.F0024.F001
{
    public class ProjectFileOperator : IProjectFileOperator
    {
        #region Infrastructure

        public static IProjectFileOperator Instance { get; } = new ProjectFileOperator();


        private ProjectFileOperator()
        {
        }

        #endregion
    }
}
