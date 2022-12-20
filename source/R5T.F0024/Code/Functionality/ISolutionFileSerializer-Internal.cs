using System;
using System.Collections.Generic;
using System.Linq;

using R5T.F0000;
using R5T.T0132;

using R5T.F0024.T001;
using R5T.Z0000;

namespace R5T.F0024.Internal
{
	[FunctionalityMarker]
	public partial interface ISolutionFileSerializer : IFunctionalityMarker
	{
        public string GetTrimmedLine(IEnumerator<string> enumerator)
        {
            var trimmedLine = enumerator.Current.Trim();
            return trimmedLine;
        }

        public string GetNextTrimmedLine(IEnumerator<string> enumerator)
        {
            enumerator.MoveNext();

            var trimmedLine = this.GetTrimmedLine(enumerator);
            return trimmedLine;
        }

        public ProjectFileReference[] Deserialize_ProjectFileReferences(
            IEnumerator<string> enumerator)
        {
            var projectFileReferences = new List<ProjectFileReference>();

            var trimmedLine = this.GetTrimmedLine(enumerator);

            var isProjectLine = this.IsProjectLine(trimmedLine);

            // Handle projects.
            while (isProjectLine)
            {
                var projectFileReference = this.Deserialize_ProjectFileReference(enumerator);

                projectFileReferences.Add(projectFileReference);

                trimmedLine = this.GetTrimmedLine(enumerator);

                isProjectLine = this.IsProjectLine(trimmedLine);
            }

            // No move-next because project file reference has already done it.

            return projectFileReferences.ToArray();
        }

        public ProjectFileReference Deserialize_ProjectFileReference(
            IEnumerator<string> enumerator)
        {
            var trimmedLine = this.GetTrimmedLine(enumerator);

            this.Verify_IsProjectLine(trimmedLine);

            var projectFileReference = this.Deserialize_ProjectFileReference(trimmedLine);

            trimmedLine = this.GetNextTrimmedLine(enumerator);

            var isProjectSectionLine = this.IsProjectSectionLine(trimmedLine);
            if(isProjectSectionLine)
            {
                var projectSections = this.Deserialize_ProjectSections(enumerator);

                projectFileReference.ProjectSections.AddRange(projectSections);
            }

            trimmedLine = this.GetTrimmedLine(enumerator);

            var isEndProjectLine = this.IsEndProjectLine(trimmedLine);
            if(isEndProjectLine)
            {
                enumerator.MoveNext();

                return projectFileReference;
            }

            // Else, unknown.
            throw new Exception($"Expected end project line. Found:\n{trimmedLine}");
        }

        public IProjectSection[] Deserialize_ProjectSections(IEnumerator<string> enumerator)
        {
            var trimmedLine = this.GetTrimmedLine(enumerator);

            this.Verify_IsProjectSectionLine(trimmedLine);

            var projectSections = new List<IProjectSection>();

            var isProjectSectionLine = this.IsProjectSectionLine(trimmedLine);

            // Handle projects.
            while (isProjectSectionLine)
            {
                var projectSection = this.Deserialize_ProjectSection(enumerator);

                projectSections.Add(projectSection);

                trimmedLine = this.GetTrimmedLine(enumerator);

                isProjectSectionLine = this.IsProjectSectionLine(trimmedLine);
            }

            // No move-next, since project section has handled that.

            return projectSections.ToArray();
        }

        public IProjectSection Deserialize_ProjectSection(IEnumerator<string> enumerator)
        {
            var trimmedLine = this.GetTrimmedLine(enumerator);

            this.Verify_IsProjectSectionLine(trimmedLine);

            var lines = this.Deserialize_Section(
                enumerator,
                this.IsEndProjectSectionLine);

            var projectSection = this.Deserialize_ProjectSection(lines);
            return projectSection;
        }

        public IProjectSection Deserialize_ProjectSection(LinesBasedSection lines)
        {
            IProjectSection output = lines.Name switch
            {
                // Everything is a lines-based project section for now.

                // Otherwise, just return the lines-based project section.
                _ => this.Deserialize_LinesBasedProjectSection(lines)
            };

            return output;
        }

        public ProjectFileReference Deserialize_ProjectFileReference(string projectLine)
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

        public string Serialize_ProjectFileReference_ToLine(ProjectFileReference projectFileReference)
        {
            var output = $"{SolutionFileStrings.Instance.Project}(\"{Instances.GuidOperator.ToString_ForSolutionFile(projectFileReference.ProjectTypeIdentity)}\") = \"{projectFileReference.ProjectName}\", \"{projectFileReference.ProjectRelativeFilePath}\", \"{Instances.GuidOperator.ToString_ForSolutionFile(projectFileReference.ProjectIdentity)}\"";
            return output;
        }

