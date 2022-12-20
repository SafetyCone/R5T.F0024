using System;

using R5T.F0024.T001;
using R5T.T0132;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface ISectionOperator : IFunctionalityMarker
	{
		public void FillFrom<TDestintationSection, TSourceSection>(TDestintationSection destination, TSourceSection source)
			where TDestintationSection : SectionBase
			where TSourceSection : ISection
		{
			destination.Name = source.Name;
			destination.PreOrPost = source.PreOrPost;
		}
	}
}