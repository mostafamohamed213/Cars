using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Models
{
    public class OrderDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long OrderDetailsID { get; set; }
        [Required] 
        public string Items { get; set; }
        public int Quantity { get; set; }
        public bool?  IsApproved { get; set; }
        public string Prefix { get; set; }
        public int BranchID { get; set; }
        public long OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        public int OrderDetailsTypeID { get; set; }
        [ForeignKey("OrderDetailsTypeID")]
        public  OrderDetailsType OrderDetailsType { get; set; }
    
        public int? LayerID { get; set; }
        [ForeignKey("LayerID")]
        public Layer Layer { get; set; }

        // flase = save as draft
        // true =save 
        // null = save order and order details without click button save or save as draft
        public bool? Enabled { get; set; }
        [Required]
        public string SystemUserCreate { get; set; }
        public DateTime DTsCreate { get; set; }

        public string SystemUserUpdate { get; set; }
        public DateTime? DTsUpdate { get; set; }
    }

}
