using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    public class ProjectFileReferenceIdentityBasedEqualityComparer : IEqualityComparer<ProjectFileReference>
    {
        #region Static

        public static ProjectFileReferenceIdentityBasedEqualityComparer Instance { get; } = new ProjectFileReferenceIdentityBasedEqualityComparer();

        #endregion


        public bool Equals(ProjectFileReference x, ProjectFileReference y)
        {
            var areEqual = Instances.ProjectFileReferenceOperator.IdentityBasedEquals(x, y);
            return areEqual;
        }

        public int GetHashCode(ProjectFileReference obj)
        {
            var hashCode = Instances.ProjectFileReferenceOperator.IdentityBasedHashCode(obj);
            return hashCode;
        }
    }
}
