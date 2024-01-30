using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.L0089.T000;
using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface ISolutionFileOperator : IFunctionalityMarker
    {
        private static Internal.ISolutionFileOperator Internal { get; } = F001.Internal.SolutionFileOperator.Instance;


        public void AddProject_InSolutionFolder(
            string solutionFilePath,
            string projectFilePath,
            string solutionFolderName,
            Guid projectIdentity)
        {
            this.InModifyContext_Synchronous(solutionFilePath,
                (solutionFile, _) =>
                {
                    // Acquire the solution folder identity (adding the solution folder if need be).
                    var solutionFolderIdentity = this.Acquire_SolutionFolderIdentity(solutionFile, solutionFolderName);

                    // First add the project.
                    this.AddProject(
                        solutionFile,
                        solutionFilePath,
                        projectFilePath,
                        projectIdentity);

                    // Then add the nesting.
                    var nestedProjectsGlobalSection = Instances.GlobalSectionOperator.Acquire_NestedProjects(solutionFile);

                    var projectNesting = new ProjectNesting()
                    {
                        ChildProjectIdentity = projectIdentity,
                        ParentProjectIdentity = solutionFolderIdentity,
                    };

                    nestedProjectsGlobalSection.ProjectNestings.Add(projectNesting);

                    return projectIdentity;
                });
        }

        public Guid AddProject_InSolutionFolder(
            string solutionFilePath,
            string projectFilePath,
            string solutionFolderName)
        {
            var projectIdentity = this.InModifyContext_Synchronous(solutionFilePath,
                (solutionFile, _) =>
                {
                    // Acquire the solution folder identity (adding the solution folder if need be).
                    var solutionFolderIdentity = this.Acquire_SolutionFolderIdentity(solutionFile, solutionFolderName);

                    // First add the project.
                    var projectIdentity = this.AddProject(
                        solutionFile,
                        solutionFilePath,
                        projectFilePath);

                    // Then add the nesting.
                    var nestedProjectsGlobalSection = Instances.GlobalSectionOperator.Acquire_NestedProjects(solutionFile);

                    var projectNesting = new ProjectNesting()
                    {
                        ChildProjectIdentity = projectIdentity,
                        ParentProjectIdentity = solutionFolderIdentity,
                    };

                    nestedProjectsGlobalSection.ProjectNestings.Add(projectNesting);

                    return projectIdentity;
                });

            return projectIdentity;
        }

        public Dictionary<string, Guid> AddProjects_InSolutionFolder(
            string solutionFilePath,
            IEnumerable<string> projectFilePaths,
            string solutionFolderName)
        {
            var projectIdentitiesByFilePath = this.InModifyContext_Synchronous(solutionFilePath,
                (solutionFile, _) =>
                {
                    // Acquire the solution folder identity (adding the solution folder if need be).
                    var solutionFolderIdentity = this.Acquire_SolutionFolderIdentity(solutionFile, solutionFolderName);

                    // First add the project.
                    var projectIdentitiesByFilePath = this.AddProjects(
                        solutionFile,
                        solutionFilePath,
                        projectFilePaths);

                    // Then add the solution folder nestings.
                    var nestedProjectsGlobalSection = Instances.GlobalSectionOperator.Acquire_NestedProjects(solutionFile);

                    var projectNestings = projectIdentitiesByFilePath.Values
                        .Select(projectIdentity =>
                        {
                            var projectNesting = new ProjectNesting()
                            {
                                ChildProjectIdentity = projectIdentity,
                                ParentProjectIdentity = solutionFolderIdentity,
                            };

                            return projectNesting;
                        });

                    nestedProjectsGlobalSection.ProjectNestings.AddRange(projectNestings);

                    return projectIdentitiesByFilePath;
                });

            return projectIdentitiesByFilePath;
        }

        public Guid Acquire_SolutionFolderIdentity(
            SolutionFile solutionFile,
            string solutionFolderName)
        {
            var hasSolutionFolder = this.Has_SolutionFolder(solutionFile, solutionFolderName);

            var solutionFolderIdentity = hasSolutionFolder
                ? hasSolutionFolder.Result.ProjectIdentity
                : this.AddSolutionFolder(
                    solutionFile,
                    solutionFolderName)
                ;

            return solutionFolderIdentity;
        }

        /// <summary>
        /// Gets all project references that are not solution folders.
        /// </summary>
        public ProjectFileReference[] Get_NonSolutionFolderProjectFileReferences(SolutionFile solutionFile)
        {
            var output = solutionFile.ProjectFileReferences
                .WhereIsNotSolutionFolder()
                .ToArray();

            return output;
        }

        /// <summary>
        /// Quality-of-life overload for <see cref="Get_NonSolutionFolderProjectFileReferences(SolutionFile)"/>.
        /// <para><inheritdoc cref="Get_NonSolutionFolderProjectFileReferences(SolutionFile)" path="/summary"/></para>
        /// </summary>
        public ProjectFileReference[] Get_ProjectFileReferences(SolutionFile solutionFile)
        {
            var output = this.Get_NonSolutionFolderProjectFileReferences(solutionFile);
            return output;
        }

        public string[] Get_ProjectReferenceFilePaths(
            IEnumerable<ProjectFileReference> projectFileReferences,
            string solutionFilePath)
        {
            var projectFileReferecePaths = projectFileReferences
                .Select(projectFileReference => Instances.PathOperator.GetProjectFilePath(
                    solutionFilePath,
                    projectFileReference.ProjectRelativeFilePath))
                .ToArray();

            return projectFileReferecePaths;
        }

        public string[] Get_ProjectReferenceFilePaths(
            SolutionFile solutionFile,
            string solutionFilePath)
        {
            var projectFileReferences = solutionFile.GetProjectFileReferences();

            var projectFileReferecePaths = projectFileReferences
                .Select(projectFileReference => Instances.PathOperator.GetProjectFilePath(
                    solutionFilePath,
                    projectFileReference.ProjectRelativeFilePath))
                .ToArray();

            return projectFileReferecePaths;
        }

        public string[] Get_ProjectReferenceFilePaths(
            string solutionFilePath)
        {
            var projectReferenceFilePaths = Instances.SolutionFileOperator.InReadContext(
                solutionFilePath,
                Instances.SolutionFileOperator.Get_ProjectReferenceFilePaths);

            return projectReferenceFilePaths;
        }

        public Guid Get_SolutionFolderIdentity(SolutionFile solutionFile, string solutionFolderName)
        {
            var hasSolutionFolder = this.Has_SolutionFolder(solutionFile, solutionFolderName);
            if (!hasSolutionFolder)
            {
                throw new Exception($"No solution found. No solution folder with name '{solutionFolderName}' found.");
            }

            var output = hasSolutionFolder.Result.ProjectIdentity;
            return output;
        }

        public Guid Get_SolutionIdentity(SolutionFile solutionFile)
        {
            var extensibilityGlobals = Instances.GlobalSectionOperator.Get_ExtensibilityGlobals(solutionFile);

            var output = extensibilityGlobals.SolutionIdentity;
            return output;
        }

        public WasFound<ProjectFileReference> Has_SolutionFolder(SolutionFile solutionFile, string solutionFolderName)
        {
            var solutionFolderOrDefault = solutionFile.ProjectFileReferences
                .Where(x => x.ProjectTypeIdentity == Instances.ProjectTypeIdentities.SolutionFolder && x.ProjectName == solutionFolderName)
                // Use robust First() even though there should only be a single instance.
                .FirstOrDefault();

            var output = WasFound.From(solutionFolderOrDefault);
            return output;
        }

        public void AddSolutionFolder(
            string solutionFilePath,
            string solutionFolderName,
            Guid solutionFolderIdentity)
        {
            this.InModifyContext_Synchronous(solutionFilePath,
                (solutionFile, _) =>
                {
                    var projectFileReference = new ProjectFileReference
                    {
                        ProjectTypeIdentity = Instances.ProjectTypeIdentities.SolutionFolder,
                        ProjectName = solutionFolderName,
                        ProjectRelativeFilePath = solutionFolderName,
                        ProjectIdentity = solutionFolderIdentity,
                    };

                    this.AddProjectReferenceOnly(solutionFile, projectFileReference);
                });
        }

        public void AddSolutionFolder(
            string solutionFilePath,
            string solutionFolderName)
        {
            this.InModifyContext_Synchronous(solutionFilePath,
                (solutionFile, _) =>
                {
                    this.AddSolutionFolder(solutionFile, solutionFolderName);
                });
        }

        public Guid AddSolutionFolder(
            SolutionFile solutionFile,
            string solutionFolderName)
        {
            // Abuse the AddProjectReferenceOnly() logic for the solution folder.
            var solutionFolderIdentity = this.AddProjectReferenceOnly(
                solutionFile,
                Instances.ProjectTypeIdentities.SolutionFolder,
                solutionFolderName,
                solutionFolderName);

            return solutionFolderIdentity;
        }

        /// <summary>
        /// Adds on the project reference (not any of the project configurations, project nestings, etc.).
        /// </summary>
        public void AddProjectReferenceOnly(
            SolutionFile solutionFile,
            ProjectFileReference projectFileReference)
        {
            solutionFile.ProjectFileReferences.Add(projectFileReference);
        }

        /// <inheritdoc cref="AddProjectReferenceOnly(SolutionFile, ProjectFileReference)"/>
        public Guid AddProjectReferenceOnly(
            SolutionFile solutionFile,
            Guid projectTypeIdentity,
            string projectName,
            string projectRelativePath)
        {
            var projectIdentity = Instances.GuidOperator.New();

            this.AddProjectReferenceOnly(
                solutionFile,
                projectTypeIdentity,
                projectName,
                projectRelativePath,
                projectIdentity);

            return projectIdentity;
        }

        /// <inheritdoc cref="AddProjectReferenceOnly(SolutionFile, ProjectFileReference)"/>
        public void AddProjectReferenceOnly(
            SolutionFile solutionFile,
            Guid projectTypeIdentity,
            string projectName,
            string projectRelativePath,
            Guid projectIdentity)
        {
            var projectFileReference = new ProjectFileReference
            {
                ProjectTypeIdentity = projectTypeIdentity,
                ProjectRelativeFilePath = projectRelativePath,
                ProjectName = projectName,
                ProjectIdentity = projectIdentity,
            };

            this.AddProjectReferenceOnly(
                solutionFile,
                projectFileReference);
        }

        /// <inheritdoc cref="AddProject(SolutionFile, string, string)"/>
        public Guid AddProject(
            string solutionFilePath,
            string projectFilePath)
        {
            var projectIdentity = this.InModifyContext_Synchronous(solutionFilePath,
                (solutionFile, _) =>
                {
                    var projectIdentity = this.AddProject(
                        solutionFile,
                        solutionFilePath,
                        projectFilePath);

                    return projectIdentity;
                });

            return projectIdentity;
        }

        public Dictionary<string, Guid> AddProjects(
            string solutionFilePath,
            IEnumerable<string> projectFilePaths)
        {
            var projectIdentitiesByFilePath = this.InModifyContext_Synchronous(solutionFilePath,
                (solutionFile, _) =>
                {
                    var projectIdentitiesByFilePath = this.AddProjects(
                        solutionFile,
                        solutionFilePath,
                        projectFilePaths);

                    return projectIdentitiesByFilePath;
                });

            return projectIdentitiesByFilePath;
        }

        /// <inheritdoc cref="AddProject(SolutionFile, string, string, Guid)"/>
        public Guid AddProject(
            SolutionFile solutionFile,
            string solutionFilePath,
            string projectFilePath)
        {
            var projectIdentity = Instances.GuidOperator.New();

            this.AddProject(
                solutionFile,
                solutionFilePath,
                projectFilePath,
                projectIdentity);

            return projectIdentity;
        }

        public Dictionary<string, Guid> AddProjects(
            SolutionFile solutionFile,
            string solutionFilePath,
            IEnumerable<string> projectFilePaths)
        {
            var projectIdentitiesByFilePath = projectFilePaths
                .ToDictionary(
                    projectFilePath => projectFilePath,
                    _ => Instances.GuidOperator.New());

            this.AddProjects(
                solutionFile,
                solutionFilePath,
                projectIdentitiesByFilePath);

            return projectIdentitiesByFilePath;
        }

        public void AddProject(
            string solutionFilePath,
            string projectFilePath,
            Guid projectIdentity)
        {
            this.InModifyContext_Synchronous(solutionFilePath,
                (solutionFile, _) =>
                {
                    this.AddProject(
                        solutionFile,
                        solutionFilePath,
                        projectFilePath,
                        projectIdentity);
                });
        }

        /// <summary>
        /// Simply adds a project file reference to a solution.
        /// (Does not update recursive project reference dependencies.)
        /// </summary>
        public void AddProject(
            SolutionFile solutionFile,
            string solutionFilePath,
            string projectFilePath,
            Guid projectIdentity)
        {
            var projectRelativeFilePath = Instances.PathOperator.GetProjectRelativeFilePath(
                solutionFilePath,
                projectFilePath);

            var hasProjectAlready = Internal.HasProject(
                solutionFile,
                projectRelativeFilePath);
            if (hasProjectAlready)
            {
                throw new InvalidOperationException($"Cannot add project. Solution already has project: '{projectFilePath}'.");
            }

            // Add the project.
            var projectFileNameStem = Instances.PathOperator._Base.Get_FileNameStem(projectFilePath);

            // The project name is just the file name stem of the project file.
            var projectName = projectFileNameStem;

            var projectTypeIdentity = Instances.ProjectFileOperator.GetProjectTypeIdentity_ForSolutionFile(projectFilePath);

            var projectFileReference = new ProjectFileReference
            {
                ProjectIdentity = projectIdentity,
                ProjectName = projectName,
                ProjectRelativeFilePath = projectRelativeFilePath,
                ProjectTypeIdentity = projectTypeIdentity,
            };

            Instances.SolutionFileOperator_Internal.AddProjectReference(solutionFile, projectFileReference);

            // Add values to global sections.
            var solutionConfigurationPlatforms = Instances.GlobalSectionOperator.Acquire_SolutionConfigurationPlatforms(solutionFile);

            var projectConfigurationPlatforms = Instances.GlobalSectionOperator.Acquire_ProjectConfigurationPlatforms(solutionFile);

            Instances.GlobalSectionOperator.AddProjectConfigurations(
                projectConfigurationPlatforms,
                projectIdentity,
                solutionConfigurationPlatforms);
        }

        public void AddProjects(
            SolutionFile solutionFile,
            string solutionFilePath,
            IDictionary<string, Guid> projectIdentitiesByFilePath)
        {
            var projectRelativeFilePathsByFilePath = projectIdentitiesByFilePath
                .ToDictionary(
                    xPair => xPair.Key,
                    xPair => Instances.PathOperator.GetProjectRelativeFilePath(
                        solutionFilePath,
                        xPair.Key));

            var wasFoundByProjectFilePath = Internal.HasProjects(
                solutionFile,
                solutionFilePath,
                projectIdentitiesByFilePath.Keys);

            if (wasFoundByProjectFilePath.Values.Any_Found())
            {
                var alreadyAddProjectRelativeFilePaths = wasFoundByProjectFilePath.Values.ValuesFound()
                    .Select(x => x.ProjectRelativeFilePath);

                var text = Instances.StringOperator.Join(
                    Instances.Strings.NewLine_ForEnvironment,
                    alreadyAddProjectRelativeFilePaths);

                throw new InvalidOperationException($"Cannot add project. Solution already has projects:\n{text}");
            }

            // Add the project file references.
            foreach (var pair in projectIdentitiesByFilePath)
            {
                var projectFilePath = pair.Key;
                var projectIdentity = pair.Value;
                var projectRelativeFilePath = projectRelativeFilePathsByFilePath[projectFilePath];

                var projectFileNameStem = Instances.PathOperator._Base.Get_FileNameStem(projectFilePath);

                // The project name is just the file name stem of the project file.
                var projectName = projectFileNameStem;

                var projectTypeIdentity = Instances.ProjectFileOperator.GetProjectTypeIdentity_ForSolutionFile(projectFilePath);

                var projectFileReference = new ProjectFileReference
                {
                    ProjectIdentity = projectIdentity,
                    ProjectName = projectName,
                    ProjectRelativeFilePath = projectRelativeFilePath,
                    ProjectTypeIdentity = projectTypeIdentity,
                };

                Instances.SolutionFileOperator_Internal.AddProjectReference(solutionFile, projectFileReference);
            }

            // Add values to global sections.
            var solutionConfigurationPlatforms = Instances.GlobalSectionOperator.Acquire_SolutionConfigurationPlatforms(solutionFile);

            var projectConfigurationPlatforms = Instances.GlobalSectionOperator.Acquire_ProjectConfigurationPlatforms(solutionFile);

            foreach (var pair in projectIdentitiesByFilePath)
            {
                var projectIdentity = pair.Value;

                Instances.GlobalSectionOperator.AddProjectConfigurations(
                    projectConfigurationPlatforms,
                    projectIdentity,
                    solutionConfigurationPlatforms);
            }
        }

        public SolutionFile Deserialize(string solutionFilePath)
        {
            var output = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);
            return output;
        }

        /// <summary>
        /// Choose <see cref="HasProject_ByFilePath(string, string)"/> as the default for the function name.
        /// </summary>
        public bool HasProject(string solutionFilePath, string projectFilePath)
        {
            var output = this.HasProject_ByFilePath(solutionFilePath, projectFilePath);
            return output;
        }

        public bool HasProject_ByFilePath(string solutionFilePath, string projectFilePath)
        {
            var solutionFile = this.Deserialize(solutionFilePath);

            var projectRelativeFilePath = Instances.PathOperator.GetProjectRelativeFilePath(
                solutionFilePath,
                projectFilePath);

            var output = solutionFile.ProjectFileReferences
                .Where(x => x.ProjectRelativeFilePath == projectRelativeFilePath)
                // Use robust Any(), allowing for multiple.
                .Any();

            return output;
        }

        public bool HasProject_ByName(string solutionFilePath, string projectName)
        {
            var solutionFile = this.Deserialize(solutionFilePath);

            var output = solutionFile.ProjectFileReferences
                .Where(x => x.ProjectName == projectName)
                // Use robust Any(), allowing for multiple.
                .Any();

            return output;
        }

        public bool HasProject(string solutionFilePath, Guid projectIdentity)
        {
            var solutionFile = this.Deserialize(solutionFilePath);

            var output = solutionFile.ProjectFileReferences
                .Where(x => x.ProjectIdentity == projectIdentity)
                // Use robust Any(), allowing for multiple.
                .Any();

            return output;
        }

        public NestedProjectsGlobalSection Get_NestedProjectsGlobalSection(SolutionFile solutionFile)
        {
            var nestedProjectsGlobalSection = Instances.GlobalSectionOperator.Get_NestedProjects(solutionFile);
            return nestedProjectsGlobalSection;
        }

        public WasFound<NestedProjectsGlobalSection> Has_NestedProjectsGlobalSection(SolutionFile solutionFile)
        {
            var wasFound = Instances.GlobalSectionOperator.Has_NestedProjects(solutionFile);
            return wasFound;
        }

        public void InModifyContext_Synchronous(string solutionFilePath,
            Action<SolutionFile> solutionFileAction)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            solutionFileAction(solutionFile);

            Instances.SolutionFileSerializer.Serialize_Synchronous(solutionFilePath, solutionFile);
        }

        public void InModifyContext_Synchronous(string solutionFilePath,
            Action<SolutionFile, string> solutionFileAction)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            solutionFileAction(solutionFile, solutionFilePath);

            Instances.SolutionFileSerializer.Serialize_Synchronous(solutionFilePath, solutionFile);
        }

        public async Task InModifyContext(string solutionFilePath,
                Func<SolutionFile, string, Task> solutionFileAction)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            await solutionFileAction(solutionFile, solutionFilePath);

            Instances.SolutionFileSerializer.Serialize_Synchronous(solutionFilePath, solutionFile);
        }

        public TOutput InModifyContext_Synchronous<TOutput>(string solutionFilePath,
            Func<SolutionFile, string, TOutput> solutionFileFunction)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            var output = solutionFileFunction(solutionFile, solutionFilePath);

            Instances.SolutionFileSerializer.Serialize_Synchronous(solutionFilePath, solutionFile);

            return output;
        }

        public async Task<TOutput> InModifyContext<TOutput>(string solutionFilePath,
            Func<SolutionFile, string, Task<TOutput>> solutionFileFunction)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            var output = await solutionFileFunction(solutionFile, solutionFilePath);

            Instances.SolutionFileSerializer.Serialize_Synchronous(solutionFilePath, solutionFile);

            return output;
        }

        public async Task InQueryContext(string solutionFilePath,
            Func<SolutionFile, Task> solutionFileAction = default)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            await Instances.ActionOperator.Run(
                solutionFile,
                solutionFileAction);
        }

        public async Task<TOutput> InQueryContext<TOutput>(string solutionFilePath,
            Func<SolutionFile, Task<TOutput>> solutionFileAction = default)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            var output = await Instances.ActionOperator.Run(
                solutionFile,
                solutionFileAction);

            return output;
        }

        public void InQueryContext_Synchronous(string solutionFilePath,
            Action<SolutionFile> solutionFileAction = default)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            Instances.ActionOperator.Run(
                solutionFile,
                solutionFileAction);
        }

        public TOutput InQueryContext_Synchronous<TOutput>(string solutionFilePath,
            Func<SolutionFile, TOutput> solutionFileAction = default)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            var output = Instances.ActionOperator.Run(
                solutionFile,
                solutionFileAction);

            return output;
        }

        public void InReadContext(string solutionFilePath,
            Action<SolutionFile, string> solutionFileAction)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            solutionFileAction(solutionFile, solutionFilePath);
        }

        public Task InReadContext(string solutionFilePath,
            Func<SolutionFile, string, Task> solutionFileAction)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            return solutionFileAction(solutionFile, solutionFilePath);
        }

        public TOutput InReadContext<TOutput>(string solutionFilePath,
            Func<SolutionFile, string, TOutput> solutionFileFunction)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            var output = solutionFileFunction(solutionFile, solutionFilePath);
            return output;
        }

        public Task<TOutput> InReadContext<TOutput>(string solutionFilePath,
            Func<SolutionFile, string, Task<TOutput>> solutionFileFunction)
        {
            var solutionFile = Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

            return solutionFileFunction(solutionFile, solutionFilePath);
        }

        /// <summary>
        /// Examines file context to determine if a file is a solution file.
        /// </summary>
        public bool IsSolutionFile(string possibleSolutionFilePath)
        {
            // File exists?
            Instances.FileSystemOperator.VerifyFileExists(possibleSolutionFilePath);

            // Solution files should have the byte-order-mark.
            var hasByteOrderMark = Instances.FileOperator.Has_ByteOrderMark(possibleSolutionFilePath);

            if (!hasByteOrderMark)
            {
                return false;
            }

            // Solution file should have a second line.
            var lines = Instances.FileOperator.ReadAllLines_Synchronous(possibleSolutionFilePath);

            var secondLine = lines.Second();

            // And the second line should start with "Microsoft Visual Studio Solution File".
            var correctSecondLineStart = secondLine.StartsWith(
                Instances.Strings.MicrosoftVisualStudioSolutionFile);

            if (!correctSecondLineStart)
            {
                return false;
            }

            // Success.
            return true;
        }

        public void RemoveProject(SolutionFile solutionFile, string solutionFilePath, string projectFilePath)
        {
            // Check to see if the project even exists.
            var hasProject = Internal.HasProject_ByProjectFilePath(
                solutionFile,
                solutionFilePath,
                projectFilePath);

            if (!hasProject)
            {
                throw new InvalidOperationException($"Cannot remove project. Solution already has project: '{projectFilePath}'.");
            }

            // Else, remove the project.
            // Remove the project file reference.
            solutionFile.ProjectFileReferences.Remove(hasProject.Result);

            // Remove the build configuration platforms for the project.
            var projectConfigurationPlatforms = Instances.GlobalSectionOperator.Get_ProjectConfigurationPlatforms(solutionFile);

            var projectIdentityToRemove = hasProject.Result.ProjectIdentity;

            var projectBuildConfigurationMappingsToRemove = projectConfigurationPlatforms.ProjectBuildConfigurationMappings
                .Where(x => x.ProjectIdentity == projectIdentityToRemove)
                .ToArray();

            foreach (var projectBuildConfigurationMapping in projectBuildConfigurationMappingsToRemove)
            {
                projectConfigurationPlatforms.ProjectBuildConfigurationMappings.Remove(projectBuildConfigurationMapping);
            }

            // If a nested project mapping for the project exists, remove it.
            var hasNestedProjects = Instances.GlobalSectionOperator.Has_NestedProjects(solutionFile);
            if (hasNestedProjects)
            {
                var nestedProjects = hasNestedProjects.Result;

                var projectNestingsToRemove = nestedProjects.ProjectNestings
                    .Where(x => x.ChildProjectIdentity == projectIdentityToRemove)
                    .ToArray();

                foreach (var projectNesting in projectNestingsToRemove)
                {
                    nestedProjects.ProjectNestings.Remove(projectNesting);
                }
            }
        }

        public void RemoveProject(string solutionFilePath, string projectFilePath)
        {
            this.InModifyContext_Synchronous(solutionFilePath,
                (solutionFile, solutionFilePath) =>
                {
                    this.RemoveProject(solutionFile, solutionFilePath, projectFilePath);
                });
        }

        public void Serialize(
            string solutionFilePath,
            SolutionFile solutionFile)
        {
            Instances.SolutionFileSerializer.Serialize_Synchronous(solutionFilePath, solutionFile);
        }

        public void Set_SolutionIdentity(SolutionFile solutionFile, Guid solutionIdentity)
        {
            var extensibilityGlobals = Instances.GlobalSectionOperator.Get_ExtensibilityGlobals(solutionFile);

            extensibilityGlobals.SolutionIdentity = solutionIdentity;
        }

        /// <summary>
        /// Strict, in the sense that if the project file is not already included in the solution file, then an exception will be thrown.
		/// <para>The first project in the solution's projects list is the default startup project.
		/// (After opening a solution, a startup project can be set. But that setting is not contained within the solution, and is not checked into source control.)</para>
        /// </summary>
        public void Set_DefaultStartupProject(
            SolutionFile solutionFile,
            string solutionFilePath,
            string projectFilePath)
        {
            // Check to see if the project even exists.
            var projectFileReference = Internal.Verify_HasProject_ByProjectFilePath(
                solutionFile,
                solutionFilePath,
                projectFilePath);

            // Remove the project file reference.
            solutionFile.ProjectFileReferences.Remove(projectFileReference);

            // Then insert at the beginning.
            solutionFile.ProjectFileReferences.Insert(0, projectFileReference);
        }
    }
}