        public bool IsEndGlobalLine(string line)
        {
            var output = line.StartsWith(
                SolutionFileStrings.Instance.EndGlobal);

            return output;
        }

        public bool IsEndGlobalSectionLine(string line)
        {
            var output = line.TrimStart().StartsWith(
                SolutionFileStrings.Instance.EndGlobalSection);

            return output;
        }

        public bool IsEndProjectLine(string line)
        {
            var output = line.StartsWith(
                SolutionFileStrings.Instance.EndProject);

            return output;
        }

        public bool IsEndProjectSectionLine(string line)
        {
            var output = line.StartsWith(
                SolutionFileStrings.Instance.EndProjectSection);

            return output;
        }

        public bool IsGlobalLine(string line)
        {
            var output = line.StartsWith(
                SolutionFileStrings.Instance.Global);

            return output;
        }

        public bool IsGlobalSectionLine(string line)
        {
            var output = line.TrimStart().StartsWith(
                SolutionFileStrings.Instance.GlobalSection);

            return output;
        }

        public bool IsProjectLine(string line)
        {
            var output = line.StartsWith(
                SolutionFileStrings.Instance.Project);

            return output;
        }

        public bool IsProjectSectionLine(string line)
        {
            var output = line.TrimStart().StartsWith(
                SolutionFileStrings.Instance.ProjectSection);

            return output;
        }

        public IGlobalSection[] Deserialize_GlobalSections(IEnumerator<string> enumerator)
        {
            var trimmedLine = this.GetTrimmedLine(enumerator);

            this.Verify_IsGlobalLine(trimmedLine);

            trimmedLine = this.GetNextTrimmedLine(enumerator);

            var globalSections = new List<IGlobalSection>();

            var isGlobalSectionLine = this.IsGlobalSectionLine(trimmedLine);

            // Handle global sections.
            while (isGlobalSectionLine)
            {
                var globalSection = this.Deserialize_GlobalSection(enumerator);

                globalSections.Add(globalSection);

                trimmedLine = this.GetTrimmedLine(enumerator);

                isGlobalSectionLine = this.IsGlobalSectionLine(trimmedLine);
            }

            enumerator.MoveNext();

            return globalSections.ToArray();
        }

        public IGlobalSection Deserialize_GlobalSection(IEnumerator<string> enumerator)
        {
            var trimmedLine = this.GetTrimmedLine(enumerator);

            this.Verify_IsGlobalSectionLine(trimmedLine);

            var lines = this.Deserialize_Section(
                enumerator,
                this.IsEndGlobalSectionLine);

            var globalSection = this.Deserialize_GlobalSection(lines);
            return globalSection;
        }

        public IGlobalSection Deserialize_GlobalSection(LinesBasedSection lines)
        {
            IGlobalSection output = lines.Name switch
            {
                IGlobalSectionNames.ExtensibilityGlobals_Constant => this.Deserialize_ExtensibilityGlobals(lines),
                IGlobalSectionNames.NestedProjects_Constant => this.Deserialize_NestedProjects(lines),
                IGlobalSectionNames.ProjectConfigurationPlatforms_Constant => this.Deserialize_ProjectConfigurationPlatforms(lines),
                IGlobalSectionNames.SolutionConfigurationPlatforms_Constant => this.Deserialize_SolutionConfigurationPlatforms(lines),
                // Otherwise, just return the lines-based global section.
                _ => this.Deserialize_LinesBasedGlobalSection(lines)
            };

            return output;
        }

