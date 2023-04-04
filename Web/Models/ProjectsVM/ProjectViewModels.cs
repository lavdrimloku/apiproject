using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.ProjectsVM
{
    public class ProjectViewModels
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NrTeams { get; set; }
        public bool IsActive { get; set; }
    }  
    
    public class AddProjectViewModels
    {
        public string Name { get; set; }
        public int NrTeams { get; set; }
        public bool IsActive { get; set; }
    }   
    
    public class UpdateProjectViewModels
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NrTeams { get; set; }
        public bool IsActive { get; set; }
    }

}
