using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace WebPrint.Framework
{
    public static class SmtpHelper
    {
        /// <summary>
        /// 同步发送
        /// </summary>
        /// <param name="info"></param>
        public static void SendMail(SmtpInformation info)
        {
            var msg = GetMailMessage(info);

            var client = new SmtpClient();
            client.Host = info.Server;
            if (!string.IsNullOrEmpty(info.Username)) client.Credentials = new NetworkCredential(info.Username, info.Password);
            client.Send(msg);

            msg.Dispose();
        }

        /// <summary>
        /// 异步发送
        /// </summary>
        /// <param name="info"></param>
        public static void SendMailAsync(SmtpInformation info)
        {
            var msg = GetMailMessage(info);

            var client = new SmtpClient();
            client.Host = info.Server;
            if (!string.IsNullOrEmpty(info.Username)) client.Credentials = new NetworkCredential(info.Username, info.Password);
            client.SendAsync(msg, msg);
        }

        /// <summary>
        /// 获得 MailMessage 对象
        /// </summary>
        private static MailMessage GetMailMessage(SmtpInformation info)
        {
            var msg = new MailMessage();

            info.ToAddresses
                .Where(addr => !addr.IsNullOrEmpty())
                .Select(addr => addr.ToLower())
                .Distinct()
                .ForEach(addr => msg.To.Add(addr));

            info.CcAddresses
                .Where(addr => !addr.IsNullOrEmpty())
                .Select(addr => addr.ToLower())
                .Distinct()
                .ForEach(addr => msg.CC.Add(addr));

            msg.From = new MailAddress(info.FromAddress, info.FromDisplayName, Encoding.UTF8);//发件人地址（可以随便写），发件人姓名，编码
            msg.Subject = info.Subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = info.Body;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;

            info.Attachments.ForEach(item => msg.Attachments.Add(item));

            return msg;
        }

        //可以用NVelocity模板引擎
        public static string EmailTemplate(string content, string orderSystemName)
        {
            var template = "<html><body>"
                            + "Thank you for your confirmation!<br/>"
                            + "You could view the order's detail information via the link below:<br/>"
                            + "{0}<br/><br/>"
                            + "************************************************************************************<br/>"
                            + "This e-mail is sent by the r-pac {1} ordering system automatically.<br/>"
                            + "Please don't reply this e-mail directly!<br/>"
                            + "************************************************************************************<br/>"
                            + "</body></html>";

            return string.Format(template, content, orderSystemName);
        }

        public static string EmailCancelledTemplete(string content, string orderSystemName)
        {
            var template = "<html><body>"
                           + "Below order is cancelled:<br/>"
                           + "{0}<br/><br/>"
                           + "************************************************************************************<br/>"
                           + "This e-mail is sent by the r-pac {1} ordering system automatically.<br/>"
                           + "Please don't reply this e-mail directly!<br/>"
                           + "************************************************************************************<br/>"
                           + "</body></html>";

            return string.Format(template, content, orderSystemName);
        }
    }

    public class SmtpInformation
    {
        public SmtpInformation()
        {
            ToAddresses = new List<string>();
            CcAddresses = new List<string>();
            Attachments = new Attachments();
        }
       
        /// <summary>
        /// 发送邮件服务器地址
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 登陆用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 显示发件人email
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// 用于显示的发件人名
        /// </summary>
        public string FromDisplayName { get; set; }
        /// <summary>
        /// 收件人列表
        /// </summary>
        public List<string> ToAddresses { get; private set; }
        //抄送人列表
        public List<string> CcAddresses { get; private set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件内容 HTML格式
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public Attachments Attachments { get; private set; }
    }

    public class Attachments : IEnumerable<Attachment>
    {
        private readonly List<Attachment> attachments;

        internal Attachments()
        {
            attachments = new List<Attachment>();
        }

        public void Add(string fileName)
        {
            attachments.Add(new Attachment(fileName));
        }

        public void Add(System.IO.Stream contentStream, string name)
        {
            attachments.Add(new Attachment(contentStream, name));
        }

        public void ForEach(Action<Attachment> action)
        {
            attachments.ForEach(action);
        }

        public void Clear()
        {
            attachments.Clear();
        }

        #region IEnumerable<Attachment> 成员

        public IEnumerator<Attachment> GetEnumerator()
        {
            return attachments.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
