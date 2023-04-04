using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.StatusServ
{
    public interface IStatusService
    {
        Status GetById(Guid Id);
        List<Status> GetAll();
        void Delete(Status Item);
        void Update(Status Item);
        void Insert(Status Item);
    }
}
