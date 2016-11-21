using WebPrint.Model;

namespace WebPrint.Data.Repositories
{
    public class UpcRepository : Repository<Upc>, IUpcRepository
    {
        public UpcRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
