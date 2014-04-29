using System;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPrint.Model;
using WebPrint.Model.Services;

namespace WebPrint.Test
{
    [TestClass]
    public class ServicesTest
    {
        [TestMethod]
        public void TestSave()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var upcServices = lifetime.Resolve<IService<Upc>>();
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

                var id = upcServices.Save<int>(model);

                Assert.IsNotNull(id, "create upc failed.");
                Assert.AreEqual(id, model.Id);
            }
        }

        [TestMethod]
        public void TestQuery()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            //using (var lifetime = IoC.IoC.Container.BeginLifetimeScope("httpRequest"))
            {
                var upcServices = lifetime.Resolve<IService<Upc>>();
                var models = upcServices.Query(u => u.UpcValue == "789456123000");

                Assert.IsTrue(models.Any(), "Not found.");
            }
        }

        [TestMethod]
        public void TestPagedList()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var upcService = lifetime.Resolve<IService<Upc>>();

                var list1 = upcService.Query(u => u.UpcValue == "123456789098", 1, 2);
                var list2 = upcService.Query(u => u.UpcValue == "123456789098", 2, 2);

                Assert.AreEqual(2, list1.Count());
                Assert.IsTrue(2 <= list2.Count());
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var upcServices = lifetime.Resolve<IService<Upc>>();
                upcServices.Update(u => u.UpcValue == "789456123000", u => u.CreatedTime = new DateTime(1988, 8, 8));

                var model = upcServices.Get(u => u.UpcValue == "789456123000");

                Assert.AreEqual(new DateTime(1988, 8, 8), model.CreatedTime, "Update failed.");
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var upcServices = lifetime.Resolve<IService<Upc>>();
                upcServices.Delete(u => u.UpcValue == "789456123000");

                var model = upcServices.Query(u => u.UpcValue == "789456123000").FirstOrDefault();

                Assert.IsNull(model);
            }
        }
    }
}
