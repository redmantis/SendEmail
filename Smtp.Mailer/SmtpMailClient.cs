using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smtp.Mailer.Objects.Excetions;
using Smtp.Mailer.util;
using System.Net.Sockets;
using Smtp.Mailer.Objects.Exceptions;
using System.IO;
using Smtp.Mailer.Objects;
using System.Threading;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
namespace Smtp.Mailer
{
    public class SmtpMailClient
    {
        public SmtpServerInfo ServerInfo { get; set; }
        public int Timeout { get; set; }
        private TcpClient client = null;
        private Stream stream = null;
        public SmtpMailClient(SmtpServerInfo serverInfo)
        {
            this.ServerInfo = serverInfo;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="smtpHost">smtp 服务器</param>
        /// <param name="port">端口</param>
        /// <param name="isSendAuth">是否需要验证</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public SmtpMailClient(string smtpHost, int port, bool isSendAuth, string userName, string password)
        {
            this.ServerInfo = new SmtpServerInfo(smtpHost, port, isSendAuth, userName, password);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message"></param>
        public void Send(MailMessage message)
        {
            this.SendMail(message);
        }
        /// <summary>
        /// 线程发送
        /// </summary>
        /// <param name="message"></param>
        public void SendAsync(MailMessage message)
        {

            Thread t = new Thread(new ParameterizedThreadStart(this.SendMail));
            t.IsBackground = true;
            t.Start(message);
        }
        /// <summary>
        /// 验证服务器信息
        /// </summary>
        /// <param name="serverInfo"></param>
        private void CheckStmpServerInfo(SmtpServerInfo serverInfo)
        {
            if (serverInfo == null)
                throw new InfoMissingException("Smtp Server Info");
            if (serverInfo.SmtpServer == null || serverInfo.SmtpServer.Length < 6)
                throw new InfoMissingException("Smtp Server");
            if (serverInfo.IsSendAuth)
            {
                if (serverInfo.Account == null || serverInfo.Account.Length < 1)
                    throw new InfoMissingException("Account");
                if (serverInfo.Password == null || serverInfo.Password.Length < 1)
                    throw new InfoMissingException("Password");
            }
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="message"></param>
        private void CheckMailMessage(MailMessage message)
        {
            if (message == null)
            {
                throw new InfoMissingException("MailMessage");
            }
            if (message.From == null)
            {
                throw new InfoMissingException("From");
            }
            if (message.DisplayFrom == null)
            {
                message.DisplayFrom = message.From;
            }
            if (message.Tos.Count < 1)
                throw new InfoMissingException("To");
            try
            {
                if (System.Text.Encoding.GetEncoding(message.Encoding) == null)
                {
                    throw new InfoMissingException("Encoding");
                }
            }
            catch
            {
                throw new InfoMissingException("Encoding");
            }
            foreach (var account in message.Tos)
            {
                if (!CommonValidator.CheckEmail(account.MailAddress))
                {
                    throw new EmailAddressNotValidatorException();
                }
            }
            foreach (var account in message.CCs)
            {
                if (!CommonValidator.CheckEmail(account.MailAddress))
                {
                    throw new EmailAddressNotValidatorException();
                }
            }
            foreach (var fileName in message.Attachment)
            {
                if (!CommonValidator.CheckFile(fileName))
                {
                    throw new AttchmentException("附件不正确");
                }
            }
            if (!CommonValidator.CheckEmail(message.From.MailAddress))
            {
                throw new EmailAddressNotValidatorException();
            }

        }
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="message1"></param>
        private void SendMail(Object message1)
        {
            var info = this.ServerInfo;
            var message = message1 as MailMessage;
            //验证信息
            CheckStmpServerInfo(this.ServerInfo);
            CheckMailMessage(message);
           
            client = new TcpClient();
            stream = null;
            
            if (Timeout != 0)
            {
                client.SendTimeout = Timeout;
                client.ReceiveTimeout = Timeout;
            }
            try
            {
                
                client.Connect(info.SmtpServer, info.ServerPort); //连接服务器
                if (info.EnableSsl)
                {
                    //ssl
                    stream = new SslStream(client.GetStream(), true,new RemoteCertificateValidationCallback(ValidateServerCertificate));
                    try
                    {
                        ((SslStream)stream).AuthenticateAsClient(info.SmtpServer,null,System.Security.Authentication.SslProtocols.Tls,false);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    //非 ssl
                    stream = client.GetStream();
                }
                //发送请求


                string code = "";
                code = MailerMethods.ReaderCode(stream);
               
                if (code.IndexOf(MailCode.READY) < 0)
                {
                    throw new SendInfoException(code);
                }
                //say hi
                MailerMethods.SendCommand(SmtpCommand.GetHelloCommand("13355716451@163.com", ServerInfo.IsSendAuth), stream, message.Encoding);
                code = MailerMethods.ReaderCode(stream);
                if (code.IndexOf(MailCode.OK) < 0)
                {
                    throw new SendInfoException(code);
                }
                //身份验证
                if (info.IsSendAuth)
                {
                    MailerMethods.SendCommand(SmtpCommand.GetLoginCommand(), stream, message.Encoding);

                    code = MailerMethods.ReaderCode(stream);
                    if (code.IndexOf(MailCode.AUTH) < 0)
                    {
                        throw new SendInfoException(code);
                    }
                    //用户名
                    MailerMethods.SendCommand(Convert.ToBase64String(Encoding.Default.GetBytes(ServerInfo.Account)), stream, message.Encoding);

                    code = MailerMethods.ReaderCode(stream);
                    if (code.IndexOf(MailCode.AUTH) < 0)
                    {
                        throw new SendInfoException(code);
                    }
                    //密码
                    MailerMethods.SendCommand(Convert.ToBase64String(Encoding.Default.GetBytes(ServerInfo.Password)), stream, message.Encoding);

                    code = MailerMethods.ReaderCode(stream);
                    if (code.IndexOf(MailCode.AUTH_OK) < 0)
                    {
                        throw new SendInfoException(code);
                    }

                }
                //邮件内容
                MailerMethods.SendCommand(SmtpCommand.GetMailFromCommand( message.From.MailAddress),stream, message.Encoding);
                code = MailerMethods.ReaderCode(stream);
                if (code.IndexOf(MailCode.OK) < 0)
                {
                    throw new SendInfoException(code);
                }
                foreach (var account in message.Tos)
                {
                    MailerMethods.SendCommand(SmtpCommand.GetMailToCommand(account.MailAddress), stream, message.Encoding);
                    code = MailerMethods.ReaderCode(stream);
                    if (code.IndexOf(MailCode.OK) < 0)
                    {
                        throw new SendInfoException(code);
                    }
                }
                foreach (var account in message.CCs)
                {
                    MailerMethods.SendCommand(SmtpCommand.GetMailToCommand(account.MailAddress), stream, message.Encoding);
                    code = MailerMethods.ReaderCode(stream);
                    if (code.IndexOf(MailCode.OK) < 0)
                    {
                        throw new SendInfoException(code);
                    }
                }

                MailerMethods.SendCommand(SmtpCommand.GetDataCommand(), stream, message.Encoding);
                code = MailerMethods.ReaderCode(stream);
                if (code.IndexOf(MailCode.DATA_READY) < 0)
                {
                    throw new SendInfoException(code);
                }
                
                MailerMethods.SendHeader(stream, message);
                MailerMethods.SendBody(message.MailContent, stream, message.Encoding, message.IsHtml);
                MailerMethods.SendAttchments(message.Attachment, stream, message.Encoding);
                MailerMethods.WriterEnd(stream, message.Encoding);
                code = MailerMethods.ReaderCode(stream);
                if (code.IndexOf(MailCode.OK) < 0)
                {
                    throw new SendInfoException(code);
                }
                MailerMethods.SendCommand(SmtpCommand.GetQuitCommand(), stream, message.Encoding);
            }
            catch (SendInfoException ex)
            {
                MailerMethods.SendCommand(SmtpCommand.GetQuitCommand(), stream, message.Encoding);
                throw ex;
            }
            catch (Exception ex)
            {
                //throw new SendInfoException(string.Format("Can't Connect Smtp Server,{0}",ex));
            }
            finally
            {
                CloseConnection();
            }

        }
        /// <summary>
        /// SSL 远程回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public  bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            return false;
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        private void CloseConnection()
        {
            try
            {
                stream.Close();
                client.Close();
            }
            catch
            {

            }
        }
        ~SmtpMailClient()
        {
            this.CloseConnection();
        }

    }
}
