using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class GraphicTagItemMapping : EntityBaseMapping<GraphicTagItem> //ClassMapping<GraphicTagItem>
    {
        public GraphicTagItemMapping()
            : base("wp_graphic_tag_item")
        {
            /*
            Table("wp_graphic_tag_item");

            Id(f => f.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence,
                                gm => gm.Params(new {sequence = "wp_graphic_tag_item_id_seq"}));
                });
             * */

            Property(x => x.Name, m => m.Column("name"));
            Property(x => x.Code, m => m.Column("code"));
            Property(x => x.Size, m => m.Column("size"));
            Property(x => x.PreviewImage, m => m.Column("preview_image"));
            Property(x => x.Desc, m => m.Column("`desc`"));
            Property(x => x.Surcharge, m => m.Column("surcharge"));
            Property(x => x.LeadTime, m => m.Column("lead_time"));
            Property(x => x.SortNo, m => m.Column("sort_no"));
            Property(x => x.Active, m => m.Column("active"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));
            
            ManyToOne(x=>x.Category, m =>
                {
                    m.Column("category_id");
                    m.Class(typeof(GraphicTagCategory));
                    m.Cascade(Cascade.Persist);
                    m.NotNullable(false);
                    m.Lazy(LazyRelation.Proxy);
                });
            
            Bag(x => x.Details, c =>
                {
                    c.Table("wp_graphic_tag_item_detail");
                    c.Cascade(Cascade.Persist);
                    c.Inverse(true);
                    c.BatchSize(100);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("item_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });
                }, r => r.OneToMany(m => m.Class(typeof (GraphicTagItemDetail))));
        }
    }
}
