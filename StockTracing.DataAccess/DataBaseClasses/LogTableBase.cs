using System;

namespace StockTracking.DataAccess.DatabaseClasses
{
    public class LogTableBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime lastUpdate { get; set; } = DateTime.Now;
        public Guid editedBy { get; set; }
        public bool deleted { get; set; } = false;
        public DateTime createdDate { get; set; }
        public Guid createdBy { get; set; }
    }
}
