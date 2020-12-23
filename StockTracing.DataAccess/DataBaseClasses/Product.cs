using System;

namespace StockTracking.DataAccess.DatabaseClasses
{
    public class Product : LogTableBase
    {
        public string name { get; set; }
        public string barcodeNo { get; set; }
        public int criticalLevel { get; set; }
        public string genus { get; set; }
        public Guid categoryId { get; set; }
        public int inStock { get; set; }
    }
}
