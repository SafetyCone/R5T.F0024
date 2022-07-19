using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface ISolutionFileOperator : IFunctionalityMarker
	{
		#region Static

		private static ProjectFileReference DeserializeProjectFileReference(string projectLine)
        {
			// Example project line:
			// Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "R5T.F0024.Construction", "R5T.F0024.Construction\R5T.F0024.Construction.csproj", "{388EFF42-1731-4882-9034-63D9D7427904}"

			// Split on quotes, and take tokens of interest.
			var tokens = projectLine.Split('"',
				// Keep empty entries.
				StringSplitOptions.None);

			var projectTypeGuidToken = tokens[1];
			var projectName = tokens[3];
			var projectRelativeFilePath = tokens[5];
			var projectGuidToken = tokens[7];

			var projectTypeIdentity = Instances.GuidOperator.Parse_ForSolutionFile(projectTypeGuidToken);
			var projectIdentity = Instances.GuidOperator.Parse_ForSolutionFile(projectGuidToken);

			var output = new ProjectFileReference
			{
				ProjectIdentity = projectIdentity,
				ProjectName = projectName,
				ProjectRelativeFilePath = projectRelativeFilePath,
				ProjectTypeIdentity = projectTypeIdentity,
			};

			return output;
		}

		private static string SerializeProjectFileReferenceToLine(ProjectFileReference projectFileReference)
		{
			var output = $"Project(\"{Instances.GuidOperator.ToString_ForSolutionFile(projectFileReference.ProjectTypeIdentity)}\") = \"{projectFileReference.ProjectName}\", \"{projectFileReference.ProjectRelativeFilePath}\", \"{Instances.GuidOperator.ToString_ForSolutionFile(projectFileReference.ProjectIdentity)}\"";
			return output;
		}

		private static bool IsEndGlobalLine(string line)
		{
			var output = line.StartsWith("EndGlobal");
			return output;
		}

		private static bool IsEndGlobalSectionLine(string line)
		{
			var output = line.TrimStart().StartsWith("EndGlobalSection");
			return output;
		}

		private static bool IsEndProjectLine(string line)
		{
			var output = line.StartsWith("EndProject");
			return output;
		}

		private static bool IsGlobalLine(string line)
		{
			var output = line.StartsWith("Global");
			return output;
		}

		private static bool IsGlobalSectionLine(string line)
		{
			var output = line.TrimStart().StartsWith("GlobalSection");
			return output;
		}

		private static bool IsProjectLine(string line)
		{
			var output = line.StartsWith("Project");
			return output;
		}

		#endregion


		public void Serialize(
			string solutionFilePath,
			SolutionFile solutionFile)
        {
            var lines = new List<string>
            {
                // Blank first line.
                String.Empty,
				solutionFile.VersionInformation.FormatInformation,
				solutionFile.VersionInformation.VersionDescription,
				solutionFile.VersionInformation.Version,
				solutionFile.VersionInformation.MinimumVersion,
            };

			lines.AddRange(solutionFile.ProjectFileReferences.SelectMany(
				projectReferenceLine => new[]
				{
                    ISolutionFileOperator.SerializeProjectFileReferenceToLine(projectReferenceLine),
					"EndProject",
				}));

			lines.Add("Global");

            foreach (var globalSection in solutionFile.GlobalSections)
            {
				if(globalSection is LinesBasedGlobalSection linesBasedGlobalSection)
                {
					var sectionLines = EnumerableHelper.From(
						$"GlobalSection({linesBasedGlobalSection.Name}) = {linesBasedGlobalSection.PreOrPostSolution.ToString_ForSolutionFile()}")
						.Append(linesBasedGlobalSection.Lines
							// Prepend tab.
							.Select(x => $"\t{x}"))
						.Append("EndGlobalSection")
						// Prepend tab.
						.Select(x => $"\t{x}")
						.Now();

					lines.AddRange(sectionLines);
				}
				else
                {
					throw new Exception("Unhandled global section type.");
                }	
            }

			lines.Add("EndGlobal");

            using var stream = FileStreamHelper.NewWrite(solutionFilePath);
			using var writer = StreamWriterHelper.NewLeaveOpenAddBOM(stream);

            foreach (var line in lines)
            {
				writer.WriteLine(line);
            }
        }

		public SolutionFile Deserialize(string solutionFilePath)
        {
			var solutionFile = new SolutionFile
			{
				GlobalSections = new List<IGlobalSection>(),
			};

			var allLines = FileHelper.ReadAllLinesSynchronous(solutionFilePath)
				.ToList();

			var enumerator = allLines.GetEnumerator();

			// Start.
			enumerator.MoveNext();

			// Skip the first line.
			enumerator.MoveNext();

			var formatInformation = enumerator.Current;
			enumerator.MoveNext();

			var versionDescription = enumerator.Current;
			enumerator.MoveNext();

			var version = enumerator.Current;
			enumerator.MoveNext();

			var minimumVersion = enumerator.Current;
			enumerator.MoveNext();

			solutionFile.VersionInformation = new VersionInformation
			{
				FormatInformation = formatInformation,
				VersionDescription = versionDescription,
				Version = version,
				MinimumVersion = minimumVersion,
			};

			var line = enumerator.Current;

			var hasProjects = ISolutionFileOperator.IsProjectLine(line);
			if(hasProjects)
            {
				var isGlobalLine = false;

				var projectFileReferences = new List<ProjectFileReference>();

				while(!isGlobalLine)
                {
					var projectFileReference = ISolutionFileOperator.DeserializeProjectFileReference(line);

					projectFileReferences.Add(projectFileReference);

					enumerator.MoveNext();

					line = enumerator.Current;

					var isEndProjectLine = ISolutionFileOperator.IsEndProjectLine(line);
					if(!isEndProjectLine)
                    {
						throw new Exception("Line should have been an end project line.");
                    }

					enumerator.MoveNext();

					line = enumerator.Current;

					isGlobalLine = IsGlobalLine(line);
                }

				solutionFile.ProjectFileReferences = projectFileReferences;
            }

			// At this point, line is a global line.
			var isEndGlobal = false;
			while(!isEndGlobal)
            {
				enumerator.MoveNext();

				line = enumerator.Current;

				var isGlobalSection = ISolutionFileOperator.IsGlobalSectionLine(line);
				if(isGlobalSection)
                {
					// Parse the global section.
					var tokens = line.Split('(', ')', '=');

					var sectionName = tokens[1];
					var preOrPostSolutionToken = tokens.Last()
						// Trim.
						.Trim();

					var preOrPostSolution = preOrPostSolutionToken == "preSolution"
						? PreOrPostSolution.PreSolution
						: PreOrPostSolution.PostSolution
						;

					enumerator.MoveNext();

					line = enumerator.Current;

					var isEndGlobalSection = false;

					// For now, just parse all global sections as lines-based global sections
					var sectionLines = new List<string>();
					while(!isEndGlobalSection)
                    {
						// Trim the line.
						sectionLines.Add(line.Trim());

						enumerator.MoveNext();

						line = enumerator.Current;

						isEndGlobalSection = ISolutionFileOperator.IsEndGlobalSectionLine(line);
                    }

					var globalSection = new LinesBasedGlobalSection
					{
						Name = sectionName,
						PreOrPostSolution = preOrPostSolution,
						Lines = sectionLines,
					};

					solutionFile.GlobalSections.Add(globalSection);
                }

				isEndGlobal = IsEndGlobalLine(line);
            }

			return solutionFile;
		}
	}
}