using WebPrint.Model;

namespace WebPrint.Data.Repositories
{
    public interface IRepositoryProvider
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase;
    }
}
