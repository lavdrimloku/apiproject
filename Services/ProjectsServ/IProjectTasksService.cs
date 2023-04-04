using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ProjectsServ
{
    public interface IProjectTasksService
    {
        ProjectTask GetById(Guid Id);
        List<ProjectTask> GetAll();
        List<ProjectTask> GetAllTaskAssignedMe(Guid ProjectId, string AssigneId);
        void Delete(ProjectTask Item);
        void Update(ProjectTask Item);
        void Insert(ProjectTask Item);
    }
}
