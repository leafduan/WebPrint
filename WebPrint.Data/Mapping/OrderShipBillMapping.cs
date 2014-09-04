using System.Reflection;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class OrderShipBillMapping : EntityBaseMapping<OrderShipBill> //ClassMapping<OrderShipBill>
    {
        public OrderShipBillMapping()
            : base("wp_order_ship_bill")
        {
            /*
            Table("wp_order_ship_bill");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence,
                                gm => gm.Params(new {sequence = "wp_order_ship_bill_id_seq"}));
                });
             * */

            /*
            Property(x => x.ShipCompany, m=>m.Column("ship_company"));
            Property(x => x.ShipAttention, m=>m.Column("ship_attention"));
            Property(x => x.ShipAddress, m=>m.Column("ship_address"));
            Property(x => x.ShipAddress2, m=>m.Column("ship_address2"));
            Property(x => x.ShipAddress3, m=>m.Column("ship_address3"));
            Property(x => x.ShipCityTown, m=>m.Column("ship_city_town"));
            Property(x => x.ShipState, m=>m.Column("ship_state"));
            Property(x => x.ShipZipCode, m=>m.Column("ship_zip_code"));
            Property(x => x.ShipCountry, m=>m.Column("ship_country"));
            Property(x => x.ShipPhone, m=>m.Column("ship_phone"));
            Property(x => x.ShipFax, m=>m.Column("ship_fax"));
            Property(x => x.ShipEmail, m=>m.Column("ship_email"));
            Property(x => x.ShipRemark, m=>m.Column("ship_remark"));
            Property(x => x.BillCompany, m=>m.Column("bill_company"));
            Property(x => x.BillAttention, m=>m.Column("bill_attention"));
            Property(x => x.BillAddress, m=>m.Column("bill_address"));
            Property(x => x.BillAddress2, m=>m.Column("bill_address2"));
            Property(x => x.BillAddress3, m=>m.Column("bill_address3"));
            Property(x => x.BillCityTown, m=>m.Column("bill_city_town"));
            Property(x => x.BillState, m=>m.Column("bill_state"));
            Property(x => x.BillZipCode, m=>m.Column("bill_zip_code"));
            Property(x => x.BillCountry, m=>m.Column("bill_country"));
            Property(x => x.BillPhone, m=>m.Column("bill_phone"));
            Property(x => x.BillFax, m=>m.Column("bill_fax"));
            Property(x => x.BillEmail, m=>m.Column("bill_email"));
            Property(x => x.BillRemark, m=>m.Column("bill_remark"));
             * */

            Component(x => x.ShipAddress, c =>
                {
                    c.Property(x => x.Company, m => m.Column("ship_company"));
                    c.Property(x => x.Attention, m => m.Column("ship_attention"));
                    c.Property(x => x.Address1, m => m.Column("ship_address"));
                    c.Property(x => x.Address2, m => m.Column("ship_address2"));
                    c.Property(x => x.Address3, m => m.Column("ship_address3"));
                    c.Property(x => x.CityTown, m => m.Column("ship_city_town"));
                    c.Property(x => x.State, m => m.Column("ship_state"));
                    c.Property(x => x.ZipCode, m => m.Column("ship_zip_code"));
                    c.Property(x => x.Country, m => m.Column("ship_country"));
                    c.Property(x => x.Phone, m => m.Column("ship_phone"));
                    c.Property(x => x.Fax, m => m.Column("ship_fax"));
                    c.Property(x => x.Email, m => m.Column("ship_email"));
                    c.Property(x => x.Remark, m => m.Column("ship_remark"));
                });

            Component(x => x.BillAddress, c =>
                {
                    c.Property(x => x.Company, m => m.Column("bill_company"));
                    c.Property(x => x.Attention, m => m.Column("bill_attention"));
                    c.Property(x => x.Address1, m => m.Column("bill_address"));
                    c.Property(x => x.Address2, m => m.Column("bill_address2"));
                    c.Property(x => x.Address3, m => m.Column("bill_address3"));
                    c.Property(x => x.CityTown, m => m.Column("bill_city_town"));
                    c.Property(x => x.State, m => m.Column("bill_state"));
                    c.Property(x => x.ZipCode, m => m.Column("bill_zip_code"));
                    c.Property(x => x.Country, m => m.Column("bill_country"));
                    c.Property(x => x.Phone, m => m.Column("bill_phone"));
                    c.Property(x => x.Fax, m => m.Column("bill_fax"));
                    c.Property(x => x.Email, m => m.Column("bill_email"));
                    c.Property(x => x.Remark, m => m.Column("bill_remark"));
                });

            Property(x => x.CreatedTime, m => m.Column("created_time"));

            /*
            // 如果order ship bill的主键就是外键(关联order表id)，则此种方式配置
            // 且Id用Generators.Foreign()而不是Generators.Sequence
            OneToOne(x => x.Order, m =>
                {
                    m.Cascade(Cascade.Persist);
                    m.Constrained(true);
                    m.Lazy(LazyRelation.Proxy);
                    m.PropertyReference(typeof (Order).GetProperty("ShipBill", BindingFlags.IgnoreCase));
                    m.Access(Accessor.Property);
                });
             * */

            OneToOne(x => x.Order, m =>
            {
                m.Cascade(Cascade.Persist);
                // 延迟加载 必须选项，表示先保存order 再保存order ship bill
                m.Constrained(true);
                m.Lazy(LazyRelation.Proxy);
                //m.PropertyReference(typeof(Order).GetProperty("ShipBill", BindingFlags.IgnoreCase));
                // NHibernate 4.0
                m.Class(typeof(Order));
                m.Access(Accessor.Property);
            });
        }
    }
}
