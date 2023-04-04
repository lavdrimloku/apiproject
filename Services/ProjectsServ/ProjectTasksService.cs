using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.ProjectsServ
{
    public class ProjectTasksService : IProjectTasksService
    {
        private readonly IRepository<ProjectTask> _repo;

        public ProjectTasksService(IRepository<ProjectTask> repo)
        {
            _repo = repo;
        }

        public void Delete(ProjectTask Item)
        {
            _repo.Delete(Item);
        }

        public List<ProjectTask> GetAll()
        {
            return _repo.GetAll();
        }

        public List<ProjectTask> GetAllTaskAssignedMe(Guid ProjectId, string AssigneId)
        {
            return _repo.Get(filter: x => x.ProjectId == ProjectId && x.AssigneId.ToString() == AssigneId).ToList();
        }

        public ProjectTask GetById(Guid Id)
        {
            return _repo.GetById(Id.ToString());
        }

        public void Insert(ProjectTask Item)
        {
            _repo.Insert(Item);
        }

        public void Update(ProjectTask Item)
        {
            _repo.Update(Item);
        }
    }
}
