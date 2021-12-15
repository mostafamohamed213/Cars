using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Models
{
    public class UsersLogs
    {
        public string UserIP { get; set; }
        public string UserCity { get; set; }

        public string UserRegion { get; set; }

        public DateTime CreateDts { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }


    }
}
