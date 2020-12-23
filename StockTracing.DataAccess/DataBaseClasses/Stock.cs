using System;

namespace StockTracking.DataAccess.DatabaseClasses
{
    public class Stock : LogTableBase
    {
        public Guid incomingCompanyId { get; set; }
        public Guid outcomingCompanyId { get; set; }
        public Guid? shippingCompanyId { get; set; }
        public Guid? shippingUserId { get; set; }
        public string invoiceNo { get; set; }
        public string barcodeNo { get; set; }
        public Guid receiverPerson { get; set; }
        public Guid deliveryPerson { get; set; }
        public DateTime date { get; set; }
        public bool? confirmStatus { get; set; }
        public Guid confirmById { get; set; }
        public DateTime confirmDate { get; set; }
        public string confirmNote { get; set; }
    }
}
