using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class UserMapping : EntityBaseMapping<User> //ClassMapping<User>
    {
        public UserMapping()
            : base("wp_user")
        {
            /*
            Table("wp_user");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence,
                                gm => gm.Params(new {sequence = "wp_user_id_seq"}));
                });
             * */

            Property(x => x.UserName, m => m.Column("user_name"));
            Property(x => x.Password, m => m.Column("password"));
            Property(x => x.DisplayName, m => m.Column("display_name"));
            Property(x => x.Email, m => m.Column("email"));
            Property(x => x.VendorCode, m => m.Column("vendor_code"));
            Property(x => x.Active, m => m.Column("active"));
            Property(x => x.CreatedTime, m => m.Column("created_time"));
            Property(x => x.ModifiedTime, m => m.Column("modified_time"));

            // groups
            Bag(x => x.Groups, c =>
                {
                    c.Table("wp_user_group");
                    c.BatchSize(100);
                    c.Cascade(Cascade.Persist);
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("user_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });
                },
                r => r.ManyToMany(m =>
                    {
                        m.Column("group_id");
                        m.Lazy(LazyRelation.Proxy);
                        m.Class(typeof (Group));
                    }));

            /*
            // Created Orders
            Bag(x => x.CreatedOrders, c =>
                {
                    c.Table("wp_order");
                    c.BatchSize(100);
                    c.Cascade(Cascade.Persist);
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("created_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });
                },
                r => r.OneToMany(m => m.Class(typeof (Order))));


            // Modified Orders
            Bag(x => x.ModifiedOrders, c =>
                {
                    c.Table("wp_order");
                    c.BatchSize(100);
                    c.Cascade(Cascade.Persist);
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("modified_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });
                },
                r => r.OneToMany(m => m.Class(typeof (Order))));
             * */

            // Address Books
            Bag(x => x.AddressBooks, c =>
                {
                    c.Table("wp_user_address_book");
                    c.BatchSize(100);
                    c.Cascade(Cascade.Persist);
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("user_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });
                },
                r => r.OneToMany(m => m.Class(typeof (AddressBook))));

            // Print Shop
            ManyToOne(x => x.PrintShop, m =>
                {
                    m.Column("print_shop_id");
                    m.Class(typeof (PrintShop));
                    m.Cascade(Cascade.Persist);
                    m.Lazy(LazyRelation.Proxy);
                    m.NotNullable(true);
                });
        }
    }
}
