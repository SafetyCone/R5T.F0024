using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface ISolutionFileSerializer : IFunctionalityMarker
	{
		// A constant values for use where a constant value is required (like switch statements).
		public const string ExtensibilityGlobals = "ExtensibilityGlobals";
		public const string NestedProjects = "NestedProjects";
		public const string ProjectConfigurationPlatforms = "ProjectConfigurationPlatforms";
		public const string SolutionConfigurationPlatforms = "SolutionConfigurationPlatforms";


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


		public string[] GetGlobalSectionOrderedNames()
        {
			var output = new[]
			{
				Instances.GlobalSectionNames.SolutionConfigurationPlatforms,
				Instances.GlobalSectionNames.ProjectConfigurationPlatforms,
				Instances.GlobalSectionNames.SolutionProperties,
				Instances.GlobalSectionNames.ExtensibilityGlobals,
			};

			return output;
        }

		public IEnumerable<IGlobalSection> OrderGlobalSections(IEnumerable<IGlobalSection> globalSections)
        {
			var orderedNames = this.GetGlobalSectionOrderedNames();

			var orderedNamesComparer = new OrderedNamesComparer(orderedNames);

			var output = globalSections.OrderBy(x => x.Name, orderedNamesComparer);
			return output;
        }

		public string Serialize_ProjectNesting(ProjectNesting projectNesting)
        {
			var output = $"{Instances.GuidOperator.ToString_ForSolutionFile(projectNesting.ChildProjectIdentity)} = {Instances.GuidOperator.ToString_ForSolutionFile(projectNesting.ParentProjectIdentity)}";
			return output;
        }

		public string Serialize_BuildConfigurationPlatform(BuildConfigurationPlatform buildConfigurationPlatform)
        {
			var buildConfiguration = this.Serialize_BuildConfiguration(buildConfigurationPlatform.BuildConfiguration);
			var platform = this.Serialize_Platform(buildConfigurationPlatform.Platform);

			var output = $"{buildConfiguration}|{platform}";
			return output;
        }

		public string Serialize_BuildConfiguration(BuildConfiguration buildConfiguration)
        {
			var output = buildConfiguration.ToString();
			return output;
        }

		public string Serialize_Platform(Platform platform)
        {
			var output = platform switch
			{
				Platform.AnyCPU => "Any CPU",
				Platform.x64 => "x64",
				Platform.x86 => "x86",
				_ => throw F0002.Instances.EnumerationHelper.GetSwitchDefaultCaseException(platform),
            };

			return output;
        }

		public string Serialize_ProjectConfigurationIndicator(ProjectConfigurationIndicator indicator)
        {
			var output = indicator switch
			{
				ProjectConfigurationIndicator.ActiveCfg => "ActiveCfg",
				ProjectConfigurationIndicator.Build0 => "Build.0",
                _ => throw F0002.Instances.EnumerationHelper.GetSwitchDefaultCaseException(indicator),
            };

			return output;
        }

		public string Serialize_ProjectBuildConfigurationMapping(ProjectBuildConfigurationMapping mapping)
        {
			var projectIdentity = Instances.GuidOperator.ToString_ForSolutionFile(mapping.ProjectIdentity);
			var solutionBuildConfiguration = this.Serialize_BuildConfigurationPlatform(mapping.BuildConfigurationPlatform);
			var projectConfigurationIndicator = this.Serialize_ProjectConfigurationIndicator(mapping.ProjectConfigurationIndicator);
			var mappedSolutionBuildConfiguration = this.Serialize_BuildConfigurationPlatform(mapping.MappedBuildConfigurationPlatform);

			var output = $"{projectIdentity}.{solutionBuildConfiguration}.{projectConfigurationIndicator} = {mappedSolutionBuildConfiguration}";
			return output;
		}

		public IEnumerable<string> Serialize_GlobalSection(IGlobalSection globalSection, IEnumerable<string> bodyLines)
        {
			var output = Instances.EnumerableOperator.From(
				$"GlobalSection({globalSection.Name}) = {globalSection.PreOrPostSolution.ToString_ForSolutionFile()}")
				.Append(bodyLines
					// Prepend tab to each body line.
					.Select(x => $"\t{x}"))
				.Append("EndGlobalSection")
				// Prepend tab to each line of the whole section.
				.Select(x => $"\t{x}")
				.Now();

			return output;
		}

		public void Serialize(
			string solutionFilePath,
			SolutionFile solutionFile)
		{
			var lines = new List<string>
			{
				// Add an initial blank line.
				"",
				solutionFile.VersionInformation.FormatInformation,
				solutionFile.VersionInformation.VersionDescription,
				solutionFile.VersionInformation.Version,
				solutionFile.VersionInformation.MinimumVersion,
			};

			lines.AddRange(solutionFile.ProjectFileReferences.SelectMany(
				projectReferenceLine => new[]
				{
					ISolutionFileSerializer.SerializeProjectFileReferenceToLine(projectReferenceLine),
					"EndProject",
				}));

			lines.Add("Global");

			var orderedGlobalSections = this.OrderGlobalSections(solutionFile.GlobalSections);

			foreach (var globalSection in orderedGlobalSections)
            {
				var sectionlines = globalSection switch
				{
					ExtensibilityGlobalsGlobalSection extensibilityGlobalsGlobalSection => this.Serialize_GlobalSection(extensibilityGlobalsGlobalSection,
						Instances.EnumerableOperator.From($"SolutionGuid = {Instances.GuidOperator.ToString_ForSolutionFile(extensibilityGlobalsGlobalSection.SolutionIdentity)}")),

					LinesBasedGlobalSection linesBasedGlobalSection => this.Serialize_GlobalSection(linesBasedGlobalSection,
						linesBasedGlobalSection.Lines),

					NestedProjectsGlobalSection nestedProjectsGlobalSection => this.Serialize_GlobalSection(nestedProjectsGlobalSection,
						nestedProjectsGlobalSection.ProjectNestings
							.Select(x => this.Serialize_ProjectNesting(x))),

					ProjectConfigurationPlatformsGlobalSection projectConfigurationPlatforms => this.Serialize_GlobalSection(projectConfigurationPlatforms,
						projectConfigurationPlatforms.ProjectBuildConfigurationMappings
							.Select(x => this.Serialize_ProjectBuildConfigurationMapping(x))),

					SolutionConfigurationPlatformsGlobalSection solutionConfigurationPlatforms => this.Serialize_GlobalSection(solutionConfigurationPlatforms,
						solutionConfigurationPlatforms.SolutionBuildConfigurationMappings
							.Select(x => $"{this.Serialize_BuildConfigurationPlatform(x.Source)} = {this.Serialize_BuildConfigurationPlatform(x.Destination)}")),

					_ => throw new Exception("Unhandled global section type."),
				};

				lines.AddRange(sectionlines);
            }

			lines.Add("EndGlobal");

			using var stream = Instances.FileStreamOperator.NewWrite(solutionFilePath);
			using var writer = Instances.StreamWriterOperator.NewLeaveOpenAddBOM(stream);

			foreach (var line in lines)
			{
				writer.WriteLine(line);
			}
		}

		public SolutionFile Deserialize(string solutionFilePath)
		{
			var solutionFile = new SolutionFile();

			var allLines = Instances.FileOperator.ReadAllLines_Synchronous(solutionFilePath)
				.ToList();

			var enumerator = allLines.GetEnumerator();

			// Start.
			enumerator.MoveNext();

			// Ignore the first blank line.
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

			var hasProjects = ISolutionFileSerializer.IsProjectLine(line);
			if (hasProjects)
			{
				var isGlobalLine = false;

				var projectFileReferences = solutionFile.ProjectFileReferences;

				while (!isGlobalLine)
				{
					var projectFileReference = ISolutionFileSerializer.DeserializeProjectFileReference(line);

					projectFileReferences.Add(projectFileReference);

					enumerator.MoveNext();

					line = enumerator.Current;

					var isEndProjectLine = ISolutionFileSerializer.IsEndProjectLine(line);
					if (!isEndProjectLine)
					{
						throw new Exception("Line should have been an end project line.");
					}

					enumerator.MoveNext();

					line = enumerator.Current;

					isGlobalLine = IsGlobalLine(line);
				}
			}

			// At this point, line is a global line.
			var isEndGlobal = false;
			while (!isEndGlobal)
			{
				enumerator.MoveNext();

				line = enumerator.Current;

				var isGlobalSection = ISolutionFileSerializer.IsGlobalSectionLine(line);
				if (isGlobalSection)
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

					// First, deserialize to a lines-based global section. Then, convert to a specific global section type (if possible).
					var sectionLines = new List<string>();
					while (!isEndGlobalSection)
					{
						// Trim the line.
						sectionLines.Add(line.Trim());

						enumerator.MoveNext();

						line = enumerator.Current;

						isEndGlobalSection = ISolutionFileSerializer.IsEndGlobalSectionLine(line);
					}

					var lines = new LinesBasedGlobalSection
					{
						Name = sectionName,
						PreOrPostSolution = preOrPostSolution,
						Lines = sectionLines,
					};

					// Convert to a specific global section type, if possible.
					var globalSection = this.Deserialize_GlobalSection(lines);

					solutionFile.GlobalSections.Add(globalSection);
				}

				isEndGlobal = IsEndGlobalLine(line);
			}

			return solutionFile;
		}

		public IGlobalSection Deserialize_GlobalSection(LinesBasedGlobalSection lines)
        {
			IGlobalSection output = lines.Name switch
			{
				ISolutionFileSerializer.ExtensibilityGlobals => this.Deserialize_ExtensibilityGlobals(lines),
				ISolutionFileSerializer.NestedProjects => this.Deserialize_NestedProjects(lines),
				ISolutionFileSerializer.ProjectConfigurationPlatforms => this.Deserialize_ProjectConfigurationPlatforms(lines),
				ISolutionFileSerializer.SolutionConfigurationPlatforms => this.Deserialize_SolutionConfigurationPlatforms(lines),
				// Otherwise, just return the lines-based global section.
				_ => lines,
			};

			return output;
        }

		public ExtensibilityGlobalsGlobalSection Deserialize_ExtensibilityGlobals(LinesBasedGlobalSection lines)
        {
			var output = new ExtensibilityGlobalsGlobalSection
			{
				Name = lines.Name,
				PreOrPostSolution = lines.PreOrPostSolution,
			};

            foreach (var line in lines.Lines)
            {
				var tokens = line.Split(Instances.Characters.Equals, StringSplitOptions.RemoveEmptyEntries)
					.Trim()
					.ToArray();
				
				var key = tokens[0];
				var valueToken = tokens[1];

                switch (key)
                {
					case "SolutionGuid":
						var solutionIdentity = Instances.GuidOperator.Parse_ForSolutionFile(valueToken);
						output.SolutionIdentity = solutionIdentity;
						break;

					default:
						throw new Exception($"Unknown extensibility global key: '{key}'");
				}
            }

			return output;
        }

		public NestedProjectsGlobalSection Deserialize_NestedProjects(LinesBasedGlobalSection lines)
        {
			var output = new NestedProjectsGlobalSection
			{
				Name = lines.Name,
				PreOrPostSolution = lines.PreOrPostSolution,
			};

            foreach (var line in lines.Lines)
            {
				var projectNesting = this.Deserialize_ProjectNesting(line);

				output.ProjectNestings.Add(projectNesting);
            }

			return output;
        }

		public ProjectNesting Deserialize_ProjectNesting(string projectNesting)
        {
			var tokens = projectNesting.Split(Instances.Characters.Equals, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.Trim())
				.ToArray();

			var childProjectIdentityToken = tokens[0];
			var parentProjectIdentityToken = tokens[1];

			var childProjectIdentity = Instances.GuidOperator.Parse_ForSolutionFile(childProjectIdentityToken);
			var parentProjectIdentity = Instances.GuidOperator.Parse_ForSolutionFile(parentProjectIdentityToken);

			var output = new ProjectNesting
			{
				ChildProjectIdentity = childProjectIdentity,
				ParentProjectIdentity = parentProjectIdentity,
			};

			return output;
        }

		public ProjectConfigurationPlatformsGlobalSection Deserialize_ProjectConfigurationPlatforms(LinesBasedGlobalSection lines)
        {
			var output = new ProjectConfigurationPlatformsGlobalSection
			{
				Name = lines.Name,
				PreOrPostSolution = lines.PreOrPostSolution,
			};

            foreach (var line in lines.Lines)
            {
				var projectBuildConfigurationMapping = this.Deserialize_ProjectBuildConfigurationMapping(line);

				output.ProjectBuildConfigurationMappings.Add(projectBuildConfigurationMapping);
			}

			return output;
        }

		public ProjectBuildConfigurationMapping Deserialize_ProjectBuildConfigurationMapping(string projectBuildConfigurationMapping)
        {
			var equalsTokens = projectBuildConfigurationMapping.Split(Instances.Characters.Equals, StringSplitOptions.RemoveEmptyEntries)
				.Trim()
				.ToArray();

			var mapToken = equalsTokens[0];
			var mappedToken = equalsTokens[1];

			var mapTokens = mapToken.Split(Instances.Characters.Period, StringSplitOptions.RemoveEmptyEntries);

			var projectIdentityToken = mapTokens[0];
			var buildConfigurationPlatformToken = mapTokens[1];
			var projectConfigurationIndicatorToken = String.Join(Instances.Characters.Period, mapTokens[2..]); // Required for "Build.0".

			var projectIdentity = Instances.GuidOperator.Parse_ForSolutionFile(projectIdentityToken);
			var buildConfigurationPlatform = this.Deserialize_BuildConfigurationPlatform(buildConfigurationPlatformToken);
			var projectConfigurationIndicator = this.Deserialize_ProjectConfigurationIndicator(projectConfigurationIndicatorToken);

			var mappedSolutionBuildConfiguration = this.Deserialize_BuildConfigurationPlatform(mappedToken);

			var output = new ProjectBuildConfigurationMapping
			{
				ProjectIdentity = projectIdentity,
				BuildConfigurationPlatform = buildConfigurationPlatform,
				ProjectConfigurationIndicator = projectConfigurationIndicator,
				MappedBuildConfigurationPlatform = mappedSolutionBuildConfiguration,
			};

			return output;
        }

		public ProjectConfigurationIndicator Deserialize_ProjectConfigurationIndicator(string projectConfigurationIndicator)
        {
			var output = projectConfigurationIndicator switch
			{
				"ActiveCfg" => ProjectConfigurationIndicator.ActiveCfg,
				"Build.0" => ProjectConfigurationIndicator.Build0,
				_ => throw Instances.EnumerationHelper.UnrecognizedEnumerationValueException<ProjectConfigurationIndicator>(projectConfigurationIndicator),
			};

			return output;
        }

		public SolutionConfigurationPlatformsGlobalSection Deserialize_SolutionConfigurationPlatforms(LinesBasedGlobalSection lines)
        {
			var output = new SolutionConfigurationPlatformsGlobalSection
			{
				Name = lines.Name,
				PreOrPostSolution = lines.PreOrPostSolution,
			};

            foreach (var line in lines.Lines)
            {
				var solutionBuildConfigurationPlatform = this.Deserialize_SolutionBuildConfigurationPlatform(line);

				output.SolutionBuildConfigurationMappings.Add(solutionBuildConfigurationPlatform);
            }

			return output;
        }

		public SolutionBuildConfigurationPlatform Deserialize_SolutionBuildConfigurationPlatform(string solutionBuildConfigurationPlatform)
        {
			var tokens = solutionBuildConfigurationPlatform.Split(Instances.Characters.Equals, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.Trim())
				.ToArray();

			var sourceToken = tokens[0];
			var destinationToken = tokens[1];

			var source = this.Deserialize_BuildConfigurationPlatform(sourceToken);
			var destination = this.Deserialize_BuildConfigurationPlatform(destinationToken);

			var output = new SolutionBuildConfigurationPlatform
			{
				Destination = destination,
				Source = source,
			};

			return output;
        }

		public BuildConfigurationPlatform Deserialize_BuildConfigurationPlatform(string buildConfigurationPlatform)
        {
			var tokens = buildConfigurationPlatform.Split(Instances.Characters.Pipe, StringSplitOptions.RemoveEmptyEntries);

			var buildConfigurationToken = tokens[0];
			var platformToken = tokens[1];

			var buildConfiguration = this.Deserialize_BuildConfiguration(buildConfigurationToken);
			var platform = this.Deserialize_Platform(platformToken);

			var output = new BuildConfigurationPlatform
			{
				BuildConfiguration = buildConfiguration,
				Platform = platform,
			};

			return output;
        }

		public BuildConfiguration Deserialize_BuildConfiguration(string buildConfiguration)
        {
			var output = buildConfiguration switch
			{
				"Debug" => BuildConfiguration.Debug,
				"Release" => BuildConfiguration.Release,
				_ => throw Instances.EnumerationHelper.UnrecognizedEnumerationValueException<BuildConfiguration>(buildConfiguration),
            };

			return output;
        }

		public Platform Deserialize_Platform(string platform)
        {
			var output = platform switch
			{
				"Any CPU" => Platform.AnyCPU,
				"x64" => Platform.x64,
				"x86" => Platform.x86,
				_ => throw Instances.EnumerationHelper.UnrecognizedEnumerationValueException<Platform>(platform),
			};

			return output;
        }
	}
}