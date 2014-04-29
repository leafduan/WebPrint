using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class GraphicTagCategoryMapping : EntityBaseMapping<GraphicTagCategory> //ClassMapping<GraphicTagCategory>
    {
        public GraphicTagCategoryMapping()
            : base("wp_graphic_tag_category")
        {
            /*
            Table("wp_graphic_tag_category");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence, gm => gm.Params(new {sequence = "wp_graphic_tag_category_id_seq"}));
                });
             * */

            Property(x=>x.Name,m=>m.Column("name"));
            Property(x=>x.DisplayName,m=>m.Column("display_name"));
            Property(x=>x.Active,m=>m.Column("active"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));
            
            Bag(x => x.Items, c =>
                {
                    c.Table("wp_graphic_tag_item");
                    c.Cascade(Cascade.Persist);
                    c.BatchSize(100);
                    c.Lazy(CollectionLazy.Lazy);
                    c.Inverse(true);

                    c.Key(k =>
                        {
                            k.Column("category_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });
                }, r => r.OneToMany(m => m.Class(typeof (GraphicTagItem))));
        }
    }
}
