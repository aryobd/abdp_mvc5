using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace olss.Data.Entities
{
    [Table("MaintenanceItem")]
    public partial class MaintenanceItem
    {
        [Key]
        public int IdMaintenanceItem { get; set; }
        public string MaintenanceItemName { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDraft { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
