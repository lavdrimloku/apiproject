using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ProjectsServ
{
    public interface IProjectService
    {
        Project GetById(Guid Id);
        List<Project> GetAll();
        void Delete(Project Item);
        void Update(Project Item);
        void Insert(Project Item);
    }
}
