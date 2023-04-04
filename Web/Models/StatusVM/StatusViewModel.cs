using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.StatusVM
{
    public class StatusViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }    
    
    public class AddStatusViewModel
    {
        public string Name { get; set; }
    } 
    
    public class UpdateStatusViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
