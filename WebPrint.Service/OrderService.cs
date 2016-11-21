using WebPrint.Data;
using WebPrint.Data.Repositories;
using WebPrint.Model;

namespace WebPrint.Service
{
    public class OrderService : Service<Order>
    {
        public OrderService(IRepository<Order> orderRepo, IRepository<OrderDetail> detailRepo,
            IRepository<User> userRepo, IUnitOfWork unitOfWork)
            : base(orderRepo, unitOfWork)
        {

        }
    }
}
