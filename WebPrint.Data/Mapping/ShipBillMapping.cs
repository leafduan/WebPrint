using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class ShipBillMapping : EntityBaseMapping<ShipBill> //ClassMapping<ShipBill>
    {
        public ShipBillMapping()
            : base("wp_ship_bill")
        {
            /*
            Table("wp_ship_bill");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence, gm => gm.Params(new {sequence = "wp_ship_bill_id_seq"}));
                });
             * */

            
            /*
            Property(x => x.Company, m => m.Column("company"));
            Property(x => x.Attention, m => m.Column("attention"));
            Property(x => x.Address, m => m.Column("address"));
            Property(x => x.Address2, m => m.Column("address2"));
            Property(x => x.Address3, m => m.Column("address3"));
            Property(x => x.CityTown, m => m.Column("city_town"));
            Property(x => x.State, m => m.Column("state"));
            Property(x => x.ZipCode, m => m.Column("zip_code"));
            Property(x => x.Country, m => m.Column("country"));
            Property(x => x.Phone, m => m.Column("phone"));
            Property(x => x.Fax, m => m.Column("fax"));
            Property(x => x.Email, m => m.Column("email"));
            Property(x => x.Remark, m => m.Column("remark"));
             * */
            
            Component(x=>x.Address, c =>
                {
                    c.Property(x => x.Company, m => m.Column("company"));
                    c.Property(x => x.Attention, m => m.Column("attention"));
                    c.Property(x => x.Address1, m => m.Column("address"));
                    c.Property(x => x.Address2, m => m.Column("address2"));
                    c.Property(x => x.Address3, m => m.Column("address3"));
                    c.Property(x => x.CityTown, m => m.Column("city_town"));
                    c.Property(x => x.State, m => m.Column("state"));
                    c.Property(x => x.ZipCode, m => m.Column("zip_code"));
                    c.Property(x => x.Country, m => m.Column("country"));
                    c.Property(x => x.Phone, m => m.Column("phone"));
                    c.Property(x => x.Fax, m => m.Column("fax"));
                    c.Property(x => x.Email, m => m.Column("email"));
                    c.Property(x => x.Remark, m => m.Column("remark"));
                });

            Property(x => x.Type, m => m.Column("type"));
            Property(x => x.Active, m => m.Column("active"));
            Property(x => x.CreatedId, m => m.Column("created_id"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));
            Property(x => x.ModifiedId, m => m.Column("modified_id"));
            Property(x => x.ModifiedTime, m => m.Column("modified_time"));
        }
    }
}
