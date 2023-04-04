using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.StatusServ
{
    public class StatusService : IStatusService
    {
        private readonly IRepository<Status> _repo;

        public StatusService(IRepository<Status> repo)
        {
            _repo = repo;
        }

        public void Delete(Status Item)
        {
            _repo.Delete(Item);
        }

        public List<Status> GetAll()
        {
            return _repo.GetAll();
        }

        public Status GetById(Guid Id)
        {
            return _repo.GetById(Id.ToString());
        }

        public void Insert(Status Item)
        {
            _repo.Insert(Item);
        }

        public void Update(Status Item)
        {
            _repo.Update(Item);
        }
    }
}
