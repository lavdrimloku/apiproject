using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Privileges
{
    public class ActionRoot : BaseEntity, Privileges.IAuditTrail<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string CodeName { get; set; }
        public string NameAl { get; set; }
        public string NameSr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
    }
}
