using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0141;


namespace R5T.F0024.Construction
{
	[DemonstrationsMarker]
	public partial interface ISolutionFileOperatorDemonstrations : IDemonstrationsMarker
	{
		public void ListMissingProjectReferences()
		{
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0016\source\R5T.F0016.Construction.sln";

			var projectReferenceFilePaths = Instances.SolutionFileOperator.Get_ProjectReferenceFilePaths(solutionFilePath);

			var projectReferencesCount = projectReferenceFilePaths.Length;

			var alphabeticalProjectReferenceFilePaths = projectReferenceFilePaths.OrderAlphabetically();

			var lines = Instances.EnumerableOperator.From($"For solution:\n\n\t{solutionFilePath}\n")
				.Append($"({projectReferencesCount}) Project References:\n")
				.Append(alphabeticalProjectReferenceFilePaths)
				;

			Instances.FileOperator.WriteLines(
				Instances.FilePaths.OutputTextFilePath,
				lines);

			Instances.NotepadPlusPlusOperator.Open(
				Instances.FilePaths.OutputTextFilePath);
		}

		public async Task ListAllRecursiveProjectReferences()
        {
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0024\source\R5T.F0024.Construction.sln";

			var allRecursiveProjectReferences = await Instances.SolutionOperations.GetAllRecursiveProjectReferences(solutionFilePath);

			var lines = Instances.TextOutputGenerator.GetAllRecursiveSolutionProjectReferencesOutputLines(
				solutionFilePath,
				allRecursiveProjectReferences);

			Instances.Operations.WriteLinesAndOpenInNotepadPlusPlus(
				lines);
		}

		public void ListProjectReferences()
        {
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0016\source\R5T.F0016.Construction.sln";

			var projectReferenceFilePaths = Instances.SolutionFileOperator.Get_ProjectReferenceFilePaths(solutionFilePath);

			var projectReferencesCount = projectReferenceFilePaths.Length;

			var alphabeticalProjectReferenceFilePaths = projectReferenceFilePaths.OrderAlphabetically();

			var lines = Instances.EnumerableOperator.From($"For solution:\n\n\t{solutionFilePath}\n")
				.Append($"({projectReferencesCount}) Project References:\n")
				.Append(alphabeticalProjectReferenceFilePaths)
				;

			Instances.FileOperator.WriteLines(
				Instances.FilePaths.OutputTextFilePath,
				lines);

			Instances.NotepadPlusPlusOperator.Open(
				Instances.FilePaths.OutputTextFilePath);
        }

		public void GetAndSetSolutionIdentity()
        {
			var originalSolutionFilePath = @"C:\Temp\Solution.sln";
			var modifiedSolutioFilePath = @"C:\Temp\Solution-IdentitySet.sln";

			Instances.FileSystemOperator.CopyFile(originalSolutionFilePath, modifiedSolutioFilePath);

			var solutionFile = Instances.SolutionFileOperator.Deserialize(modifiedSolutioFilePath);

			var originalSolutionIdentity = Instances.SolutionFileOperator.Get_SolutionIdentity(solutionFile);

			Console.WriteLine($"Original solution identity: {F0024.Instances.GuidOperator.ToString_ForSolutionFile(originalSolutionIdentity)}");

			var random = Instances.RandomOperator.WithSeed("SolutionIdentity");

			var newSolutionIdentity = Instances.GuidOperator.New(random);

			Instances.SolutionFileOperator.Set_SolutionIdentity(
				solutionFile,
				newSolutionIdentity);

			Instances.SolutionFileOperator.Serialize(
				modifiedSolutioFilePath,
				solutionFile);
		}

		public void RemoveProjectFromSolutionFolder()
		{
			var originalSolutionFilePath = @"C:\Temp\Solution-WithProjectInSolutionFolder.sln";
			var modifiedSolutioFilePath = @"C:\Temp\Solution-WithoutProjectInSolutionFolder.sln";

			Instances.FileSystemOperator.CopyFile(originalSolutionFilePath, modifiedSolutioFilePath);

			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Magyar\source\R5T.Magyar\R5T.Magyar.csproj";

			Instances.SolutionFileOperator.RemoveProject(
				modifiedSolutioFilePath,
				projectFilePath);
		}

		public void AddProjectInSolutionFolder()
        {
			var originalSolutionFilePath = @"C:\Temp\Solution-WithSolutionFolder.sln";
			var modifiedSolutioFilePath = @"C:\Temp\Solution-WithProjectInSolutionFolder.sln";

			Instances.FileSystemOperator.CopyFile(originalSolutionFilePath, modifiedSolutioFilePath);

			var solutionFolderName = "_Dependencies";

			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Magyar\source\R5T.Magyar\R5T.Magyar.csproj";

			var random = Instances.RandomOperator.WithDefaultSeed(offset: 1);

			var projectIdentity = Instances.GuidOperator.New(random);

			Instances.SolutionFileOperator.AddProject_InSolutionFolder(
				modifiedSolutioFilePath,
				projectFilePath,
				solutionFolderName,
				projectIdentity);
		}

		public void AddSolutionFolder()
        {
			var originalSolutionFilePath = @"C:\Temp\Solution.sln";
			var modifiedSolutioFilePath = @"C:\Temp\Solution-WithSolutionFolder.sln";

			Instances.FileSystemOperator.CopyFile(originalSolutionFilePath, modifiedSolutioFilePath);

			var solutionFolderName = "_Dependencies";

			var random = Instances.RandomOperator.WithDefaultSeed();

			var solutionFolderIdentity = Instances.GuidOperator.New(random);

			Instances.SolutionFileOperator.AddSolutionFolder(
				modifiedSolutioFilePath,
				solutionFolderName,
				solutionFolderIdentity);
		}

		public void RemoveAnotherProject()
        {
			var originalSolutionFilePath = @"C:\Temp\Solution-WithAnotherProject.sln";
			var modifiedSolutioFilePath = @"C:\Temp\Solution-WithoutAnotherProject.sln";

			Instances.FileSystemOperator.CopyFile(originalSolutionFilePath, modifiedSolutioFilePath);

			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Magyar\source\R5T.Magyar\R5T.Magyar.csproj";

			Instances.SolutionFileOperator.RemoveProject(
				modifiedSolutioFilePath,
				projectFilePath);
		}

		public void AddAnotherProject()
        {
			var originalSolutionFilePath = @"C:\Temp\Solution-WithProject.sln";
			var modifiedSolutioFilePath = @"C:\Temp\Solution-WithAnotherProject.sln";

			Instances.FileSystemOperator.CopyFile(originalSolutionFilePath, modifiedSolutioFilePath);

			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Magyar\source\R5T.Magyar\R5T.Magyar.csproj";

			var random = Instances.RandomOperator.WithDefaultSeed();

			var projectIdentity = Instances.GuidOperator.New(random);

			Instances.SolutionFileOperator.AddProject(
				modifiedSolutioFilePath,
				projectFilePath,
				projectIdentity);
		}

		public void AddProject()
        {
			var originalSolutionFilePath = @"C:\Temp\Solution.sln";
			var modifiedSolutioFilePath = @"C:\Temp\Solution-WithProject.sln";

			Instances.FileSystemOperator.CopyFile(originalSolutionFilePath, modifiedSolutioFilePath);

			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0024\source\R5T.F0024\R5T.F0024.csproj";

			Instances.SolutionFileOperator.AddProject(
				modifiedSolutioFilePath,
				projectFilePath);
        }
	}
}