using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class OrderDetailMapping : EntityBaseMapping<OrderDetail> //ClassMapping<OrderDetail>
    {
        public OrderDetailMapping()
            : base("wp_order_detail")
        {
            /*
            Table("wp_order_detail");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence,
                                gm => gm.Params(new {sequence = "wp_order_detail_id_seq"}));
                });
             * */

            //Property(x => x.OrderId, m => m.Column("order_id"));
            Property(x => x.OriginalQty, m => m.Column("original_qty"));
            Property(x => x.Qty, m => m.Column("qty"));
            Property(x => x.SpareQty, m => m.Column("spare_qty"));
            Property(x => x.RemainQty, m => m.Column("remain_qty"));
            Property(x => x.EpcSerialBegin, m => m.Column("epc_serial_begin"));
            Property(x => x.EpcSerialEnd, m => m.Column("epc_serial_end"));
            Property(x => x.EpcCodeBegin, m => m.Column("epc_code_begin"));
            Property(x => x.EpcCodeEnd, m => m.Column("epc_code_end"));
            Property(x => x.PrintBeginEpc, m => m.Column("print_begin_epc"));
            Property(x => x.Status, m =>
                {
                    m.Column("status");
                    m.Type<EnumStringType<OrderDetailStatus>>();
                });
            Property(x=>x.LineNo,m=>m.Column("line_no"));
            Property(x => x.Remark, m => m.Column("remark"));

            Property(x => x.Upc, m => m.Column("upc"));
            Property(x => x.Gtin, m => m.Column("gtin"));
            Property(x => x.PoNo, m => m.Column("po_no"));
            Property(x => x.Style, m => m.Column("style"));
            Property(x => x.StyleDesc, m => m.Column("style_desc"));
            Property(x => x.Color, m => m.Column("color"));
            Property(x => x.ColorDesc, m => m.Column("color_desc"));
            Property(x => x.Size, m => m.Column("size"));
            Property(x => x.SizeDesc, m => m.Column("size_desc"));
            Property(x => x.Vendor, m => m.Column("vendor"));
            Property(x => x.VendorCode, m => m.Column("vendor_code"));
            Property(x => x.Logo, m => m.Column("logo"));
            Property(x => x.Unit, m => m.Column("unit"));
            Property(x => x.Price, m => m.Column("price"));
            Property(x => x.ProductionType, m =>
                {
                    m.Column("production_type");
                    m.Type<EnumStringType<ProductionType>>();
                });
            // 对应数据库 sql 关键字的 用`(即 ~ 键上对应的点)包括起来就行
            Property(x => x.Desc, m => m.Column("`desc`"));
            //Property(x => x.ItemId, m => m.Column("item_id"));

            Property(x => x.GraphicCategory, m => m.Column("graphic_category"));
            Property(x => x.GraphicName, m => m.Column("graphic_name"));
            Property(x => x.GraphicCode, m => m.Column("graphic_code"));
            Property(x => x.GraphicPreviewImage, m => m.Column("graphic_preview_image"));
            Property(x => x.GraphicSurcharge, m => m.Column("graphic_surcharge"));
            Property(x => x.GraphicLeadTime, m => m.Column("graphic_lead_time"));
            Property(x => x.GraphicTier, m => m.Column("graphic_tier"));
            Property(x => x.GraphicUom, m => m.Column("graphic_uom"));
            //Property(x => x.GraphicTagItemDetailId, m => m.Column("graphic_tag_item_detail_id"));

            Property(x => x.Active, m => m.Column("active"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));
            Property(x => x.CreatedId, m => m.Column("created_id"));
            Property(x => x.ModifiedTime, m => m.Column("modified_time"));
            Property(x => x.ModifiedId, m => m.Column("modified_id"));
            Property(x => x.FileName, m => m.Column("file_name"));

            ManyToOne(x => x.Order, m =>
                {
                    m.Column("order_id");
                    m.Cascade(Cascade.Persist);
                    m.Lazy(LazyRelation.Proxy);
                    m.Class(typeof (Order));
                });

            /*
            // one order detail has many print histories
            Bag(x => x.PrintHistories, c =>
                {
                    c.Table("wp_print_history");
                    c.BatchSize(100);
                    c.Cascade(Cascade.Persist);
                    // best pratice, set true, set the control to the many (if false,the control to the one)
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("order_detail_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });

                },
                // one of the relation mappings (to be described separately)
                r => r.OneToMany(m => m.Class(typeof (PrintHistory))));
             * */

            // many order details have one format
            ManyToOne(x => x.Format, m =>
                {
                    m.Column("format_id");
                    m.Class(typeof (Format));
                    //m.ForeignKey("format_id");
                    m.Cascade(Cascade.Persist); /* Database first model, 数据库的设置不能很好的反应在映射上 */
                    m.Lazy(LazyRelation.Proxy);
                    m.NotNullable(true);
                });
        }
    }
}
