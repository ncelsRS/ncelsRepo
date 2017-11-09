using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Ncels.Helpers;
using PW.Ncels.Database.DataModel;
//using Microsoft.AspNet.Identity;
using PW.Ncels.Models;
using Task = System.Threading.Tasks.Task;

namespace PW.Ncels.Services
{
    public class EmailService : IIdentityMessageService
    {
        private static EmailService _instance;

        public static EmailService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmailService();
                }
                return _instance;
            }
        }

        public async Task SendAsync(IdentityMessage message){
            try {
                // Plug in your email service here to send an email.
                var email = new MailMessage(Convert.ToString(ConfigurationManager.AppSettings["FromMailAddress"]), message.Destination){
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                using (var client = new SmtpClient()) {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials =
                        new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["EmailUsername"]),
                            Convert.ToString(ConfigurationManager.AppSettings["EmailPassword"]));
                    client.Host = Convert.ToString(ConfigurationManager.AppSettings["Host"]);
                    client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    await client.SendMailAsync(email);
                }
            }
            catch (Exception ex) {
                LogHelper.Log.Error("Ошибка при отправке на email " + message.Destination, ex);
                throw;
            }

        }

        public string GetTemplate(EmailTemplateModel template)
        {
            string filePath = "";
            if (template.Type == 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/templates/ResetPassword.html");
            }
            else if (template.Type == 1)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/templates/RegisterReject.html");
            }
            else if (template.Type == 2)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/templates/RegisterAccept.html");
            }
            string file = System.IO.File.ReadAllText(filePath);
            string newTemplate = file.Replace("HeadMessage", template.HeadMessage).Replace("Title", template.Title).Replace("Message", template.Message).Replace("CallbackUrl", template.Url);
            return newTemplate;
        }

        public async Task<MessageModel> SendEmail(string email, string title, string template)
        {
            try
            {
                IdentityMessage message = new IdentityMessage()
                {
                    Destination = email,
                    Subject = title,
                    Body = template
                };
             await SendAsync(message);

            }
            catch (Exception e)
            {
                return new MessageModel()
                {
                    IsError = true,Message = e.Message
                };
            }
            return new MessageModel()
            {
                IsError = false
            };
        }

    }
}