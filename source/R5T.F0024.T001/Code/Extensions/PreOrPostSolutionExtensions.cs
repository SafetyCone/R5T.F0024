using System;

using R5T.F0024.T001;


namespace System
{
    public static class PreOrPostSolutionExtensions
    {
        public static string ToString_ForSolutionFile(this PreOrPostSolution preOrPostSolution)
        {
            var output = preOrPostSolution switch
            {
                PreOrPostSolution.PostSolution => "postSolution",
                PreOrPostSolution.PreSolution => "preSolution",
                _ => throw R5T.F0000.EnumerationOperator.Instance.SwitchDefaultCaseException(preOrPostSolution),
            };

            return output;
        }
    }
}
