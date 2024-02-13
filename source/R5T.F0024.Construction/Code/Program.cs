using System;
using System.Threading.Tasks;


namespace R5T.F0024.Construction
{
    class Program
    {
        static async Task Main()
        {
            //Try.Instance.SolutionFileRoundTrip();

            //Program.RoundTripDeserializeThenSerializeSolutionFile();
            //Program.DeserializeSolutionFile();
            //Program.CreateSolutionFile();

            //Instances.SolutionFileExplorations.FindVersionBytesAtFileBeginning();

            //Instances.SolutionFileGeneratorDemonstrations.CreateNew();

            //Instances.SolutionFileOperatorDemonstrations.AddProject();
            //Instances.SolutionFileOperatorDemonstrations.AddAnotherProject();
            //Instances.SolutionFileOperatorDemonstrations.RemoveAnotherProject();
            //Instances.SolutionFileOperatorDemonstrations.AddSolutionFolder();
            //Instances.SolutionFileOperatorDemonstrations.AddProjectInSolutionFolder();
            //Instances.SolutionFileOperatorDemonstrations.RemoveProjectFromSolutionFolder();
            //Instances.SolutionFileOperatorDemonstrations.GetAndSetSolutionIdentity();
            //Instances.SolutionFileOperatorDemonstrations.ListProjectReferences();
            //await Instances.SolutionFileOperatorDemonstrations.ListAllRecursiveProjectReferences();
            await Instances.SolutionFileOperatorDemonstrations.Set_DefaultStartupProject();
        }

#pragma warning disable IDE0051 // Remove unused private members

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

        private static void CreateSolutionFile()
        {
            var solutionFilePath = @"C:\Temp\Solution.sln";

            Instances.SolutionFileGenerator.New_Synchronous(solutionFilePath);
        }
    }
}