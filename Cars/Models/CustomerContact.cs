using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Models
{
    public class CustomerContact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long CustomerContactID { get; set; }
        public long CustomerID{ get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
        [Required]      
        public string Phone { get; set; }
        public bool HasWhatsapp { get; set; }
        public bool HasTelegram { get; set; }


        [Required]
        public string SystemUserCreate { get; set; }
        public DateTime DTsCreate { get; set; }

        public string SystemUserUpdate { get; set; }
        public DateTime? DTsUpdate { get; set; }
    }
}
