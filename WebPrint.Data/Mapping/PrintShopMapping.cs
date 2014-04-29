using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class PrintShopMapping : EntityBaseMapping<PrintShop> //ClassMapping<PrintShop>
    {
        public PrintShopMapping()
            : base("wp_print_shop")
        {
            /*
            Table("wp_print_shop");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence, gm => gm.Params(new {sequence = "wp_print_shop_id_seq"}));
                });
             * */

            Property(x => x.Code, m => m.Column("code"));
            Property(x => x.DisplayName, m => m.Column("display_name"));
            Property(x => x.EmailList, m => m.Column("email_list"));

            /*
            Bag(x => x.Users, c =>
                {
                    c.Table("wp_uer");
                    c.Cascade(Cascade.Persist);
                    c.BatchSize(100);
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("print_shop_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });

                },
                r => r.OneToMany(m => m.Class(typeof (User))));

            Bag(x => x.Orders, c =>
                {
                    c.Table("wp_order");
                    c.Cascade(Cascade.Persist);
                    c.BatchSize(100);
                    c.Inverse(true);
                    c.Lazy(CollectionLazy.Lazy);

                    c.Key(k =>
                        {
                            k.Column("print_shop_id");
                            k.OnDelete(OnDeleteAction.NoAction);
                        });

                },
                r => r.OneToMany(m => m.Class(typeof (Order))));
             * */
        }
    }
}
