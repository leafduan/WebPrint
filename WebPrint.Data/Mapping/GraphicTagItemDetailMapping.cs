using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class GraphicTagItemDetailMapping : EntityBaseMapping<GraphicTagItemDetail> //ClassMapping<GraphicTagItemDetail>
    {
        public GraphicTagItemDetailMapping()
            : base("wp_graphic_tag_item_detail")
        {
            /*
            Table("wp_graphic_tag_item_detail");

            Id(x => x.Id, m =>
            {
                m.Column("id");
                m.Generator(Generators.Sequence, gm => gm.Params(new { sequence = "wp_graphic_tag_item_detail_id_seq" }));
            });
            * */

            Property(x => x.TierQty, m => m.Column("tier_qty"));
            Property(x => x.Price, m => m.Column("price"));
            Property(x=>x.UomName,m=>m.Column("uom_name"));
            Property(x=>x.UomQty,m=>m.Column("uom_qty"));
            Property(x => x.RoundQty, m => m.Column("round_qty"));
            Property(x => x.Active, m => m.Column("active"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));

            ManyToOne(x=>x.Item, m =>
                {
                    m.Column("item_id");
                    m.Class(typeof(GraphicTagItem));
                    m.Lazy(LazyRelation.Proxy);
                    m.Cascade(Cascade.Persist);
                    m.NotNullable(true);
                });
        }
    }
}
