using System;


namespace R5T.F0024.F001
{
    public class SolutionFileOperator : ISolutionFileOperator
    {
        #region Infrastructure

        public static ISolutionFileOperator Instance { get; } = new SolutionFileOperator();


        private SolutionFileOperator()
        {
        }

        #endregion
    }
}


namespace R5T.F0024.F001.Internal
{
    public class SolutionFileOperator : ISolutionFileOperator
    {
        #region Infrastructure

        public static ISolutionFileOperator Instance { get; } = new SolutionFileOperator();


        private SolutionFileOperator()
        {
        }

        #endregion
    }
}
