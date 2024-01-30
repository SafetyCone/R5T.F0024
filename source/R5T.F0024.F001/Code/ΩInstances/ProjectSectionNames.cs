using System;


namespace R5T.F0024.F001
{
    public class ProjectSectionNames : IProjectSectionNames
    {
        #region Infrastructure

        public static IProjectSectionNames Instance { get; } = new ProjectSectionNames();


        private ProjectSectionNames()
        {
        }

        #endregion
    }
}
