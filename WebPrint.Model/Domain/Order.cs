using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebPrint.Model
{
    public enum OrderType
    {
        [Description("Service Bureau")] ServiceBureau = 0,
        [Description("In Plant Printing")] InPlantPrinting,
        [Description("Stage Po")] StagedPo,
        [Description("Pass Through Po")] PassThrough,
        // Graphic Tag Order, Order Blank Stock
        [Description("Catalog Po")] CatalogPo
    }

    public enum OrderStatus
    {
        [Description("Cancelled")] Cancelled = 0,
        [Description("Pending Approved")] PendingApproved,
        [Description("Approved")] Approved,
        [Description("Open")] Open,
        [Description("Printing")] Printing,
        [Description("Completed")] Completed
    }

    public class Order : EntityBase
    {
        public Order()
        {
            Details = new List<OrderDetail>();
        }

        public virtual int AssociatedOrderId { get; set; }

        public virtual string JobNo { get; set; }

        public virtual string PoNo { get; set; }

        public virtual OrderType Type { get; set; }

        public virtual string VendorCode { get; set; }

        public virtual string Remark { get; set; }

        public virtual string Wastage { get; set; }

        public virtual short Production { get; set; }

        public virtual string CompletionDate { get; set; }

        public virtual OrderStatus Status { get; set; }

        public virtual string SoNo { get; set; }

        public virtual short Active { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual DateTime ModifiedTime { get; set; }

        public virtual string FileName { get; set; }

        public virtual OrderShipBill ShipBill { get; set; }

        public virtual IList<OrderDetail> Details { get; protected set; }

        public virtual User CreatedUser { get; set; }

        public virtual User ModifiedUser { get; set; }

        public virtual PrintShop PrintShop { get; set; }
    }
}
