using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class FormatMapping : EntityBaseMapping<Format>//ClassMapping<Format>
    {
        public FormatMapping()
            : base("wp_format")
        {
            /*
            Table("wp_format");

            Id(f => f.Id, map =>
                {
                    map.Column("id");
                    map.Generator(Generators.Sequence,
                                  gmap => gmap.Params(new {sequence = "wp_format_id_seq"}));
                });
             * */

            Property(x => x.CategoryId, m => m.Column("category_id"));
            Property(x => x.Name, m => m.Column("name"));
            Property(x => x.DisplayName, m => m.Column("display_name"));
            Property(x => x.Path, m => m.Column("path"));
            Property(x => x.PreviewImage, m => m.Column("preview_image"));
            Property(x => x.Code, m => m.Column("code"));
            Property(x => x.Type, m =>
                {
                    m.Column("type");
                    m.Type<EnumStringType<TicketType>>();
                });
            Property(x => x.CreatedId, m => m.Column("created_id"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));
            Property(x => x.ModifiedId, m => m.Column("modified_id"));
            Property(x => x.ModifiedTime, m => m.Column("modified_time"));
        }
    }
}
