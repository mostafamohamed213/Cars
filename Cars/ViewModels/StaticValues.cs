using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.ViewModels
{
    public static class StaticValues
    {
        public static bool RTL { get; set; }
        static StaticValues(){
            RTL = false;
       }
    }
}
