using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPrint.Framework;
using WebPrint.Model;
using WebPrint.Service;

namespace WebPrint.Test
{
    /// <summary>
    /// UserModulTest 的摘要说明
    /// </summary>
    [TestClass]
    public class UserModulTest
    {
        public UserModulTest()
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
                var userService = lifetime.Resolve<IService<User>>();
                //var printShopService = lifetime.Resolve<IService<PrintShop>>();

                var printShop = new PrintShop
                    {
                        Code = "HongKong",
                        DisplayName = "Hong Kong",
                        EmailList = "leaf.duan@r-pac.com.cn"
                    };

                var user = new User
                    {
                        UserName = "leaf",
                        Password = "leaf",
                        Active = 0,
                        CreatedTime = DateTime.Now,
                        DisplayName = "Leaf.Duan",
                        Email = "leaf.duan@r-pac.com.cn",
                        PrintShop = printShop,

                        VendorCode = "*",
                        ModifiedTime = DateTime.Now
                    };

                //var printId = 0;
                var userId = 0;

                if (!userService.Exists(u => u.UserName == "leaf"))
                {
                    /* print shop 主键作为 user 的外键时，会自动维护关系，先插入print shop，再赋值user的print_shop_id，再插入user */
                    /* 如果unique key关联，则不会自动维护关系 */
                    //printId = printShopService.Save<int>(printShop);
                    userId = (int)userService.Save(user);
                }

                //Assert.IsTrue(printId > 0, "Save PrintShop fail");
                Assert.IsTrue(userId > 0, "Save User fail");
            }
        }

        [TestMethod]
        public void TestSave2()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var userService = lifetime.Resolve<IService<User>>();
                var printShopService = lifetime.Resolve<IService<PrintShop>>();

                // 只能先获取现有 再赋值给User去插入；
                // 如果直接将new print shop，并且Id赋值，则插入User后，会更新print shop表 Why?
                var printShop = printShopService.Get(p => p.Id == 1);
                //var printShop = new PrintShop {Id = 1};

                var random = new Random().Next(3, 10000);

                var user = new User
                    {
                        UserName = "leaf{0}".Formatting(random),
                        Password = "leaf{0}".Formatting(random),
                        Active = 0,
                        CreatedTime = DateTime.Now,
                        DisplayName = "Leaf.Duan",
                        Email = "leaf.duan@r-pac.com.cn",
                        PrintShop = printShop,
                        VendorCode = "*",
                        ModifiedTime = DateTime.Now
                    };

                //var printId = 0;
                var userId = userService.Save(user);

                //Assert.IsTrue(printId > 0, "Save PrintShop fail");
                Assert.IsTrue(userId > 0, "Save User fail");
            }
        }

        [TestMethod]
        public void TestQuery()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var userService = lifetime.Resolve<IService<User>>();

                var user = userService.Get(u => u.UserName == "leaf");

                var shop = user.PrintShop;

                Assert.IsTrue(user != null);

                Assert.IsTrue(user.Id > 0);

                // 这个Id属性访问不会导致延迟加载（select shop），可能因为user表本已经查出此值
                Assert.IsTrue(shop.Id > 0); 

                // 导致延迟加载 select shop
                Assert.IsFalse(shop.DisplayName.IsNullOrEmpty());
            }
        }
    }
}
