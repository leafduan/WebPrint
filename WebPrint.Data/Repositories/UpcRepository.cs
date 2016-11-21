using NHibernate;
using WebPrint.Model;

namespace WebPrint.Data.Repositories
{
    public class UpcRepository : Repository<Upc>, IUpcRepository
    {
        public UpcRepository(ISession session)
            : base(session)
        {
        }
    }
}
