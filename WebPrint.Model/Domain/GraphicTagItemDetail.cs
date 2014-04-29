using System;

namespace WebPrint.Model
{
    public class GraphicTagItemDetail : EntityBase
    {
        public virtual int TierQty { get; set; }

        public virtual string Price { get; set; }

        public virtual string UomName { get; set; }

        public virtual int UomQty { get; set; }

        public virtual int RoundQty { get; set; }

        public virtual short Active { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual GraphicTagItem Item { get; set; }
    }
}
