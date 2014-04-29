using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class PermissionMapping : EntityBaseMapping<Permission> //ClassMapping<Permission>
    {
        public PermissionMapping()
            : base("wp_permission")
        {
            /*
            Table("wp_permission");

            Id(x => x.Id, m =>
            {
                m.Column("id");
                m.Generator(Generators.Sequence,
                            gm => gm.Params(new { sequence = "wp_permission_id_seq" }));
            });
            * */

            Property(x => x.Name, m=>m.Column("name"));
            Property(x => x.DisplayName, m=>m.Column("display_name"));
            Property(x => x.CreatedTime, m=>m.Column("created_time"));
        }
    }
}
