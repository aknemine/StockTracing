using System;
using System.Collections.Generic;
using System.Text;

namespace StockTracing.DataAccess.DataBaseClasses
{
    public class CompanyUser
    {
        public Guid companyId { get; set; }
        public Guid userId { get; set; }
        public DateTime lastUpdate { get; set; } = DateTime.Now;
        public string editedBy { get; set; }
        public bool deleted { get; set; } = false;
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
    }
}
