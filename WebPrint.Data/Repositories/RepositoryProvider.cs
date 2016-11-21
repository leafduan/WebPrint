using System.Collections.Generic;
using WebPrint.Model;

namespace WebPrint.Data.Repositories
{
    // instance per request
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly Dictionary<string, object> repositories = new Dictionary<string, object>();

        private readonly IUnitOfWork unitOfWork;

        public RepositoryProvider(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase
        {
            var type = typeof(TEntity);
            var className = type.FullName;
            if (repositories.ContainsKey(className))
            {
                return (repositories[className] as IRepository<TEntity>);
            }
            else
            {
                //var repository = Activator.CreateInstance(typeof(Repository<TEntity>), unitOfWork) as IRepository<TEntity>;
                var repository = new Repository<TEntity>(unitOfWork.SessionProvider.Session);
                repositories.Add(className, repository);

                return repository;
            }
        }
    }
}
