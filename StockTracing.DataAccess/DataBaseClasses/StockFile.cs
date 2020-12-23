using System;

namespace StockTracking.DataAccess.DatabaseClasses
{
    public class StockFile : LogTableBase
    {
        public Guid stockId { get; set; }
        public string fileDescription { get; set; }
        public string file { get; set; }
    }
}
