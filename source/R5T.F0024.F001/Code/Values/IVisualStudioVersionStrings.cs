using System;

using R5T.T0131;


namespace R5T.F0024.F001
{
    [ValuesMarker]
    public partial interface IVisualStudioVersionStrings : IValuesMarker
    {
        public const string Version_15_Constant = "15";
        public const string Version_16_Constant = "16";
        public const string Version_17_Constant = "17";

        public string Version_15 => IVisualStudioVersionStrings.Version_15_Constant;
        public string Version_16 => IVisualStudioVersionStrings.Version_16_Constant;
        public string Version_17 => IVisualStudioVersionStrings.Version_17_Constant;
    }
}
