using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Consts
{
    public static class TablesMaxRows
    {
        public static int IndexOrdersMaxRows { get; set; }
        public static int IndexOrdersDraftMaxRows { get; set; }
        public static int IndexOrderLinesMaxRows { get; set; }
        static TablesMaxRows()
        {
            IndexOrdersMaxRows = 10;
            IndexOrdersDraftMaxRows = 10;
            IndexOrderLinesMaxRows = 10;
        }
    }
}
