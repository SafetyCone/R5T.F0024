using System;


namespace R5T.F0024.T001
{
    public abstract class SectionBase : ISection
    {
        public string Name { get; set; }
        public string PreOrPost { get; set; }


        public override string ToString()
        {
            var representation = this.Name;
            return representation;
        }
    }
}
