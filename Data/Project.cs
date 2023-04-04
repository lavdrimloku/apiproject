using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
   public  class Project : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NrTeams { get; set; }
        public bool IsActive { get; set; }
    }
}
