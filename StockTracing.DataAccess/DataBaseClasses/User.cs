using System;
using System.Collections.Generic;
using System.Text;

namespace StockTracing.DataAccess.DataBaseClasses
{
    public class User : LogTableBase
    {
        public string accountName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string mobile { get; set; }
        public string phone { get; set; }
        public string eMail { get; set; }
        public Guid companyId { get; set; }
        public string department { get; set; }
        public string thumbnailPhoto { get; set; }
        public DateTime birthOfDate { get; set; }
        public bool genus { get; set; }
        public int authorityLevel { get; set; }
        
    }
}
