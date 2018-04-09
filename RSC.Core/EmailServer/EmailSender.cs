using System;
using System.Net.Mail;
using System.Collections.Generic;
using Microsoft.Extensions;

namespace RSC.Core.EmailServer
{
    using Microsoft.Extensions.Configuration;
    using Model;
    public class EmailSender
    {
        private readonly IConfiguration _myConfiguration;
        public EmailSender(IConfiguration myConfiguration)
        {
            _myConfiguration = myConfiguration;
        }
        public void sendMail(EmailModel emailModel)
        {
            IConfigurationSection emailConfiguration = _myConfiguration.GetSection("EmailSettings");

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = emailConfiguration["Host"];
            client.Port = Convert.ToInt32(emailConfiguration["Port"]);
            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(emailConfiguration["From"], emailConfiguration["Password"]);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            //can be obtained from your model
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(emailConfiguration["From"]);
            msg.To.Add(new MailAddress(emailModel.To));

            msg.Subject = emailModel.Subject;
            msg.IsBodyHtml = false;
            msg.Body = emailModel.Body;

            client.Send(msg);
        }
    }
}
