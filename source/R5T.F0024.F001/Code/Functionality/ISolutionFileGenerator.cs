using System;

using R5T.T0132;

using R5T.F0024.T001;
using System.Threading.Tasks;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface ISolutionFileGenerator : IFunctionalityMarker
    {
#pragma warning disable IDE1006 // Naming Styles
        public Implementations.ISolutionFileGenerator _Implementations => Implementations.SolutionFileGenerator.Instance;
#pragma warning restore IDE1006 // Naming Styles



        public SolutionFile New(VisualStudioVersion version)
        {
            var solutionFile = version switch
            {
                VisualStudioVersion.Version_2019 => this.New_2019(),
                VisualStudioVersion.Version_2022 => this.New_2022(),
                _ => throw Instances.SwitchOperator.Get_DefaultCaseException(version),
            };

            return solutionFile;
        }

        public SolutionFile New_2019()
        {
            var solutionFile = new SolutionFile()
            .WithVersionInformation(Instances.VersionInformationGenerator.Get2019_Default)
            .AddGlobalSection(Instances.GlobalSectionGenerator.SolutionProperties_GetDefault)
            .AddGlobalSection(Instances.GlobalSectionGenerator.ExtensibilityGlobals_GetDefault)
            ;

            return solutionFile;
        }

        public SolutionFile New_2019(Action<SolutionFile> modifier)
        {
            var solutionFile = this.New(
                this.New_2019,
                modifier);

            return solutionFile;
        }

        public SolutionFile New_2022()
        {
            var solutionFile = new SolutionFile()
            .WithVersionInformation(Instances.VersionInformationGenerator.Get2022_Default)
            .AddGlobalSection(Instances.GlobalSectionGenerator.SolutionProperties_GetDefault)
            .AddGlobalSection(Instances.GlobalSectionGenerator.ExtensibilityGlobals_GetDefault)
            ;

            return solutionFile;
        }

        public SolutionFile New_2022(Action<SolutionFile> modifier)
        {
            var solutionFile = this.New(
                this.New_2022,
                modifier);

            return solutionFile;
        }

        /// <summary>
        /// Chooses <see cref="New_2022()"/> as the default.
        /// </summary>
        public SolutionFile New()
        {
            var solutionFile = this.New_2022();
            return solutionFile;
        }

        public SolutionFile New(Action<SolutionFile> modifier)
        {
            var solutionFile = this.New();

            modifier(solutionFile);

            return solutionFile;
        }

        public SolutionFile New(Func<SolutionFile> constructor, Action<SolutionFile> modifier)
        {
            var solutionFile = constructor();

            modifier(solutionFile);

            return solutionFile;
        }

        public async Task New(string solutionFilePath)
        {
            var solutionFile = this.New();

            await Instances.SolutionFileSerializer.Serialize(
                solutionFilePath,
                solutionFile);
        }

        /// <inheritdoc cref="Implementations.ISolutionFileGenerator.New_UsingSolutionFileObject_Synchronous(string)"/>
        public void New_Synchronous(string solutionFilePath)
        {
            _Implementations.New_UsingSolutionFileObject_Synchronous(solutionFilePath);
        }
    }
}
