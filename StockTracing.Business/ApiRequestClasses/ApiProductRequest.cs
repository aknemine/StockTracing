using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTracking.Business.ApiRequestClasses
{
    public class ApiProductRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string BarcodeNo { get; set; }
        public int CriticalLevel { get; set; }
        public int InStock { get; set; }
        public string Genus { get; set; }
        public Guid CategoryId { get; set; }
        public bool Deleted { get; set; }
    }
}
