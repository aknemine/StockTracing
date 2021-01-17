using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTracking.Business.ApiRequestClasses
{
    public class ApiUserRequest
    {
        public Guid? Id { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string Department { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime? BirtOfDate { get; set; }
        public string ThumbnailPhoto { get; set; }
        public bool? Genus { get; set; }
        public int AuthorityLevel { get; set; }
        public bool Deleted { get; set; }
    }
}
