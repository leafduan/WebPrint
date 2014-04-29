using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class SizeColorMapMapping : EntityBaseMapping<SizeColorMap> //ClassMapping<SizeColorMap>
    {
        public SizeColorMapMapping()
            : base("wp_size_color")
        {
            /*
            Table("wp_size_color");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence,
                                gm => gm.Params(new {sequence = "wp_size_color_id_seq"}));
                });
             * */

            Property(x => x.Size, m=>m.Column("size"));
            Property(x => x.PmsColor, m=>m.Column("pms_color"));
            Property(x => x.Version, m=>m.Column("version"));
            Property(x => x.Red, m=>m.Column("red"));
            Property(x => x.Green, m=>m.Column("green"));
            Property(x => x.Blue, m=>m.Column("blue"));
            Property(x => x.SizeType, m=>m.Column("size_type"));
            Property(x => x.SortNo, m=>m.Column("sort_no"));
            Property(x => x.CreatedTime, m=>m.Column("created_time"));
        }
    }
}
