using System;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPrint.Data;
using WebPrint.Data.Repositories;
using WebPrint.Model;

namespace WebPrint.Test
{
    /// <summary>
    /// RepositoryTest 的摘要说明
    /// </summary>
    [TestClass]
    public class RepositoryTest
    {
        public RepositoryTest()
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
            get { return testContextInstance; }
            set { testContextInstance = value; }
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
                var unitOfWork = lifetime.Resolve<IUnitOfWork>();

                var upcRepository = lifetime.Resolve<IRepository<Upc>>();
                var model = new Upc
                    {
                        CreatedId = 818,
                        CreatedTime = DateTime.Now,
                        ModifiedId = 825,
                        ModifiedTime = DateTime.Now,
                        Active = 0,
                        LastSerialNumber = 1,
                        UpcValue = "789456123000",
                        Gtin = "789456123000"
                    };

                var id = upcRepository.Save(model);
                
                //unitOfWork.Commit();

                Assert.IsNotNull(id, "create upc failed.");
                Assert.AreEqual(id, model.Id);
            }
        }

        [TestMethod]
        public void TestQuery()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var unitOfWork = lifetime.Resolve<IUnitOfWork>();

                var upcRepository = lifetime.Resolve<IRepository<Upc>>();
                var query = upcRepository.Query(u => u.UpcValue == "789456123000");

                Assert.IsNotNull(query, "Not found.");
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var unitOfWork = lifetime.Resolve<IUnitOfWork>();

                var upcRepository = lifetime.Resolve<IRepository<Upc>>();
                var model = upcRepository.Query(u => u.UpcValue == "789456123000").FirstOrDefault();
                model.CreatedTime = new DateTime(1988, 8, 8);
                upcRepository.Update(model);

                var model1 = upcRepository.Query(u => u.UpcValue == "789456123000").First();

                Assert.AreEqual(model.CreatedTime, model1.CreatedTime, "Update failed.");
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var unitOfWork = lifetime.Resolve<IUnitOfWork>();

                var upcRepository = lifetime.Resolve<IRepository<Upc>>();
                var model = upcRepository.Query(u => u.UpcValue == "789456123000").FirstOrDefault();
                upcRepository.Delete(model);

                model = upcRepository.Query(u => u.UpcValue == "789456123000").FirstOrDefault();

                Assert.IsNull(model);
            }
        }
    }
}
