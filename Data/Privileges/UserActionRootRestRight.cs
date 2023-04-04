using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Privileges
{
    public class UserActionRootRestRight: BaseEntity, Privileges.IAuditTrail<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string IdentityRoleId { get; set; }
        public virtual ApplicationRole IdentityRole { get; set; }

        public Guid ActionRootId { get; set; }
        public virtual ActionRoot ActionRoot { get; set; }

        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }
    }
}
