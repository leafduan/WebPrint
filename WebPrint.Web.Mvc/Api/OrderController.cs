using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebPrint.Model;
using WebPrint.Service;

namespace WebPrint.Web.Mvc.Api
{
    public class OrderController : ApiController
    {
        private readonly IService<Order> service;

        public OrderController(IService<Order> service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<Order> Orders()
        {
            // 序列化Domain中的Model异常 因为这些Model有Navigation Property 而这些属性序列化异常
            // 如Order中 除了一般字段属性 还有Details(IList<OrderDetail>) CreatedUser(User) 等属性
            // 而这些属性导致对象的循环引用 如Order.Details 而Detail.Order 他们之间构成循环(Self referencing loop)
            var orders = service.Query(o => o.CreatedUser.Id == 1).ToList();
            /*
            orders.ForEach(o =>
                {
                    o.ShipBill = null;
                    o.CreatedUser = null;
                    o.ModifiedUser = null;
                    o.Details.Clear();
                    o.PrintShop = null;
                });
            * */
            return orders;

            /*
            return new[]
                {
                    new Order {Id = 111, JobNo = "12333", PoNo = "45454"},
                    new Order {Id = 222, JobNo = "12333", PoNo = "45454"}
                };
             * */
        }

        [HttpGet]
        public Order Order(int id)
        {
            var order = service.Get(o => o.Id == id);
            /*
            order.ShipBill = null;
            order.CreatedUser = null;
            order.ModifiedUser = null;
            order.Details.Clear();
            order.PrintShop = null;
            * */
            return order;
            //return new Order {Id = 123, JobNo = "12333", PoNo = "45454"};
        }
    }
}
