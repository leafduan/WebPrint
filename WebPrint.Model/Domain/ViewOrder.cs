using System;

namespace WebPrint.Model
{
    public class ViewOrder : EntityBase
    {
        public virtual string JobNo { get; protected set; }

        public virtual string PoNo { get; protected set; }

        public virtual OrderType Type { get; protected set; }

        public virtual string VendorCode { get; protected set; }

        public virtual string Remark { get; protected set; }

        public virtual string CompletionDate { get; protected set; }

        public virtual OrderStatus Status { get; protected set; }

        public virtual string SoNo { get; protected set; }

        public virtual DateTime CreatedTime { get; protected set; }

        public virtual string Username { get; protected set; }

        public virtual string PrintShopName { get; protected set; }

        public virtual int DetailsCount { get; protected set; }

        public virtual int TotQty { get; protected set; }
    }
}
