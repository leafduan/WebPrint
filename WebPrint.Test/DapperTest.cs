using System.Linq;
using Autofac;
using Dapper;
using WebPrint.Service;
using Xunit;
using Xunit.Abstractions;

namespace WebPrint.Test
{
    public class DapperTest
    {
        private ITestOutputHelper output;

        public DapperTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestConnection()
        {
            using (var lifetime = IocConfig.Container.BeginLifetimeScope())
            {
                var dbService = lifetime.Resolve<IDbService>();

                var aeoConnection = dbService.GetAeoSystemConnection();
                var data = aeoConnection.Query("select * from wp_user").ToList();

                output.WriteLine(data.First().user_name);

                Assert.True(data.Count > 0);
            }
        }
    }
}
