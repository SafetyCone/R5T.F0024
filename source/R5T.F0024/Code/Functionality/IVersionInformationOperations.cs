using System;

using R5T.F0024.T001;
using R5T.T0132;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface IVersionInformationOperations : IFunctionalityMarker
	{
		/// <summary>
		/// Creates a version information instance with unspecified values.
		/// This is useless for creating actual functional Visual Studio solution files, but useful for creating an initial solution file object that can actualy be serialized.
		/// </summary>
		public VersionInformation Create_Unspecified()
		{
			var versionInformation = new VersionInformation
			{
				FormatInformation = Strings.Instance.FormatInformation_Unspecified,
				VersionDescription = Strings.Instance.VersionDescription_Unspecified,
				Version = Strings.Instance.VisualStudioVersion_Unspecified,
				MinimumVersion = Strings.Instance.MinimumVisualStudioVersion_Unspecified,
			};

			return versionInformation;
		}

        public VersionInformation Create_VS2022()
		{
			var output = VersionInformationGenerator.Instance.Get2022_Default();
			return output;
		}

        public VersionInformation Create_VS2019()
        {
            var output = VersionInformationGenerator.Instance.Get2019_Default();
            return output;
        }

		/// <summary>
		/// Chooses <see cref="Create_VS2022"/> as the standard.
		/// </summary>
		public VersionInformation Create_Standard()
		{
			var output = this.Create_VS2022();
			return output;
		}

		/// <summary>
		/// Quality-of-life overload for <see cref="Create_Standard"/>.
		/// </summary>
		public VersionInformation New()
		{
			var output = this.Create_Standard();
			return output;
		}
    }
}