using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTracking.Business.ApiRequestClasses
{
    public class ApiCompanyRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string TaxNo { get; set; }
        public string Address { get; set; }
        public string Eposta { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public bool Deleted { get; set; }
        public bool IsShipping { get; set; }
    }
}
