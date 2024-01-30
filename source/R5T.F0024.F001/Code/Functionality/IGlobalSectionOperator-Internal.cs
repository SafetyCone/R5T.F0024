using System;
using System.Linq;

using R5T.L0089.T000;
using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024.F001.Internal
{
    [FunctionalityMarker]
    public partial interface IGlobalSectionOperator : IFunctionalityMarker
    {
        public T Acquire_GlobalSection<T>(
            SolutionFile solutionFile,
            string globalSectionName,
            Func<T> constructor)
            where T : IGlobalSection
        {
            var hasGlobalSection = this.Has_GlobalSection<T>(solutionFile, globalSectionName);
            if (!hasGlobalSection)
            {
                var globalSection = constructor();

                Instances.SolutionFileOperator_Internal.AddGlobalSection(solutionFile, globalSection);

                return globalSection;
            }

            return hasGlobalSection;
        }

        public WasFound<T> Has_GlobalSection<T>(
            SolutionFile solutionFile,
            string globalSectionName)
            where T : ISection
        {
            var outputOrDefault = solutionFile.GlobalSections
                .Where(x => x.Name == globalSectionName)
                .Cast<T>()
                .FirstOrDefault(); // Use more robust first-or-default. There should only be one section, but why enforce it?

            var output = WasFound.From(outputOrDefault);
            return output;
        }
    }
}
