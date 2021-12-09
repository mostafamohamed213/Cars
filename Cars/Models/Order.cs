using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long OrderID { get; set; }
        public bool? WithMaintenance { get; set; }
        public string Prefix { get; set; }
        
        public long CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public  Customer Customer { get; set; }
       
        public long VehicleID { get; set; }
        [ForeignKey("VehicleID")]
        public  Vehicle Vehicle { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
        public long EmployeeBranchID { get; set; }

        [Required]
        public string SystemUserCreate { get; set; }
        public DateTime DTsCreate { get; set; }

        public string SystemUserUpdate { get; set; }
        public DateTime? DTsUpdate { get; set; }
    }
}
