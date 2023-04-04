using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
    public class ProjectTask : BaseEntity
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }


        [Column("ProjectId")]
        [ForeignKey("Project")]
        public Guid? ProjectId { get; set; }
        public virtual Project Project { get; set; }


        [Column("AppUserId")]
        [ForeignKey("AppUser")]
        public Guid? AssigneId { get; set; }
        public virtual AppUser Assigne  { get; set; }


        [Column("Status")]
        [ForeignKey("Status")]
        public Guid? StatusId { get; set; }
        public virtual Status Status { get; set; }

        public DateTime? FinishedDate { get; set; }

    }
}
