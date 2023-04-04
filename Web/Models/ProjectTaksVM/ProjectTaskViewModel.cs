using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.ProjectTaksVM
{
    public class ProjectTaskViewModel
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }


        public Guid? ProjectId { get; set; }

        public Guid? AssigneId { get; set; }

        public Guid? StatusId { get; set; }
        public DateTime? FinishedDate { get; set; }
    } 
    
    public class AddProjectTaskViewModel
    {
        public string Summary { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? AssigneId { get; set; }
        public Guid? StatusId { get; set; }
        public DateTime? FinishedDate { get; set; }
    }
    
    public class UpdateProjectTaskViewModel
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? AssigneId { get; set; }
        public Guid? StatusId { get; set; }
        public DateTime? FinishedDate { get; set; }
    }

}
