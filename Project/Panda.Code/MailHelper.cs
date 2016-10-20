/*******************************************************************************
 * 作者：星星    
 * 描述：Email发送  
 * 修改记录： 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Panda.Code
{
    public class MailHelper
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string MailServer { get; set; }
        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public int MailServerPort { get; set; } = 25;
        /// <summary>
        /// 用户名
        /// </summary>
        public string MailUserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string MailPassword { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string MailName { get; set; }

        /// <summary>
        /// 抄送地址 
        /// </summary>
        public List<string> CC { get; set; }

        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="encoding">编码</param>
        /// <param name="isBodyHtml">是否Html</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <returns>是否成功</returns>
        public bool Send(string to, string subject, string body, string encoding = "UTF-8", bool isBodyHtml = true, bool enableSsl = false)
        {
            try
            {
                MailMessage message = new MailMessage();
                // 接收人邮箱地址
                message.To.Add(new MailAddress(to));
                message.From = new MailAddress(MailUserName, MailName);
                message.BodyEncoding = Encoding.GetEncoding(encoding);
                message.Body = body;

                //抄送
                foreach (string s in CC)
                    message.CC.Add(s);

                //编码
                message.SubjectEncoding = Encoding.GetEncoding(encoding);
                message.Subject = subject;
                message.IsBodyHtml = isBodyHtml;

                SmtpClient smtpclient = new SmtpClient(MailServer, MailServerPort);
                smtpclient.Credentials = new NetworkCredential(MailUserName, MailPassword);
                //SSL连接
                smtpclient.EnableSsl = enableSsl;
                smtpclient.Send(message);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 异步发送邮件 独立线程
        /// </summary>
        /// <param name="to">邮件接收人</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public Task SendAsync(string to, string subject, string body, string encoding = "UTF-8", bool isBodyHtml = true, bool enableSsl = false)
        {
            return Task.Run(() =>
           {
               try
               {
                   MailMessage message = new MailMessage();
                   // 接收人邮箱地址
                   message.To.Add(new MailAddress(to));
                   message.From = new MailAddress(MailUserName, MailName);
                   message.BodyEncoding = Encoding.GetEncoding(encoding);
                   message.Body = body;

                   //抄送
                   foreach (string s in CC)
                       message.CC.Add(s);

                   //编码
                   message.SubjectEncoding = Encoding.GetEncoding(encoding);
                   message.Subject = subject;
                   message.IsBodyHtml = isBodyHtml;

                   SmtpClient smtpclient = new SmtpClient(MailServer, MailServerPort);
                   smtpclient.Credentials = new NetworkCredential(MailUserName, MailPassword);
                   //SSL连接
                   smtpclient.EnableSsl = enableSsl;
                   smtpclient.Send(message);
                   return true;
               }
               catch (Exception)
               {
                   throw;
               }
           });

        }
    }
}
