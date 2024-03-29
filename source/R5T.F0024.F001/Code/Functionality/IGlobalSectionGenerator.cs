using System;
using System.Collections.Generic;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface IGlobalSectionGenerator : IFunctionalityMarker
    {
        public ExtensibilityGlobalsGlobalSection ExtensibilityGlobals_GetDefault(Guid solutionIdentity)
        {
            var extensibilityGlobalsSection = new ExtensibilityGlobalsGlobalSection()
            {
                Name = Instances.GlobalSectionNames.ExtensibilityGlobals,
                PreOrPost = SolutionFileStrings.Instance.PostSolution,
                SolutionIdentity = solutionIdentity,
            };

            return extensibilityGlobalsSection;
        }

        public ExtensibilityGlobalsGlobalSection ExtensibilityGlobals_GetDefault()
        {
            var solutionIdentity = Instances.GuidOperator.New();

            var extensibilityGlobalsSection = this.ExtensibilityGlobals_GetDefault(solutionIdentity);
            return extensibilityGlobalsSection;
        }

        /// <summary>
        /// Gets the default <see cref="IGlobalSectionNames.SolutionProperties"/> global section.
        /// </summary>s
        public LinesBasedGlobalSection SolutionProperties_GetDefault()
        {
            var solutionPropertiesGloblaSection = new LinesBasedGlobalSection()
            {
                Name = Instances.GlobalSectionNames.SolutionProperties,
                PreOrPost = SolutionFileStrings.Instance.PreSolution,
                Lines = new List<string>()
                {
                    "HideSolutionNode = FALSE",
                },
            };

            return solutionPropertiesGloblaSection;
        }
    }
}
