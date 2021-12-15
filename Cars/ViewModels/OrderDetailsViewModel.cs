using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.ViewModels
{
    public class OrderDetailsViewModel
    {
        public long OrderDetailsID { get; set; }
        public string Items { get; set; }
        public int Quantity { get; set; }
        public bool? IsApproved { get; set; }
        public string type { get; set; }
    }
}
