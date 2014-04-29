using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPrint.Model;
using WebPrint.Model.Services;

namespace WebPrint.Test
{
    /// <summary>
    /// OrderModulTest 的摘要说明
    /// </summary>
    [TestClass]
    public class OrderModulTest
    {
        public OrderModulTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestSave()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var orderService = lifetime.Resolve<IService<Order>>();
                var userService = lifetime.Resolve<IService<User>>();
                var printShopService = lifetime.Resolve<IService<PrintShop>>();
                var formatService = lifetime.Resolve<IService<Format>>();

                var user = userService.Load(1); //userService.Get(u => u.UserName == "leaf");
                var shop = printShopService.Load(1); //printShopService.Get(s => s.Code == "HongKong");
                var format = formatService.Load(1); //GetFormat(user.Id, formatService);

                var random = new Random().Next(1, 1000);

                #region create order

                var order = new Order
                    {
                        AssociatedOrderId = 0,
                        JobNo = "WP" + DateTime.Now.ToString("yyyyMMdd"),
                        PoNo = "Purchase" + random.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0'),
                        Type = OrderType.ServiceBureau,
                        VendorCode = "LEAF",
                        Remark = "Service Bureau",
                        Wastage = "0",
                        Production = 0,
                        CompletionDate = string.Empty,
                        Status = OrderStatus.Open,
                        SoNo = string.Empty,
                        Active = 0,
                        CreatedTime = DateTime.Now,
                        ModifiedTime = DateTime.Now,
                        FileName = string.Empty,
                        CreatedUser = user,
                        ModifiedUser = user,
                        PrintShop = shop
                    };

                #endregion

                var shipBill = GetShipBill();
                // 保存时 必须要此赋值
                shipBill.Order = order;
                order.ShipBill = shipBill;

                GetDetails(user.Id).ForEach(d =>
                    {
                        d.Order = order;
                        d.Format = format;
                        order.Details.Add(d);
                    });

                #region order保存相关表顺序

                /*
                 * 此种方式执行顺序
                 * 1  select user
                 * 2  select print shop
                 * 3  select order id seq
                 * 4  select order ship bill id seq
                 * 5  select order detail id seq(多个detail则select多次)
                 * 6  select format id seq(正常情况应该select format，而不是新增)
                 * 
                 * 7  insert order_ship_bill, 与order one to one关系
                 * 8  insert order
                 * 9  insert format(如果select了format，就不需要此步)
                 * 10 insert detail(每个依次保存)
                 * 
                 * summary：整个流程的正确，表明配置正确
                 * */

                #endregion

                orderService.Save(order);
                order.JobNo = order.JobNo + order.Id.ToString(CultureInfo.InvariantCulture).PadLeft(7, '0');

