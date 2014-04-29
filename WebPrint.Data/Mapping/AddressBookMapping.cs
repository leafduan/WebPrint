using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebPrint.Model;

namespace WebPrint.Data.Mapping
{
    public class AddressBookMapping : EntityBaseMapping<AddressBook>//ClassMapping<AddressBook>
    {
        public AddressBookMapping()
            : base("wp_user_address_book")
        {
            /*
            Table("wp_user_address_book");

            Id(x => x.Id, m =>
                {
                    m.Column("id");
                    m.Generator(Generators.Sequence,
                                gm => gm.Params(new {sequence = "wp_user_address_book_id_seq"}));
                });
             * */

            //Property(x => x.UserId, m => m.Column("user_id"));

            ManyToOne(x => x.User, m =>
                {
                    m.Column("user_id");
                    m.Cascade(Cascade.Persist);

                    m.Lazy(LazyRelation.Proxy);
                    m.Class(typeof (User));
                });

            ManyToOne(x => x.Ship, m =>
                {
                    m.Column("ship_id");
                    m.Cascade(Cascade.Persist);
                    m.Lazy(LazyRelation.Proxy);
                    m.Class(typeof (ShipBill));
                });

            ManyToOne(x => x.Bill, m =>
                {
                    m.Column("bill_id");
                    m.Cascade(Cascade.Persist);
                    m.Lazy(LazyRelation.Proxy);
                    m.Class(typeof (ShipBill));
                });
        }
    }
}
