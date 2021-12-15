using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Models
{
    public class DraftOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long DraftOrderID { get; set; }
        public string Chases { get; set; }
        public string Phone { get; set; }     
        public string Name { get; set; }     
        public string Brand { get; set; }       
        public string Model { get; set; }
        public string Year { get; set; }
        public long EmployeeBranchID { get; set; }
        public bool? WithMaintenance { get; set; }
        public bool Enable { get; set; }

        [Required]
        public string SystemUserCreate { get; set; }
        public DateTime DTsCreate { get; set; }

        public string SystemUserUpdate { get; set; }
        public DateTime? DTsUpdate { get; set; }
    }
}
