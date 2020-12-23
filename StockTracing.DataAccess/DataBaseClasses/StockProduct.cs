using System;

namespace StockTracking.DataAccess.DatabaseClasses
{
    public class stockProduct : LogTableBase
    {
        public Guid stockId { get; set; }
        public string serialNumber { get; set; }
        public int qunatity { get; set; }
    }
}
