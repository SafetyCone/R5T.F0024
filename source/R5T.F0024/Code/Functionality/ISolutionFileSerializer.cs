using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024
{
	[FunctionalityMarker]
	public partial interface ISolutionFileSerializer : IFunctionalityMarker
	{
		private Internal.ISolutionFileSerializer Internal => F0024.Internal.SolutionFileSerializer.Instance;


		public async Task Serialize(
			string solutionFilePath,
			SolutionFile solutionFile)
		{
			var text = Internal.Serialize_ToText(solutionFile);

			using var stream = Instances.FileStreamOperator.Open_Write(solutionFilePath);
			using var writer = Instances.StreamWriterOperator.NewLeaveOpenAddBOM(stream);

			await writer.WriteLineAsync(text);
		}

        public void Serialize_Synchronous(
            string solutionFilePath,
            SolutionFile solutionFile)
        {
            var text = Internal.Serialize_ToText(solutionFile);

            using var stream = Instances.FileStreamOperator.Open_Write(solutionFilePath);
            using var writer = Instances.StreamWriterOperator.NewLeaveOpenAddBOM(stream);

            writer.WriteLine(text);
        }

        public async Task<SolutionFile> Deserialize(string solutionFilePath)
		{
			var lines = await Instances.FileOperator.ReadAllLines(solutionFilePath);

			var solutionFile = Internal.Deserialize_FromLines(lines);
			return solutionFile;
        }

        public SolutionFile Deserialize_Synchronous(string solutionFilePath)
        {
            var lines = Instances.FileOperator.ReadAllLines_Synchronous(solutionFilePath);

            var solutionFile = Internal.Deserialize_FromLines(lines);
            return solutionFile;
        }
    }
}