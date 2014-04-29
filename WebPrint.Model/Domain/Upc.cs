using System;

namespace WebPrint.Model
{
    public class Upc : EntityBase
    {
        public virtual string UpcValue { get; set; }
        public virtual string Gtin { get; set; }
        public virtual int LastSerialNumber { get; set; }
        public virtual int Active { get; set; }
        public virtual int CreatedId { get; set; }
        public virtual DateTime CreatedTime { get; set; }
        public virtual int ModifiedId { get; set; }
        public virtual DateTime ModifiedTime { get; set; }
    }
}