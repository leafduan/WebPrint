using System;
using System.Linq;
using System.Text;
using WebPrint.Model;
using WebPrint.Model.Repositories;
using WebPrint.Model.Services;

namespace WebPrint.Web.Forms
{
    public partial class TestNHibernate : System.Web.UI.Page
    {
        /* should be public that autofac(ioc) can work with */
        public IService<Upc> UpcService { get; set; }
        public IService<User> UserService { get; set; }

        public IRepository<Upc> UpcRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            UpcService.Delete(u => u.UpcValue == "123456789098");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            var entity = new Upc
                {
                    Active = 0,
                    CreatedTime = DateTime.Now,
                    CreatedId = 1,
                    LastSerialNumber = 0,
                    UpcValue = "123456789098",
                    ModifiedId = 1,
                    ModifiedTime = DateTime.Now,
                    Gtin = DateTime.Now.ToString("HHmmssffff")
                };

            var result = UpcService.Save(entity);

            sb.AppendLine("Result: " + result + "<br/>");
            sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}<br/>", entity.Id, entity.UpcValue, entity.LastSerialNumber,
                                        entity.CreatedTime));

            Response.Write(sb.ToString());
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Query()).Where(u => u.UPC == \"123456789098\")<br/>");
            var query = UpcService
                .Query(u => u.UpcValue == "123456789098");

            query
                .ToList()
                .ForEach(u => sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}<br/>", u.Id, u.UpcValue,
                                                          u.LastSerialNumber, u.CreatedTime)));

            var select = UpcRepository.Query().Select(u => new { u.Id, u.Gtin }).FirstOrDefault();

            Response.Write(sb.ToString());
        }

        protected void btnLazyLoadTest_Click(object sender, EventArgs e)
        {
            var user = UserService.Get(u => u.Id == 1);

            var s = user.Id + " " + user.UserName + " " + user.Password + " " + user.Email + " " +
                    user.PrintShop.DisplayName; 

            Response.Write(s);
        }
    }
}