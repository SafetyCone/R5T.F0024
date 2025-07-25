using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface INestedProjectsGlobalSectionOperator : IFunctionalityMarker
    {
        public ProjectNesting[] GetGetNestingsForParent(
            NestedProjectsGlobalSection nestedProjectsGlobalSection,
            Guid parentProjectIdentity)
        {
            var nestingsForParent = nestedProjectsGlobalSection.ProjectNestings
                .Where(x => x.ParentProjectIdentity == parentProjectIdentity)
                .ToArray();

            return nestingsForParent;
        }

        public Guid[] GetChildProjectIdentitiesForParent(
            NestedProjectsGlobalSection nestedProjectsGlobalSection,
            Guid parentProjectIdentity)
        {
            var nestingsForParent = this.GetGetNestingsForParent(
                nestedProjectsGlobalSection,
                parentProjectIdentity);

            var childProjectIdentities = nestingsForParent
                .Select(xNesting => xNesting.ChildProjectIdentity)
                .ToArray();

            return childProjectIdentities;
        }

        public ProjectFileReference[] GetProjectFileReferencesInParent(
            NestedProjectsGlobalSection nestedProjectsGlobalSection,
            IEnumerable<ProjectFileReference> projectFileReferences,
            Guid parentProjectIdentity)
        {
            var childProjectIdentities = this.GetChildProjectIdentitiesForParent(
                nestedProjectsGlobalSection,
                parentProjectIdentity);

            var childProjectIdentitiesHash = new HashSet<Guid>(childProjectIdentities);

            var output = projectFileReferences
                .Where(xProjectFileReference => childProjectIdentitiesHash.Contains(xProjectFileReference.ProjectIdentity))
                .ToArray();

            return output;
        }
    }
}
