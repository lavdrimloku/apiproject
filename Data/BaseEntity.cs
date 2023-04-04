using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            IsDeleted = false;
            CreatedDate = DateTime.Now;
        }

        [Column("CreatedById")]
        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }
        public virtual AppUser CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [Column("LastChangedById")]
        [ForeignKey("LastChangedBy")]
        public string LastChangedById { get; set; }
        public virtual AppUser LastChangedBy { get; set; }

        public DateTime LastChangedDate { get; set; }

        [Column("DeletedById")]
        [ForeignKey("DeletedBy")]
        public string DeletedById { get; set; }
        public virtual AppUser DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
