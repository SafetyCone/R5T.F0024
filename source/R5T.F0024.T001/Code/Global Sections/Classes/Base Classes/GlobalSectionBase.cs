using System;


namespace R5T.F0024.T001
{
    public abstract class GlobalSectionBase : IGlobalSection
    {
        public string Name { get; set; }
        public PreOrPostSolution PreOrPostSolution { get; set; }


        public override string ToString()
        {
            var representation = this.Name;
            return representation;
        }
    }
}
