using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long CustomerID { get; set; }      
        public string Name { get; set; }
        public virtual List<CustomerContact> CustomerContacts { get; set; }
        public virtual List<Order> Orders { get; set; }

        [Required]
        public string SystemUserCreate { get; set; }
        public DateTime DTsCreate { get; set; }

        public string SystemUserUpdate { get; set; }
        public DateTime? DTsUpdate { get; set; }

    }
}
