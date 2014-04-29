using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class ViewOrderMapping : ClassMapping<ViewOrder>
    {
        public ViewOrderMapping()
        {
            Table("view_order");

            Property(x => x.Id, m => m.Column("id"));
            Property(x => x.JobNo, m => m.Column("job_no"));
            Property(x => x.PoNo, m => m.Column("po_no"));
            Property(x => x.Type, m =>
                {
                    m.Column("type");
                    m.Type<EnumStringType<OrderType>>();
                });
            Property(x => x.VendorCode, m => m.Column("vendor_code"));
            Property(x => x.Remark, m => m.Column("remark"));
            Property(x => x.CompletionDate, m => m.Column("completion_date"));
            Property(x => x.Status, m =>
                {
                    m.Column("status");
                    m.Type<EnumStringType<OrderStatus>>();
                });
            Property(x => x.SoNo, m => m.Column("so_no"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));

            Property(x => x.Username, m => m.Column("user_name"));
            Property(x => x.PrintShopName, m => m.Column("print_shop_name"));

            Property(x => x.DetailsCount, m => m.Column("details_count"));
            Property(x => x.TotQty, m => m.Column("tot_qty"));
        }
    }
}
