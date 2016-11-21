using WebPrint.Data.Repositories;
using WebPrint.Model;

namespace WebPrint.Service
{
    public class OrderService : Service<Order>
    {
        private IRepository<Order> ordeRepository;
        private IRepository<OrderDetail> detailRepository;
        private IRepository<User> useRepository;

        public OrderService(IRepositoryProvider repositoryProvider)
            : base(repositoryProvider)
        {
            ordeRepository = repositoryProvider.GetRepository<Order>();
            detailRepository = repositoryProvider.GetRepository<OrderDetail>();
            useRepository = repositoryProvider.GetRepository<User>();
        }
    }
}
