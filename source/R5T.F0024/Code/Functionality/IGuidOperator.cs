using System;

using R5T.T0132;

using GuidDocumentation = R5T.Y0000.Documentation.ForGuid;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface IGuidOperator : IFunctionalityMarker
	{
		public Guid New()
        {
			var output = Instances.GuidOperator_Base.New();
			return output;
        }

		/// <summary>
		/// Uses the braced (B) uppercase Guid format.
		/// <inheritdoc cref="GuidDocumentation.B_Uppercase_Format"/>
		/// </summary>
		public string ToString_ForSolutionFile(Guid guid)
        {
			var output = Instances.GuidOperator_Base.ToString_B_Uppercase_Format(guid);
			return output;
        }

		public Guid Parse_ForSolutionFile(string guidString)
        {
			var output = Instances.GuidOperator_Base.Parse(guidString);
			return output;
        }
	}
}