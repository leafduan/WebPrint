using System;

namespace WebPrint.Model
{
    public class Item : EntityBase
    {
        public virtual string PoNo { get; set; }

        public virtual string Style { get; set; }

        public virtual string StyleDesc { get; set; }

        public virtual string Color { get; set; }

        public virtual string ColorDesc { get; set; }

        public virtual string Size { get; set; }

        public virtual string SizeDesc { get; set; }

        public virtual string Vendor { get; set; }

        public virtual string VendorCode { get; set; }

        public virtual string Upc { get; set; }

        public virtual string Gtin { get; set; }

        public virtual string Price { get; set; }
         
        public virtual int Qty { get; set; }

        public virtual short Active { get; set; }

        public virtual string Type { get; set; }

        public virtual int CreatedId { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual int ModifiedId { get; set; }

        public virtual DateTime ModifiedTime { get; set; }

        public virtual string FileName { get; set; }
    }
}
