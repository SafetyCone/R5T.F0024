using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.F0024.V000
{
    [TestClass]
    public class SolutionFileGeneratorTests
    {
        [TestMethod]
        public void CreateSolutionFile()
        {
            var solutionFilePath = Instances.SolutionFilePaths.ForTestingOutput;

            var expectedSolutionFilePath = Instances.ExampleFilePaths.ExampleSolution;

            Instances.SolutionFileGenerator.CreateNew(solutionFilePath);

            Instances.FileEqualityVerifier.VerifyFileByteLevelEquality(
                solutionFilePath,
                expectedSolutionFilePath);
        }
    }
}
