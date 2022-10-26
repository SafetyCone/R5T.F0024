using System;

using R5T.T0132;


namespace R5T.F0024.T001
{
	[FunctionalityMarker]
	public partial interface IProjectFileReferenceOperator : IFunctionalityMarker
	{
		public bool IdentityBasedEquals(
			ProjectFileReference x,
			ProjectFileReference y)
        {
			var areEqualByIdentity = x.ProjectIdentity == y.ProjectIdentity;
			return areEqualByIdentity;
        }

		public int IdentityBasedHashCode(ProjectFileReference obj)
        {
			var hashCode = obj.ProjectIdentity.GetHashCode();
			return hashCode;
        }
	}
}