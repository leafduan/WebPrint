using NHibernate.Mapping.ByCode;
using NHibernate.Type;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class OrderMapping : EntityBaseMapping<Order> //ClassMapping<Order>
    {
        public OrderMapping()
            : base("wp_order")
        {
            /*
            Table("wp_order");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence, gm => gm.Params(new {sequence = "wp_order_id_seq"}));
                });
             * */

            Property(x => x.AssociatedOrderId, m => m.Column("associated_order_id"));
            Property(x => x.JobNo, m => m.Column("job_no"));
            Property(x => x.PoNo, m => m.Column("po_no"));
            Property(x => x.Type, m =>
                {
                    m.Column("type");
                    m.Type<EnumStringType<OrderType>>();
                });
            Property(x => x.VendorCode, m => m.Column("vendor_code"));
            Property(x => x.Remark, m => m.Column("remark"));
            Property(x => x.Wastage, m => m.Column("wastage"));
            Property(x => x.Production, m => m.Column("production"));
            Property(x => x.CompletionDate, m => m.Column("completion_date"));
            Property(x => x.Status, m =>
                {
                    m.Column("status");
                    m.Type<EnumStringType<OrderStatus>>();
                });
            Property(x => x.SoNo, m => m.Column("so_no"));
            Property(x => x.Active, m => m.Column("active"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));
            Property(x => x.ModifiedTime, m => m.Column("modified_time"));
            Property(x => x.FileName, m => m.Column("file_name"));

            ManyToOne(x => x.ShipBill, m =>
                {
                    m.Column("order_ship_bill_id");
                    m.Cascade(Cascade.Persist);
                    m.Class(typeof (OrderShipBill));
                    m.Lazy(LazyRelation.Proxy);
                    // 必须 这样才能实现one to one
                    m.Unique(true);
                });

            Bag(x => x.Details, c =>
                {
                    c.Table("wp_order_detail");
                    c.Cascade(Cascade.Persist);
                    c.BatchSize(100);
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("order_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });
                },
                r => r.OneToMany(m => m.Class(typeof (OrderDetail))));


            ManyToOne(x => x.CreatedUser, m =>
                {
                    m.Column("created_id");
                    m.Cascade(Cascade.Persist);
                    m.Class(typeof (User));
                    m.Lazy(LazyRelation.Proxy);
                    m.NotNullable(true);
                });

            ManyToOne(x => x.ModifiedUser, m =>
                {
                    m.Column("modified_id");
                    m.Cascade(Cascade.Persist);
                    m.Class(typeof (User));
                    m.Lazy(LazyRelation.Proxy);
                    m.NotNullable(true);
                });

            ManyToOne(x => x.PrintShop, m =>
                {
                    m.Column("print_shop_id");
                    m.Cascade(Cascade.Persist);
                    m.Class(typeof (PrintShop));
                    m.Lazy(LazyRelation.Proxy);
                });
        }
    }
}
