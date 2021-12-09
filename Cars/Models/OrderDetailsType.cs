using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Models
{
    public class OrderDetailsType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OrderDetailsTypeID { get; set; }
        [Required]
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Details { get; set; }    
        public virtual List<OrderDetails> OrderDetails { get; set; }

        [Required]
        public string SystemUserCreate { get; set; }
        public DateTime DTsCreate { get; set; }
        public string SystemUserUpdate { get; set; }
        public DateTime? DTsUpdate { get; set; }
    }
}
