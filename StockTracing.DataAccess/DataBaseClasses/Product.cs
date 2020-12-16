using System;
using System.Collections.Generic;
using System.Text;

namespace StockTracing.DataAccess.DataBaseClasses
{
    public class Product:LogTableBase
    {
        public string categoryName { get; set; }
        public string barcodeNo { get; set; }
        public int criticalLevel { get; set; }
        public string genus { get; set; }
        public Guid categoryId { get; set; }
        public int inStock { get; set; }

    }
}
