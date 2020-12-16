using System;
using System.Collections.Generic;
using System.Text;

namespace StockTracing.DataAccess.DataBaseClasses
{
    public class Category : LogTableBase
    {
        public short type { get; set; }
        public string name { get; set; }
        public Guid parentId { get; set; }


    }
}
