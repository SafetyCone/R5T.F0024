using System;

using R5T.T0131;


namespace R5T.F0024
{
	[ValuesMarker]
	public partial interface ISolutionFileFormatVersionStrings : IValuesMarker
	{
		public string Current => this.Version_12_00;

		public string Version_12_00 => "12.00";
	}
}