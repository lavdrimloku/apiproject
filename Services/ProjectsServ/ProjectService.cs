using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ProjectsServ
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repo;

        public ProjectService(IRepository<Project> repo)
        {
            _repo = repo;
        }

        public void Delete(Project Item)
        {
            _repo.Delete(Item);
        }

        public List<Project> GetAll()
        {
            return _repo.GetAll();
        }

        public Project GetById(Guid Id)
        {
            return _repo.GetById(Id.ToString());
        }

        public void Insert(Project Item)
        {
            _repo.Insert(Item);
        }

        public void Update(Project Item)
        {
            _repo.Update(Item);
        }
    }
}