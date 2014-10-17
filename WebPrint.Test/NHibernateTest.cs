using System;
using System.IO;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPrint.Data;
using WebPrint.Data.Repositories;
using WebPrint.Model;

namespace WebPrint.Test
{
    /// <summary>
    /// NHibernateTest 的摘要说明
    /// </summary>
    [TestClass]
    public class NHibernateTest
    {
        public NHibernateTest()
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
        public void TestHbm()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
#if DEBUG
                if (File.Exists("hbm.xml")) File.Delete("hbm.xml");
                File.AppendAllText("hbm.xml", SessionFactoryProvider.BuildMappingsXml());
#endif

                Assert.IsTrue(2 + 2 == 2*2);
            }
        }

        [TestMethod]
        public void TestSqlQuery()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var repository = lifetime.Resolve<IRepository<User>>();

                var list = repository.SqlQuery<ViewModel>("select user_name as Name, password, active, created_time as Date from wp_user").ToList();

                Assert.IsTrue(list.Any());
            }
        }

        [TestMethod]
        public void TestSqlExcute()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var repository = lifetime.Resolve<IRepository<User>>();

                var list = repository.ExcuteSql("update wp_user set user_name = 'leaf' where user_name = 'leaf'");

                Assert.IsTrue(list > 0);
            }
        }
    }

    public class ViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public int Active { get; set; }
        public DateTime Date { get; set; }
    }
}
