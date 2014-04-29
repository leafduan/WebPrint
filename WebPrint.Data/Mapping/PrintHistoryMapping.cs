using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class PrintHistoryMapping : EntityBaseMapping<PrintHistory> //ClassMapping<PrintHistory>
    {
        public PrintHistoryMapping()
            : base("wp_print_history")
        {
            /*
            Table("wp_print_history");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence,
                                gm => gm.Params(new {sequence = "wp_print_history_id_seq"}));
                });
             * */

            Property(x => x.Qty, m => m.Column("qty"));
            Property(x => x.EpcCodeBegin, m => m.Column("epc_code_begin"));
            Property(x => x.EpcCodeEnd, m => m.Column("epc_code_end"));
            Property(x => x.ClientIp, m => m.Column("client_ip"));
            Property(x => x.PrinterName, m => m.Column("printer_name"));
            Property(x => x.FormatName, m => m.Column("format_name"));
            Property(x => x.Upc, m => m.Column("upc"));
            Property(x => x.Message, m => m.Column("message"));
            Property(x => x.Active, m => m.Column("active"));
            Property(x => x.Type, m => m.Column("type"));
            Property(x => x.CreatedId, m => m.Column("created_id"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));

            ManyToOne(x => x.OrderDetail, m =>
                {
                    //Table("wp_order_detail");
                    m.Column("order_detail_id");
                    //m.ForeignKey("order_detail_id");
                    m.Cascade(Cascade.Persist);
                    m.Lazy(LazyRelation.Proxy);
                    m.NotNullable(true);
                });
        }
    }
}
