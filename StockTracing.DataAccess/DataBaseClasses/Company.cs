﻿namespace StockTracking.DataAccess.DatabaseClasses
{
    public class Company : LogTableBase
    {
        public string name { get; set; }
        public string taxNo { get; set; }
        public string address { get; set; }
        public string eMail { get; set; }
        public string phone { get; set; }
        public string webSite { get; set; }
        public bool isShipping { get; set; }
    }
}
