using System;
using System.Collections.Generic;
using System.Drawing;
using WebPrint.Framework;
using WebPrint.Web.Core;

namespace WebPrint.Web.Forms
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            string proname = "A";
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");

            UnaryExpression instanceCast = Expression.Convert(instance, typeof(TT).GetProperty(proname).ReflectedType);


            MemberExpression propertyAccessor = Expression.Property(instanceCast, typeof(TT).GetProperty(proname));

            UnaryExpression castPropertyValue = Expression.Convert(propertyAccessor, typeof(object));

            Expression<Func<object,object>> exp = Expression.Lambda<Func<object,object>>(castPropertyValue,instance);
            //最后缓存 这个exp 每个类型(Type)的每个属性(Property)都有一个获取值委托(Fuc)
            //故缓存字段 Dictionary<Type,Dictonary<string,Fuc>>  当用到的类型及属性很多时 这个缓存会有多大
            //其实这个可以用反射实现，但反射慢
            */
   
            /*
            Response.Write("<br/>" + exp.ToString());
            Response.Write("<br/>" + exp.Compile()(new TT()));

            proname = "B";
            Response.Write("<br/>" + exp.ToString());
            Response.Write("<br/>" + exp.Compile()(new TT()));

            proname = "C";
            Response.Write("<br/>" + exp.ToString());
            Response.Write("<br/>" + exp.Compile()(new TT()));
             * */

            DynamicMethodExecutor excutor = new DynamicMethodExecutor(typeof(TT).GetProperty("a").GetGetMethod());

            var t = new TT() { A = "Yes Get It." };
            var v = excutor.Execute(t, new object[] { });
            Response.Write("<br/>" + v);

            DynamicMethodExecutor excutor2 = new DynamicMethodExecutor(typeof(TT).GetProperty("A").GetSetMethod());
            excutor2.Execute(t, new object[] { "Yes Set It." });

            v = excutor.Execute(t, new object[] { });
            Response.Write("<br/>" + v);


            EpcManager epc = EpcManager.Parse("30340348E837BB00000770D9");
            Response.Write("<br/>30340348E837BB00000770D9");
            Response.Write("<br/>" + epc.Header);
            Response.Write("<br/>" + epc.FilterValue);
            Response.Write("<br/>" + epc.Partition);
            Response.Write("<br/>" + epc.GTIN.Gtin);
            Response.Write("<br/>" + epc.GTIN.Upc);
            Response.Write("<br/>" + epc.GTIN.Partition);
            Response.Write("<br/>" + epc.GTIN.CompanyPrefix);
            Response.Write("<br/>" + epc.GTIN.ItemReference);
            Response.Write("<br/>" + epc.SerialNumber);

            Response.Write("<br/>" + EpcManager.UpcConvertEpc("053818570685", "487641"));
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            /*
            var barTenderApp = new BarTenderApplication(new BtSettings
                {
                    LabelFormatFullPath = Server.MapPath(@"\Repository\direct.btw"),
                    PreviewFileFullPath = BarTenderHelper.PreviewFileFullPath
                });

            //barTenderApp.SetSubStrings(new Dictionary<string, string> {{"COLOR", "123"}});

            barTenderApp.SetDesignObjectsColor(new[] {new KeyValuePair<string, Color>("Cup", Color.DodgerBlue)});

            var fileName = string.Format("{0}.{1}.png", Session.SessionID,
                                         DateTime.Now.ToString("yyyyMMddHHmmssffff"));
            string msg;
            imgPreview.ImageUrl = barTenderApp.GeneratePreview(Request.PhysicalApplicationPath, fileName, out msg);

            lblMsg.Text = msg;
             * */
        }

        protected void btnTestEmail_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //DataBaseService.MySqlDBOperator dbbase = new DataBaseService.MySqlDBOperator();
            //dbbase.BeginTrans();
            //dbbase.ExecuteNonQuery("insert into test(testname) values('1')");
            //dbbase.ExecuteNonQuery("insert into test(testname) values('2')");
            //dbbase.ExecuteNonQuery("insert into test(testname) values('3')");
            //dbbase.ExecuteNonQuery("insert into test(testname) values('4')");
            //dbbase.ExecuteNonQuery("insert into test(testname) values('5')");
            //dbbase.ExecuteNonQuery("insert into test(testname) values('6')");
            //dbbase.ExecuteNonQuery("insert into test(testname) values('7')");

            //dbbase.ExecuteNonQuery("insert into test1(testname) values(1111)"); 
            //dbbase.ExecuteNonQuery("insert into test1(testname) values('2')");
            //dbbase.CommitTrans();
        }

        protected void btnNetPreview_Click(object sender, EventArgs e)
        {
            /*
            var clientPrint = new BarTenderClientPrint(new Settings
                {
                    LabelFormatFullPath = Server.MapPath(@"\Repository\direct.btw"),
                    PreviewFileFullPath = BarTenderHelper.PreviewFileFullPath
                });

            var fileName = string.Format("{0}.{1}.png", Session.SessionID,
                                         DateTime.Now.ToString("yyyyMMddHHmmssffff"));
            string msg;
            imgPreview.ImageUrl = clientPrint.GeneratePreview(Request.PhysicalApplicationPath, fileName, out msg);
            lblMsg.Text = msg;
             * */
        }
    }

    public class TT
    {
        public string A { get; set; }
        public string B { get; set; }
        public TT C { get; set; }

        public string a { get; set; }
    }
}