                Assert.IsTrue(order.Id > 0, "save order failed.");
            }
        }

        private OrderShipBill GetShipBill()
        {
            var shipAddress = new Address();
            var billAddress = new Address();

            shipAddress.Address1 = shipAddress.Address2 = shipAddress.Address3
                = shipAddress.Attention = shipAddress.CityTown = shipAddress.Company
                = shipAddress.Country = shipAddress.Country = shipAddress.Fax
                = shipAddress.Phone = shipAddress.Remark = shipAddress.State
                = shipAddress.ZipCode = "AAA";

            billAddress.Address1 = billAddress.Address2 = billAddress.Address3
               = billAddress.Attention = billAddress.CityTown = billAddress.Company
               = billAddress.Country = billAddress.Country = billAddress.Fax
               = billAddress.Phone = billAddress.Remark = billAddress.State
               = billAddress.ZipCode = "BBB";

            shipAddress.Email = billAddress.Email = "leaf.duan@r-pac.com.cn";

            var shipBill = new OrderShipBill
            {
                ShipAddress = shipAddress,
                BillAddress = billAddress,
                CreatedTime = DateTime.Now
            };

            return shipBill;
        }

        private Format GetFormat(int userId, IService<Format> service)
        {
            var format = service.Get(f => f.Name == "WP");

            if (format == null)
            {
                format = new Format
                    {
                        CategoryId = 0,
                        Name = "WP",
                        DisplayName = "WP Test",
                        Path = @"\Repository\direct.btw",
                        PreviewImage = @"\Repository\Image\direct.jpg",
                        Code = "WP",
                        Type = TicketType.NonRFID,
                        CreatedId = userId,
                        CreatedTime = DateTime.Now,
                        ModifiedTime = DateTime.Now,
                        ModifiedId = userId
                    };
            }

            return format;
        }

        private List<OrderDetail> GetDetails(int userId)
        {
            #region create detail

            var detail = new OrderDetail
                {
                    OriginalQty = 10,
                    Qty = 10,
                    SpareQty = 0,
                    RemainQty = 10,
                    EpcSerialBegin = 0,
                    EpcSerialEnd = 0,
                    EpcCodeBegin = string.Empty,
                    EpcCodeEnd = string.Empty,
                    PrintBeginEpc = string.Empty,
                    Status = OrderDetailStatus.New,
                    LineNo = 1,
                    Remark = "New",
                    Active = 0,
                    CreatedTime = DateTime.Now,
                    ModifiedTime = DateTime.Now,
                    CreatedId = userId,
                    ModifiedId = userId,
                    FileName = "",
                    PoNo = "Purchase0001",
                    Style = "Style",
                    StyleDesc = "StyleDesc",
                    Color = "Color",
                    ColorDesc = "ColorDesc",
                    Size = "Size",
                    SizeDesc = "SizeDesc",
                    Vendor = "vendor",
                    VendorCode = "Vendor",
                    Upc = "719544153775",
                    Gtin = "00719544153775",
                    Logo = "WP",
                    Unit = "1.2",
                    Price = "12",
                    ProductionType = ProductionType.Thermal,
                    Desc = "desc",
                    GraphicCategory = "GraphicCategory",
                    GraphicName = "GraphicName",
                    GraphicCode = "GraphicCode",
                    GraphicLeadTime = "GraphicLeadTime",
                    GraphicPreviewImage = @"\repository\image\direct.jpg",
                    GraphicSurcharge = "GraphicSurcharge",
                    GraphicTier = "GraphicTier",
                    GraphicUom = "GraphicUom"
                };
            #endregion

            #region create detail2
            var detail2 = new OrderDetail
                {
                    OriginalQty = 20,
                    Qty = 20,
                    SpareQty = 0,
                    RemainQty = 20,
                    EpcSerialBegin = 0,
                    EpcSerialEnd = 0,
                    EpcCodeBegin = string.Empty,
                    EpcCodeEnd = string.Empty,
                    PrintBeginEpc = string.Empty,
                    Status = OrderDetailStatus.New,
                    LineNo = 2,
                    Remark = "detail2",
                    Active = 0,
                    CreatedTime = DateTime.Now,
                    ModifiedTime = DateTime.Now,
                    CreatedId = userId,
                    ModifiedId = userId,
                    FileName = "",
                    PoNo = "Purchase0002",
                    Style = "Style",
                    StyleDesc = "StyleDesc",
                    Color = "Color",
                    ColorDesc = "ColorDesc",
                    Size = "Size",
                    SizeDesc = "SizeDesc",
                    Vendor = "vendor",
                    VendorCode = "Vendor",
                    Upc = "719544153775",
                    Gtin = "00719544153775",
                    Logo = "WP",
                    Unit = "1.2",
                    Price = "12",
                    ProductionType = ProductionType.Thermal,
                    Desc = "desc",
                    GraphicCategory = "GraphicCategory",
                    GraphicName = "GraphicName",
                    GraphicCode = "GraphicCode",
                    GraphicLeadTime = "GraphicLeadTime",
                    GraphicPreviewImage = @"\repository\image\direct.jpg",
                    GraphicSurcharge = "GraphicSurcharge",
                    GraphicTier = "GraphicTier",
                    GraphicUom = "GraphicUom"
                };
            #endregion

            // 如果多次添加同一实例,则只保存一个
            var details = new List<OrderDetail> {detail, detail2};

            return details;
        }

        [TestMethod]
        public void TestQuery()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var orderService = lifetime.Resolve<IService<Order>>();

                //var order = orderService.Get(o => o.JobNo == "WP0001");
                var order = orderService.Query(o => o.CreatedUser.Id == 1).ToList();

                var shipBill = order.First().ShipBill;

                Assert.IsNotNull(order,"get order failed.");
            }
        }

        /// <summary>
        /// 模拟单向一对多关系的查询测试
        /// </summary>
        [TestMethod]
        public void TestQueryManyWithouMapping()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var orderDetailService = lifetime.Resolve<IService<OrderDetail>>();

                // 如果使用非外键，而是关联表对象的其他属性为条件，则left outer join关联外键表(order)，
                // 再外键表条件查询
                //var details = orderDetailService.Query(o => o.Order.JobNo == "WP0001");

                // 如果使用外键为条件 则直接为条件查询
                var details = orderDetailService.Query(o => o.Order.Id == 1);

                // 厉害 智能实现的厉害

                Assert.IsNotNull(details.Any(), "get order details failed.");
            }
        }

        [TestMethod]
        public void TestQueryManyWithouMapping2()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var userService = lifetime.Resolve<IService<User>>();
                var ordersService = lifetime.Resolve<IService<Order>>();

                var orders = ordersService.Query(o => o.CreatedUser.Id == 1);

                var group = userService.Get(u => u.Id == 1).Groups.First();

                #region ps
                /*
                 * 多对多关系查询，不使用配置关联的方式（Linq 太伟大了，NHibernate 生成的太漂亮了）
                 * NHibernate: 
                    select
                        user0_.id as id6_,
                        user0_.user_name as user2_6_,
                        user0_.password as password6_,
                        user0_.display_name as display4_6_,
                        user0_.email as email6_,
                        user0_.vendor_code as vendor6_6_,
                        user0_.active as active6_,
                        user0_.created_time as created8_6_,
                        user0_.modified_time as modified9_6_,
                        user0_.print_shop_id as print10_6_ 
                    from
                        wp_user user0_ 
                    where
                        exists (
                            select
                                group2_.id 
                            from
                                wp_user_group groups1_,
                                wp_group group2_ 
                            where
                                user0_.id=groups1_.user_id 
                                and groups1_.group_id=group2_.id 
                                and group2_.id=:p0
                        );
                    :p0 = 1 [Type: Int32 (0)]
                 * */
                #endregion
                var users = userService.Query(u => !u.Groups.Contains(group));

                users.ToList().ForEach(Console.WriteLine);

                Assert.IsNotNull(orders.Any(), "get order details failed.");
            }
        }

        [TestMethod]
        public void TestEagerFetch()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var orderService = lifetime.Resolve<IService<Order>>();

                //var order = orderService.EagerFetch(o => o.CreatedUser.Id == 1, o => o.Details).ToList();

                var order = orderService
                    .Fetch(o => o.CreatedUser.Id == 1, o => o.ShipBill);

                //var shipBill = order.First().ShipBill;

                Assert.IsNotNull(order, "get order failed.");
            }
        }
    }
}
