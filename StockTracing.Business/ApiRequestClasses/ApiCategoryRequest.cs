using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTracking.Business.ApiRequestClasses
{
    public class ApiCategoryRequest
    {
        public Guid? Id { get; set; }
        public short Type { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
        public bool Deleted { get; set; }
    }
}
