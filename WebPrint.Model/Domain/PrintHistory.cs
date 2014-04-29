using System;

namespace WebPrint.Model
{
    public class PrintHistory : EntityBase
    {
        public virtual int Qty { get; set; }

        public virtual string EpcCodeBegin { get; set; }

        public virtual string EpcCodeEnd { get; set; }

        public virtual string ClientIp { get; set; }

        public virtual string PrinterName { get; set; }

        public virtual string FormatName { get; set; }

        public virtual string Upc { get; set; }

        public virtual string Message { get; set; }

        public virtual short Active { get; set; }

        public virtual string Type { get; set; }

        public virtual int CreatedId { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
    }
}
