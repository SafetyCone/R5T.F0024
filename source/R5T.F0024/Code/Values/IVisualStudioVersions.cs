using System;

using R5T.T0131;


namespace R5T.F0024
{
	[ValuesMarker]
	public partial interface IVisualStudioVersions : IValuesMarker
	{
		public Version MinimumVersion_Default => new(10, 0, 40219, 1);

		public Version Version_16 => new(16, 0, 32002, 261);
		public Version Version_17 => new(17, 2, 32630, 192);

		public Version VisualStudio_2019 => this.Version_16;
		public Version VisualStudio_2022 => this.Version_17;
	}
}