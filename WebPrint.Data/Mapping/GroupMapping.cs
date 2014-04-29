using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class GroupMapping : EntityBaseMapping<Group> //ClassMapping<Group>
    {
        public GroupMapping()
            : base("wp_group")
        {
            /*
            Table("wp_group");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence,
                                gm => gm.Params(new {sequence = "wp_group_id_seq"}));
                });
             * */

            Property(x => x.Name, m => m.Column("name"));
            Property(x => x.DisplayName, m => m.Column("display_name"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));

            /*
            // users
            Bag(x => x.Users, c =>
                {
                    c.Table("wp_user_group");
                    c.BatchSize(100);
                    c.Cascade(Cascade.Persist);
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("group_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });
                },
                r => r.ManyToMany(m =>
                    {
                        m.Column("user_id");
                        m.Lazy(LazyRelation.Proxy);
                        m.Class(typeof (User));
                    }));
             * */

            // permissions
            Bag(x => x.Permissions, c =>
                {
                    c.Table("wp_group_permission");
                    c.BatchSize(100);
                    c.Cascade(Cascade.Persist);
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("group_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });
                },
                r => r.ManyToMany(m =>
                    {
                        m.Column("permission_id");
                        m.Lazy(LazyRelation.Proxy);
                        m.Class(typeof (Permission));
                    }));

        }
    }
}
