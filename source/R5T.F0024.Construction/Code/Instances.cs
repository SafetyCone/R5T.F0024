using System;


namespace R5T.F0024.Construction
{
    public static class Instances
    {
        public static ISolutionFileGeneratorDemonstrations SolutionFileGeneratorDemonstrations { get; } = Construction.SolutionFileGeneratorDemonstrations.Instance;

        public static ISolutionFileGenerator SolutionFileGenerator { get; } = F0024.SolutionFileGenerator.Instance;
    }
}