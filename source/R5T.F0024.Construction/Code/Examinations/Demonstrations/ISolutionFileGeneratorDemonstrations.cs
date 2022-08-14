using System;

using R5T.T0141;


namespace R5T.F0024.Construction
{
	[DemonstrationsMarker]
	public partial interface ISolutionFileGeneratorDemonstrations : IDemonstrationsMarker
	{
		public void CreateNew()
        {
			var solutionFilePath =
                //@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Z0008\source\R5T.Z0008\Files\ExampleSolution.sln"
                @"C:\Temp\Solution.sln"
                ;

			Instances.SolutionFileGenerator.CreateNew(solutionFilePath);
        }
	}
}