using System;

namespace StockTracking.DataAccess.DatabaseClasses
{
    public class Category : LogTableBase
    {
        public short type { get; set; }
        public string name { get; set; }
        public Guid parentId { get; set; }
    }
}
