using System;
using System.IO;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface ISolutionFileGenerator : IFunctionalityMarker
	{
		public SolutionFile CreateNew(VisualStudioVersion version)
        {
			var solutionFile = version switch
			{
				VisualStudioVersion.Version_2019 => this.CreateNew_2019(),
				VisualStudioVersion.Version_2022 => this.CreateNew_2022(),
				_ => throw Instances.EnumerationHelper.GetSwitchDefaultCaseException(version),
			};

			return solutionFile;
        }

		public SolutionFile CreateNew_2019()
		{
			var solutionFile = new SolutionFile()
			.WithVersionInformation(Instances.VersionInformationGenerator.Get2019_Default)
			.AddGlobalSection(Instances.GlobalSectionGenerator.SolutionProperties_GetDefault)
			.AddGlobalSection(Instances.GlobalSectionGenerator.ExtensibilityGlobals_GetDefault)
			;

			return solutionFile;
		}

		public SolutionFile CreateNew_2019(Action<SolutionFile> modifier)
		{
			var solutionFile = this.CreateNew(
				this.CreateNew_2019,
				modifier);

			return solutionFile;
		}

		public SolutionFile CreateNew_2022()
		{
			var solutionFile = new SolutionFile()
			.WithVersionInformation(Instances.VersionInformationGenerator.Get2022_Default)
			.AddGlobalSection(Instances.GlobalSectionGenerator.SolutionProperties_GetDefault)
			.AddGlobalSection(Instances.GlobalSectionGenerator.ExtensibilityGlobals_GetDefault)
			;

			return solutionFile;
		}

		public SolutionFile CreateNew_2022(Action<SolutionFile> modifier)
		{
			var solutionFile = this.CreateNew(
				this.CreateNew_2022,
				modifier);

			return solutionFile;
		}

		/// <summary>
		/// Chooses <see cref="CreateNew_2022()"/> as the default.
		/// </summary>
		public SolutionFile CreateNew()
        {
			var solutionFile = this.CreateNew_2022();
			return solutionFile;
        }

		public SolutionFile CreateNew(Action<SolutionFile> modifier)
		{
			var solutionFile = this.CreateNew();

			modifier(solutionFile);

			return solutionFile;
		}

		public SolutionFile CreateNew(Func<SolutionFile> constructor, Action<SolutionFile> modifier)
		{
			var solutionFile = constructor();

			modifier(solutionFile);

			return solutionFile;
		}

		/// <inheritdoc cref="CreateNew()"/>
		public void Create_New(string solutionFilePath)
		{
			var solutionFile = this.CreateNew();

			Instances.SolutionFileSerializer.Serialize_Synchronous(
				solutionFilePath,
				solutionFile);
		}

		/// <summary>
		/// Creates a new, empty solution file. (VS 2022)
		/// </summary>
		public void CreateNew(string solutionFilePath)
		{
			var solutionGuid = Instances.GuidOperator.New();

			var text =
$@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.2.32630.192
MinimumVisualStudioVersion = 10.0.40219.1
Global
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {Instances.GuidOperator.ToString_ForSolutionFile(solutionGuid)}
	EndGlobalSection
EndGlobal
";
			this.WriteText(
				solutionFilePath,
				text,
				// No trimming, somehow the Visual Studio blank solution template begins and ends with a new line.
				false);
		}

		public void WriteText(
			string filePath,
			string text,
			bool trim = true)
		{
			// Trim text.
			var outputText = trim
				? text.Trim()
				: text
				;

			// Write text synchronously.
			using var stream = Instances.FileStreamOperator.Open_Write(filePath);
			using var writer = Instances.StreamWriterOperator.NewLeaveOpenAddBOM(stream);

			writer.WriteLine(outputText);
		}
	}
}