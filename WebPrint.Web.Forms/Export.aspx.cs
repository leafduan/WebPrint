using System;

namespace WebPrint.Web.Forms
{
    public partial class Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            /*
            var conn = "Server=192.168.42.93;Port=5432;User Id=postgres;Password=ky2379ck$$;Database=Wacoal;";
            var id = txtId.Text.TryParseInt();
            var filename = txtFileName.Text;
            var path = Path.Combine(@"C:\Users\Leaf.Duan\Desktop\Wacoal\Pass Through Orders\", filename);

            var session = DBOperatorFactory.Factory.CreateOperator(DBType.PostgreSQL, conn);
            var sql = @"
                       SELECT po, style, color, logo, size_desc, factorycode, price, upc
                       FROM hanes_detail_po 
                       where header_id = {0}
                       and position('0' in color) = 1";

            sql = sql.Format(id);

            var data = session.Query(sql);

            ExcelStrategy.CreateInstance().Export(path,data);
            */
        }
    }
}