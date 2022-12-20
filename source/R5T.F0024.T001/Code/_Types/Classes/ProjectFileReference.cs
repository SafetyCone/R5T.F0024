using System;
using System.Collections.Generic;


namespace R5T.F0024.T001
{
    public class ProjectFileReference
    {
        public Guid ProjectTypeIdentity { get; set; }
        public string ProjectName { get; set; }
        public string ProjectRelativeFilePath { get; set; }
        public Guid ProjectIdentity { get; set; }

        public List<IProjectSection> ProjectSections { get; private set; } = new List<IProjectSection>();


        public override string ToString()
        {
            var representation = $"{this.ProjectName}: {this.ProjectRelativeFilePath}";
            return representation;
        }
    }
}
