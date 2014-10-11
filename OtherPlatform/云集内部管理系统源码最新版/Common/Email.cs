using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common
{
    public class Email
    {
        /// <summary>
        /// 发送Email功能函数
        /// </summary>
        /// <param name="to">接收人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <returns>是否成功</returns>
        public static void SendEmail(string to, string subject, string body)
        {
            SendEmail(to, subject, body, true);
        }

        /// <summary>
        /// 发送Email功能函数
        /// </summary>
        /// <param name="to">接收人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">是否是HTML格式Mail</param>
        /// <returns>是否成功</returns> 
        public static void SendEmail(string to, string subject, string body, bool isBodyHtml)
        {
            //设置smtp
            var smtp = new SmtpClient
                           {
                               Host = ConfigurationManager.AppSettings["SMTPServer"],
                               EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTPServerEnableSsl"]),
                               Port = int.Parse(ConfigurationManager.AppSettings["SMTPServerPort"]),
                               Credentials =
                                   new NetworkCredential(ConfigurationManager.AppSettings["SMTPServerUser"],
                                                         ConfigurationManager.AppSettings["SMTPServerPassword"])
                           };

            ////开一个Message
            var mail = new MailMessage
                           {
                               Subject = subject,
                               SubjectEncoding = Encoding.GetEncoding("utf-8"),
                               BodyEncoding = Encoding.GetEncoding("utf-8"),
                               From =
                                   new MailAddress(ConfigurationManager.AppSettings["SMTPServerUser"],
                                                   ConfigurationManager.AppSettings["SMTPServerUserDisplayName"]),
                               IsBodyHtml = isBodyHtml,
                               Body = body
                           };

            mail.To.Add(to);

            var ised = new InternalSendEmailDelegate(smtp.Send);
            ised.BeginInvoke(mail, null, null);
            //smtp.SendAsync(mail, null);
        }

        #region Nested type: InternalSendEmailDelegate

        private delegate void InternalSendEmailDelegate(MailMessage m);

        #endregion
    }
}