using System;

using IGuidOperator_Base = R5T.F0000.IGuidOperator;


namespace R5T.F0024
{
    public static class Instances
    {
        public static IGuidOperator GuidOperator { get; } = F0024.GuidOperator.Instance;
        public static IGuidOperator_Base GuidOperator_Base { get; } = F0000.GuidOperator.Instance;
    }
}