using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class ItemPropertyping : EntityBaseMapping<Item> //ClassMapping<Item>
    {
        public ItemPropertyping()
            : base("wp_item")
        {
            /*
            Table("wp_item");

            Id(f => f.Id, map =>
                {
                    map.Column("id");
                    map.Generator(Generators.Sequence,
                                  gmap => gmap.Params(new {sequence = "wp_item_id_seq"}));
                });
             * */

            Property(x => x.PoNo, m => m.Column("po_no"));
            Property(x => x.Style, m => m.Column("style"));
            Property(x => x.StyleDesc, m => m.Column("style_desc"));
            Property(x => x.Color, m => m.Column("color"));
            Property(x => x.ColorDesc, m => m.Column("color_desc"));
            Property(x => x.Size, m => m.Column("size"));
            Property(x => x.SizeDesc, m => m.Column("size_desc"));
            Property(x => x.Vendor, m => m.Column("vendor"));
            Property(x => x.VendorCode, m => m.Column("vendor_code"));
            Property(x => x.Upc, m => m.Column("upc"));
            Property(x => x.Gtin, m => m.Column("gtin"));
            Property(x => x.Price, m => m.Column("price"));
            Property(x => x.Qty, m => m.Column("qty"));

            Property(x => x.Active, m => m.Column("active"));
            Property(x => x.Type, m => m.Column("type"));
            Property(x => x.CreatedId, m => m.Column("created_id"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));
            Property(x => x.ModifiedId, m => m.Column("modified_id"));
            Property(x => x.ModifiedTime, m => m.Column("modified_time"));
            Property(x => x.FileName, m => m.Column("file_name"));
        }
    }
}
