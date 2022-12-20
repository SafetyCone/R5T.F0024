using System;

using R5T.F0024;
using R5T.F0024.T001;


namespace System
{
    public static class SectionExtensions
    {
        public static TDestintationSection FillFrom<TDestintationSection, TSourceSection>(this TDestintationSection destination, TSourceSection source)
            where TDestintationSection : SectionBase
            where TSourceSection : ISection
        {
            SectionOperator.Instance.FillFrom(destination, source);

            return destination;
        }
    }
}