        public ExtensibilityGlobalsGlobalSection Deserialize_ExtensibilityGlobals(LinesBasedSection lines)
        {
            var output = new ExtensibilityGlobalsGlobalSection()
                .FillFrom(lines);

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

        public SolutionFile Deserialize_FromLines(string[] linesArray)
        {
            var lines = linesArray.ToList();

            var solutionFile = new SolutionFile();

            var enumerator = lines.GetEnumerator() as IEnumerator<string>;

            // Start.
            enumerator.MoveNext();

            // Ignore the first blank line.
            enumerator.MoveNext();

            var formatInformation = this.GetTrimmedLine(enumerator);

            var versionDescription = this.GetNextTrimmedLine(enumerator);

            var version = this.GetNextTrimmedLine(enumerator);

            var minimumVersion = this.GetNextTrimmedLine(enumerator);

            solutionFile.VersionInformation = new VersionInformation
            {
                FormatInformation = formatInformation,
                VersionDescription = versionDescription,
                Version = version,
                MinimumVersion = minimumVersion,
            };

            // Advance to the next line.
            enumerator.MoveNext();

            // Handle projects (if any).
            var projectFileReferences = this.Deserialize_ProjectFileReferences(enumerator);

            solutionFile.ProjectFileReferences.AddRange(projectFileReferences);

            // We should now be at the globals section.
            // Verify the solution file has a globals section.
            var trimmedLine = this.GetTrimmedLine(enumerator);

            this.Verify_IsGlobalLine(trimmedLine);

            // Handle global sections.
            var globalSections = this.Deserialize_GlobalSections(enumerator);

            solutionFile.GlobalSections.AddRange(globalSections);

            // Ensure there are no more lines finished.
            do
            {
                trimmedLine = this.GetTrimmedLine(enumerator);

                if (StringOperator.Instance.IsNotNullAndNotEmpty(trimmedLine))
                {
                    throw new Exception($"Null or empty lines at end of file expected. Found: {trimmedLine}");
                }
            }
            while (enumerator.MoveNext());

            return solutionFile;
        }

        public NestedProjectsGlobalSection Deserialize_NestedProjects(LinesBasedSection lines)
        {
            var output = new NestedProjectsGlobalSection()
                .FillFrom(lines);

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

        public ProjectConfigurationPlatformsGlobalSection Deserialize_ProjectConfigurationPlatforms(LinesBasedSection lines)
        {
            var output = new ProjectConfigurationPlatformsGlobalSection()
                .FillFrom(lines);

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
            var projectConfigurationIndicatorToken = StringOperator.Instance.Join(Instances.Characters.Period, mapTokens[2..]); // Required for "Build.0".

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

        public LinesBasedGlobalSection Deserialize_LinesBasedGlobalSection(LinesBasedSection lines)
        {
            var linesBasedGlobalSection = new LinesBasedGlobalSection
            {
                Lines = lines.Lines,
            }
            .FillFrom(lines);

            return linesBasedGlobalSection;
        }

        public LinesBasedProjectSection Deserialize_LinesBasedProjectSection(LinesBasedSection lines)
        {
            var linesBasedGlobalSection = new LinesBasedProjectSection
            {
                Lines = lines.Lines,
            }
            .FillFrom(lines);

            return linesBasedGlobalSection;
        }

        public SolutionConfigurationPlatformsGlobalSection Deserialize_SolutionConfigurationPlatforms(LinesBasedSection lines)
        {
            var output = new SolutionConfigurationPlatformsGlobalSection()
                .FillFrom(lines);

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

        public LinesBasedSection Deserialize_Section(
			IEnumerator<string> enumerator,
			Func<string, bool> isEndSectionLine)
		{
            var trimmedLine = this.GetTrimmedLine(enumerator);

            // Parse the global section.
            var tokens = trimmedLine.Split('(', ')', '=');

            var sectionName = tokens[1];
            var preOrPostSolutionToken = tokens.Last()
                // Trim.
                .Trim();

            trimmedLine = this.GetNextTrimmedLine(enumerator);

            var isSectionEndLine = isEndSectionLine(trimmedLine);

            // First, deserialize to a lines-based global section. Then, convert to a specific global section type (if possible).
            var sectionLines = new List<string>();

            while (!isSectionEndLine)
            {
                // Trim the line.
                sectionLines.Add(trimmedLine);

                trimmedLine = this.GetNextTrimmedLine(enumerator);

                isSectionEndLine = isEndSectionLine(trimmedLine);
            }

            enumerator.MoveNext();

            var linesSection = new LinesBasedSection
            {
                Name = sectionName,
                PreOrPost = preOrPostSolutionToken,
                Lines = sectionLines,
            };

            return linesSection;
        }

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

        public IEnumerable<ISection> OrderGlobalSections(IEnumerable<ISection> globalSections)
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
                Platform.AnyCPU => SolutionFileStrings.Instance.AnyCpu,
                Platform.x64 => SolutionFileStrings.Instance.x64,
                Platform.x86 => SolutionFileStrings.Instance.x86,
                _ => throw F0002.Instances.EnumerationHelper.GetSwitchDefaultCaseException(platform),
            };

            return output;
        }

        public string Serialize_ProjectConfigurationIndicator(ProjectConfigurationIndicator indicator)
        {
            var output = indicator switch
            {
                ProjectConfigurationIndicator.ActiveCfg => SolutionFileStrings.Instance.ActiveCfg,
                ProjectConfigurationIndicator.Build0 => SolutionFileStrings.Instance.Build_0,
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

        public IEnumerable<string> Serialize_Section(LinesBasedSection section,
            string sectionStart,
            string sectionEnd)
        {
            var output = Instances.EnumerableOperator.From(
                $"{sectionStart}({section.Name}) = {section.PreOrPost}")
                .Append(section.Lines
                    // Prepend tab to each body line.
                    .Select(x => $"\t{x}"))
                .Append(sectionEnd)
                // Prepend tab to each line of the whole section.
                .Select(x => $"\t{x}")
                .Now();

            return output;
        }

        public IEnumerable<string> Serialize_GlobalSection(IGlobalSection globalSection, IEnumerable<string> bodyLines)
        {
            var output = Instances.EnumerableOperator.From(
                $"{SolutionFileStrings.Instance.GlobalSection}({globalSection.Name}) = {globalSection.PreOrPost}")
                .Append(bodyLines
                    // Prepend tab to each body line.
                    .Select(x => $"\t{x}"))
                .Append(SolutionFileStrings.Instance.EndGlobalSection)
                // Prepend tab to each line of the whole section.
                .Select(x => $"\t{x}")
                .Now();

            return output;
        }

        public IEnumerable<string> Serialize_ProjectSection(LinesBasedProjectSection linesBasedProjectSection)
        {
            var output = this.Serialize_Section(
                linesBasedProjectSection,
                SolutionFileStrings.Instance.ProjectSection,
                SolutionFileStrings.Instance.EndProjectSection);

            return output;
        }

        public IEnumerable<string> Serialize_ProjectSection(IProjectSection projectSection)
        {
            var lines = projectSection switch
            {
                LinesBasedProjectSection linesBasedProjectSection => this.Serialize_ProjectSection(linesBasedProjectSection),
                _ => throw new Exception($"Unhandled project section type: {F0000.TypeNameOperator.Instance.GetTypeNameOf(projectSection)}"),
            };

            return lines;
        }

        public IEnumerable<string> Serialize_ProjectFileReference(ProjectFileReference projectFileReference)
        {
            var output = EnumerableOperator.Instance.From(this.Serialize_ProjectFileReference_ToLine(projectFileReference))
                .AppendRange(projectFileReference.ProjectSections
                    .SelectMany(projectSection => this.Serialize_ProjectSection(projectSection)))
                .Append(SolutionFileStrings.Instance.EndProject)
                ;

            return output;
        }

        public List<string> Serialize_ToLines(SolutionFile solutionFile)
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

            lines.AddRange(solutionFile.ProjectFileReferences
                .SelectMany(projectFileReference => this.Serialize_ProjectFileReference(projectFileReference)));

            lines.Add(SolutionFileStrings.Instance.Global);

            var orderedGlobalSections = this.OrderGlobalSections(solutionFile.GlobalSections);

            foreach (var globalSection in orderedGlobalSections)
            {
                var sectionlines = globalSection switch
                {
                    ExtensibilityGlobalsGlobalSection extensibilityGlobalsGlobalSection => this.Serialize_GlobalSection(extensibilityGlobalsGlobalSection,
                        Instances.EnumerableOperator.From($"{SolutionFileStrings.Instance.SolutionGuid} = {Instances.GuidOperator.ToString_ForSolutionFile(extensibilityGlobalsGlobalSection.SolutionIdentity)}")),

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

            lines.Add(SolutionFileStrings.Instance.EndGlobal);

            return lines;
        }

        public string Serialize_ToText(SolutionFile solutionFile)
        {
            var lines = this.Serialize_ToLines(solutionFile);

            var text = StringOperator.Instance.Join(
                Z0000.Strings.Instance.NewLineForEnvironment,
                lines);

            return text;
        }

        public void Verify_IsGlobalLine(string trimmedLine)
        {
            var isGlobalLine = this.IsGlobalLine(trimmedLine);
            if (!isGlobalLine)
            {
                throw new Exception($"Global line expected. Found:\n{trimmedLine}");
            }
        }

        public void Verify_IsGlobalSectionLine(string trimmedLine)
        {
            var isGlobalSectionLine = this.IsGlobalSectionLine(trimmedLine);
            if (!isGlobalSectionLine)
            {
                throw new Exception($"Global section line expected. Found:\n{trimmedLine}");
            }
        }

        public void Verify_IsProjectLine(string trimmedLine)
        {
            var isProjectLine = this.IsProjectLine(trimmedLine);
            if (!isProjectLine)
            {
                throw new Exception($"Project line expected. Found:\n{trimmedLine}");
            }
        }

        public void Verify_IsProjectSectionLine(string trimmedLine)
        {
            var isProjectSectionLine = this.IsProjectSectionLine(trimmedLine);
            if (!isProjectSectionLine)
            {
                throw new Exception($"Project section line expected. Found:\n{trimmedLine}");
            }
        }
    }
}