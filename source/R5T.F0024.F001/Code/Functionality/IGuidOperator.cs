using System;

using R5T.T0132;

using GuidDocumentation = R5T.Y0006.Documentation.For_Guid;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface IGuidOperator : IFunctionalityMarker
    {
#pragma warning disable IDE1006 // Naming Styles
        public L0066.IGuidOperator _Base => L0066.GuidOperator.Instance;
#pragma warning restore IDE1006 // Naming Styles


        public Guid New()
        {
            var output = _Base.New();
            return output;
        }

        /// <summary>
        /// Uses the braced (B) uppercase Guid format.
        /// <inheritdoc cref="GuidDocumentation.B_Uppercase_Format"/>
        /// </summary>
        public string ToString_ForSolutionFile(Guid guid)
        {
            var output = _Base.ToString_B_Uppercase_Format(guid);
            return output;
        }

        public Guid Parse_ForSolutionFile(string guidString)
        {
            var output = _Base.Parse(guidString);
            return output;
        }
    }
}
