using System;
using System.Linq;

using R5T.F0000;
using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface ISolutionFileOperator : IFunctionalityMarker
	{
		private static Internal.ISolutionFileOperator Internal { get; } = F0024.Internal.SolutionFileOperator.Instance;


		public void AddProject_InSolutionFolder(
			string solutionFilePath,
			string projectFilePath,
			string solutionFolderName,
			Guid projectIdentity)
        {
			Internal.InReserializedContext(solutionFilePath,
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
			var projectIdentity = Internal.InReserializedContext(solutionFilePath,
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

		public Guid Get_SolutionFolderIdentity(SolutionFile solutionFile, string solutionFolderName)
        {
			var hasSolutionFolder = this.Has_SolutionFolder(solutionFile, solutionFolderName);
			if(!hasSolutionFolder)
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
			Internal.InReserializedContext(solutionFilePath,
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
			Internal.InReserializedContext(solutionFilePath,
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

		public void AddProjectReferenceOnly(
			SolutionFile solutionFile,
			ProjectFileReference projectFileReference)
        {
			solutionFile.ProjectFileReferences.Add(projectFileReference);
        }

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

		public Guid AddProject(
			string solutionFilePath,
			string projectFilePath)
        {
			var projectIdentity = Internal.InReserializedContext(solutionFilePath,
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

		public void AddProject(
			string solutionFilePath,
			string projectFilePath,
			Guid projectIdentity)
        {
			Internal.InReserializedContext(solutionFilePath,
				(solutionFile, _) =>
				{
					this.AddProject(
						solutionFile,
						solutionFilePath,
						projectFilePath,
						projectIdentity);
				});
		}

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
			if(hasProjectAlready)
            {
				throw new InvalidOperationException($"Cannot add project. Solution already has project: '{projectFilePath}'.");
            }

			// Add the project.
			var projectFileNameStem = Instances.PathOperator_Base.GetFileNameStem(projectFilePath);

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

		public SolutionFile Deserialize(string solutionFilePath)
		{
			var output = Instances.SolutionFileSerializer.Deserialize(solutionFilePath);
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

		public void RemoveProject(string solutionFilePath, string projectFilePath)
        {
			Internal.InReserializedContext(solutionFilePath,
				(solutionFile, _) =>
				{
					// Check to see if the project even exists.
					var projectRelativeFilePath = Instances.PathOperator.GetProjectRelativeFilePath(
						solutionFilePath,
						projectFilePath);

					var hasProject = Internal.HasProject(
						solutionFile,
						projectRelativeFilePath);
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
					if(hasNestedProjects)
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
				});
		}

		public void Serialize(
			string solutionFilePath,
			SolutionFile solutionFile)
        {
			Instances.SolutionFileSerializer.Serialize(solutionFilePath, solutionFile);
        }

		public void Set_SolutionIdentity(SolutionFile solutionFile, Guid solutionIdentity)
		{
			var extensibilityGlobals = Instances.GlobalSectionOperator.Get_ExtensibilityGlobals(solutionFile);

			extensibilityGlobals.SolutionIdentity = solutionIdentity;
		}
	}


	namespace Internal
    {
		public partial interface ISolutionFileOperator
        {
			public void AddGlobalSection(
				SolutionFile solutionFile,
				IGlobalSection globalSection)
            {
				solutionFile.GlobalSections.Add(globalSection);
            }

			public void AddProjectReference(
				SolutionFile solutionFile,
				ProjectFileReference projectFileReference)
            {
				solutionFile.ProjectFileReferences.Add(projectFileReference);
            }

			public WasFound<ProjectFileReference> HasProject(
				SolutionFile solutionFile,
				string solutionFilePath,
				string projectFilePath)
            {
				var projectRelativeFilePath = Instances.PathOperator.GetProjectRelativeFilePath(
					solutionFilePath,
					projectFilePath);

				var output = this.HasProject(
					solutionFile,
					projectRelativeFilePath);

				return output;
            }

			public WasFound<ProjectFileReference> HasProject(
				SolutionFile solutionFile,
				string projectRelativeFilePath)
            {
				var projectOrDefault = solutionFile.ProjectFileReferences
					.Where(x => x.ProjectRelativeFilePath == projectRelativeFilePath)
					// Use robust First() even though there should not be multiple.
					.FirstOrDefault();

				var output = WasFound.From(projectOrDefault);
				return output;
            }

			public void InReserializedContext(string solutionFilePath,
				Action<SolutionFile, string> solutionFileAction)
            {
				var solutionFile = Instances.SolutionFileSerializer.Deserialize(solutionFilePath);

				solutionFileAction(solutionFile, solutionFilePath);

				Instances.SolutionFileSerializer.Serialize(solutionFilePath, solutionFile);
			}

			public TOutput InReserializedContext<TOutput>(string solutionFilePath,
				Func<SolutionFile, string, TOutput> solutionFileFunction)
			{
				var solutionFile = Instances.SolutionFileSerializer.Deserialize(solutionFilePath);

				var output = solutionFileFunction(solutionFile, solutionFilePath);

				Instances.SolutionFileSerializer.Serialize(solutionFilePath, solutionFile);

				return output;
			}
		}
    }
}