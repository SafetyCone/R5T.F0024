using System;

using R5T.F0024.T001;

using Instances = R5T.F0024.F001.Instances;


namespace System
{
    public static class SectionExtensions
    {
        public static TDestintationSection FillFrom<TDestintationSection, TSourceSection>(this TDestintationSection destination, TSourceSection source)
            where TDestintationSection : SectionBase
            where TSourceSection : ISection
        {
            Instances.SectionOperator.FillFrom(destination, source);

            return destination;
        }
    }
}
