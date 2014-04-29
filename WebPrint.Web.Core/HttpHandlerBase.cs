using System.Reflection;
using System.Web;

namespace WebPrint.Web.Core
{
    public class HttpHandlerBase : IHttpHandler
    {
        //protected HttpContext Context { get; private set; }
        protected HttpRequest Request { get; private set; }
        protected HttpResponse Response { get; private set; }
        protected HttpApplicationState Application { get; private set; }
        protected HttpServerUtility Server { get; private set; }
        protected UserGroupPermissionManager CurrentUser { get; private set; }

        /// <summary>
        /// 在ProcessRequest中使用，反射执行方面，免去写switch的麻烦
        /// </summary>
        /// <param name="method">方法名称，不区分大小写</param>
        /// <param name="parameters">参数</param>
        protected void Process(string method, params object[] parameters)
        {
            var methodInfo = this
                .GetType()
                .GetMethod(method, BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);

            if (methodInfo != null)
            {
                methodInfo.Invoke(this, parameters);
            }
        }

        /// <summary>
        /// 支持匿名类型
        /// </summary>
        /// <param name="obj"></param>
        protected void JsonToClient(object obj)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            Response.ContentType = "text/plain";
            Response.Write(json);
            Response.End();
        }

        protected void HtmlToClient(string responseText)
        {
            Response.ContentType = "text/html";
            Response.Write(responseText);
            Response.End();
        }

        #region 不能如此设计 如果方法里面调用 ProcessRequest(HttpContext context) 死循环了

        /*
        protected virtual void ProcessRequest() 
        {
            this.HtmlToClient(string.Empty);
        }
         * */

        #endregion

        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            //Context = context;
            Request = context.Request;
            Response = context.Response;
            Application = context.Application;
            Server = context.Server;
            CurrentUser = new UserGroupPermissionManager();

            //this.ProcessRequest();
        }

        #endregion
    }
}
