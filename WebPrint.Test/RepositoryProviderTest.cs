using System.Linq;
using Autofac;
using WebPrint.Data.Repositories;
using WebPrint.Model;
using Xunit;
using Xunit.Abstractions;

namespace WebPrint.Test
{
    public class RepositoryProviderTest
    {
        private ITestOutputHelper output;

        public RepositoryProviderTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestProvider()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var provider = lifetime.Resolve<IRepositoryProvider>();

                var orderRepo = provider.GetRepository<Order>();
                var userRepo = provider.GetRepository<User>();
                userRepo = provider.GetRepository<User>();
                userRepo = provider.GetRepository<User>();
                userRepo = provider.GetRepository<User>();
                userRepo = provider.GetRepository<User>();

                output.WriteLine(userRepo.Query().First().DisplayName);

                Assert.True(orderRepo != null && userRepo != null);
            }
        }
    }
}
