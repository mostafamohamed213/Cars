using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.ViewModels
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        [Required]
        public string Chases { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        public bool WithMaintenance { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        //public long? DraftId  { get; set; }
        //public bool saveDraft { get; set; }
    }
}
