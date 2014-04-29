using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class UpcMapping : EntityBaseMapping<Upc> //ClassMapping<Upc>
    {
        public UpcMapping()
            : base("wp_upc")
        {
            /*
            Table("wp_upc");
            
            Id(u => u.Id, map =>
                {
                    map.Column("id");
                    map.Generator(Generators.Sequence,
                                  gmap => gmap.Params(new {sequence = "wp_upc_id_seq"}));
                });
             * */

            Property(u => u.UpcValue, map => map.Column("upc"));
            Property(u => u.Gtin, map => map.Column("gtin"));
            Property(u => u.LastSerialNumber, map => map.Column("last_serial_number"));
            Property(u => u.Active, map => map.Column("active"));
            Property(u => u.CreatedId, map => map.Column("created_id"));
            Property(u => u.CreatedTime, map => map.Column("created_time"));
            Property(u => u.ModifiedId, map => map.Column("modified_id"));
            Property(u => u.ModifiedTime, map => map.Column("modified_time"));
        }
    }
}
