using System;
using System.Threading.Tasks;

using R5T.T0132;

using R5T.F0024.T001;


namespace R5T.F0024.F001
{
    [FunctionalityMarker]
    public partial interface ISolutionFileSerializer : IFunctionalityMarker
    {
#pragma warning disable IDE1006 // Naming Styles
        private Internal.ISolutionFileSerializer _Internal => Internal.SolutionFileSerializer.Instance;
#pragma warning restore IDE1006 // Naming Styles


        public async Task Serialize(
            string solutionFilePath,
            SolutionFile solutionFile)
        {
            var text = _Internal.Serialize_ToText(solutionFile);

            using var stream = Instances.FileStreamOperator.Open_Write(solutionFilePath);
            using var writer = Instances.StreamWriterOperator.New_LeaveOpen_AddByteOrderMarks(stream);

            await writer.WriteLineAsync(text);
        }

        public void Serialize_Synchronous(
            string solutionFilePath,
            SolutionFile solutionFile)
        {
            var text = _Internal.Serialize_ToText(solutionFile);

            using var stream = Instances.FileStreamOperator.Open_Write(solutionFilePath);
            using var writer = Instances.StreamWriterOperator.New_LeaveOpen_AddByteOrderMarks(stream);

            writer.WriteLine(text);
        }

        public async Task<SolutionFile> Deserialize(string solutionFilePath)
        {
            var lines = await Instances.FileOperator.ReadAllLines(solutionFilePath);

            var solutionFile = _Internal.Deserialize_FromLines(lines);
            return solutionFile;
        }

        public SolutionFile Deserialize_Synchronous(string solutionFilePath)
        {
            var lines = Instances.FileOperator.ReadAllLines_Synchronous(solutionFilePath);

            var solutionFile = _Internal.Deserialize_FromLines(lines);
            return solutionFile;
        }
    }
}
