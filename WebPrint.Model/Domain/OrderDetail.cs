using System;
using System.Collections.Generic;

namespace WebPrint.Model
{
    public enum OrderDetailStatus
    {
        Cancelled = 0,
        New,
        Printing,
        Completed
    }

    public enum ProductionType
    {
        Thermal = 0,
        Offset
    }

    public class OrderDetail : EntityBase
    {
        public OrderDetail()
        {
            //PrintHistories = new List<PrintHistory>();
        }

        //public virtual int OrderId { get; set; }

        public virtual int OriginalQty { get; set; }

        public virtual int Qty { get; set; }

        public virtual int SpareQty { get; set; }

        public virtual int RemainQty { get; set; }

        public virtual long EpcSerialBegin { get; set; }

        public virtual long EpcSerialEnd { get; set; }

        public virtual string EpcCodeBegin { get; set; }

        public virtual string EpcCodeEnd { get; set; }

        public virtual string PrintBeginEpc { get; set; }

        public virtual OrderDetailStatus Status { get; set; }

        public virtual int LineNo { get; set; }

        public virtual string Remark { get; set; }

        public virtual string Upc { get; set; }

        public virtual string Gtin { get; set; }

        public virtual string PoNo { get; set; }

        public virtual string Style { get; set; }

        public virtual string StyleDesc { get; set; }

        public virtual string Color { get; set; }

        public virtual string ColorDesc { get; set; }

        public virtual string Size { get; set; }

        public virtual string SizeDesc { get; set; }

        public virtual string Vendor { get; set; }

        public virtual string VendorCode { get; set; }

        public virtual string Logo { get; set; }

        public virtual string Unit { get; set; }

        public virtual string Price { get; set; }

        public virtual ProductionType ProductionType { get; set; }

        public virtual string Desc { get; set; }

        //public virtual int ItemId { get; set; }

        public virtual string GraphicCategory { get; set; }

        public virtual string GraphicName { get; set; }

        public virtual string GraphicCode { get; set; }

        public virtual string GraphicPreviewImage { get; set; }

        public virtual string GraphicSurcharge { get; set; }

        public virtual string GraphicLeadTime { get; set; }

        public virtual string GraphicTier { get; set; }

        public virtual string GraphicUom { get; set; }

        //public virtual int GraphicTagItemDetailId { get; set; }

        public virtual short Active { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual int CreatedId { get; set; }

        public virtual DateTime ModifiedTime { get; set; }

        public virtual int ModifiedId { get; set; }

        public virtual string FileName { get; set; }

        public virtual Order Order { get; set; }

        // 记录可能会很多 而且不用类型记录保存一张数据表，因此去掉关联
        //public virtual IList<PrintHistory> PrintHistories { get; protected set; }

        public virtual Format Format { get; set; }
    }
}
