using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
     public class Status : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
