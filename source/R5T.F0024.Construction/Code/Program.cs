using System;


namespace R5T.F0024.Construction
{
    class Program
    {
        static void Main()
        {
            Program.TestMethod();

            Instances.SolutionFileGeneratorDemonstrations.CreateNew();
        }

        private static void TestMethod()
        {
            string message = "Hello Jon!";

            Console.WriteLine(message);
        }
    }
}