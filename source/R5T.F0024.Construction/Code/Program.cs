using System;


namespace R5T.F0024.Construction
{
    class Program
    {
        static void Main()
        {
            //Program.DeserializeSolutionFile();
            Program.RoundTripDeserializeThenSerializeSolutionFile();

            //Instances.SolutionFileGeneratorDemonstrations.CreateNew();
        }

        private static void RoundTripDeserializeThenSerializeSolutionFile()
        {
            var initialSolutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0024\source\R5T.F0024.Construction.sln";
            var outputSolutionFilePath = @"C:\Temp\SolutionFile.sln";

            var solutionFile = Instances.SolutionFileOperator.Deserialize(initialSolutionFilePath);

            Instances.SolutionFileOperator.Serialize(
                outputSolutionFilePath,
                solutionFile);

            // Verify files are byte-equal.
            Instances.FileEqualityVerifier.VerifyFileByteLevelEquality(
                initialSolutionFilePath,
                outputSolutionFilePath);
        }

        private static void DeserializeSolutionFile()
        {
            var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0024\source\R5T.F0024.Construction.sln";

            var solutionFile = Instances.SolutionFileOperator.Deserialize(solutionFilePath);

            Console.WriteLine(solutionFile);
        }
    }
}