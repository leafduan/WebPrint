using WebPrint.Model;
using WebPrint.Model.Repositories;

namespace WebPrint.Data.Repositories
{
    public class UpcRepository : Repository<Upc>, IUpcRepository
    {
        public UpcRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }
    }
}
