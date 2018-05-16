using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using Ncels.Helpers;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Global.Config;

namespace PW.Ncels.Database.Helpers
{
	public class MailSender  {
		
	
		public IConfig Config { get; set; }

		public MailSender()
		{
			Config = Global.Config.Config.Instance();
		}

		public void SendMail(string email, string subject, string body, MailAddress mailAddress = null) {
            var paramsList = "email: " + email + "; subject" + subject + ";" + "body:" + body;
			try
            {
                if (Config.EnableMail) {
					if (mailAddress == null) {
						mailAddress = new MailAddress(Config.MailSetting.SmtpReply, Config.MailSetting.SmtpUser);
					}
					MailMessage message = new MailMessage(
						mailAddress,
						new MailAddress(email)) {
							Subject = subject,
							BodyEncoding = Encoding.UTF8,
							Body = body,
							IsBodyHtml = true,
							SubjectEncoding = Encoding.UTF8,
						};
					SmtpClient client = new SmtpClient {
						Host = Config.MailSetting.SmtpServer,
						Port = Config.MailSetting.SmtpPort,
						UseDefaultCredentials = false,
						EnableSsl = Config.MailSetting.EnableSsl,
						Credentials =
							new NetworkCredential(Config.MailSetting.SmtpUserName,
												  Config.MailSetting.SmtpPassword),
						DeliveryMethod = SmtpDeliveryMethod.Network
					};
					
					client.Send(message);
				} else {
                    LogHelper.Log.Info("Send mail disable. params:" + paramsList);
                }
            } catch (Exception ex) {
                LogHelper.Log.Error("Send mail trouble. params:" + paramsList+ " error: " + ex.Message );
            }
        }

		public void SendMail(string email, string subject, string body, string documentId, string files, Guid docId, List<string> listEmail, MailAddress mailAddress = null)
		{

			Unit unit = UserHelper.GetCurrentEmployee().Organization;
				if (Config.EnableMail) {
					if (mailAddress == null) {
						

						//mailAddress = new MailAddress(Config.MailSetting.SmtpReply, Config.MailSetting.SmtpUser);
						mailAddress = new MailAddress(UserHelper.GetCurrentEmployee().Organization.Email, UserHelper.GetCurrentEmployee().Organization.Name, Encoding.UTF8);
					}
					MailMessage message = new MailMessage() {
							Sender = mailAddress,
							Subject = subject,
							From = new MailAddress(unit.Email),
							ReplyTo = new MailAddress(unit.Email),
							BodyEncoding = Encoding.UTF8,
							Body = body,
							IsBodyHtml = true,
							SubjectEncoding = Encoding.UTF8,
						};
					string[] emails = email.Split(';');
					foreach (var em in emails) {
						if (em.Trim() != string.Empty && !listEmail.Contains(em.Trim())) {
							message.To.Add(em.Trim());
						}
					}

				
					if (files != null)
					{
						string[] attach = files.Split(';');
						foreach (string file in attach)
						{
							message.Attachments.Add(new Attachment(FileHelper.GetFileName(documentId, file)));
						}
					}
					SmtpClient client = new SmtpClient {
						Host = Config.MailSetting.SmtpServer,
						Port = Config.MailSetting.SmtpPort,
					
						UseDefaultCredentials = false,
						EnableSsl = Config.MailSetting.EnableSsl,
						Credentials =
							new NetworkCredential(Config.MailSetting.SmtpUserName,
												  Config.MailSetting.SmtpPassword),
						DeliveryMethod = SmtpDeliveryMethod.Network
					};

					if (message.To.Count != 0)
					{
						client.Send(message);
					}


					ncelsEntities db = UserHelper.GetCn();
					DateTime dt = DateTime.Now;
					Guid groupId = new Guid();
					foreach (string e in emails)
						db.Histories.Add(GetHistory(e, subject, body, files, dt, groupId, docId));
					db.SaveChanges();
				}
		}

		private History GetHistory(string email, string subject, string body, string files, DateTime dateTime, Guid groupId, Guid objId) {
			History history = new History {
				ColumnName = subject,
				CreatedTime = dateTime,
				GroupId = groupId,
				Ip = HttpContext.Current.Request.UserHostAddress,
				NewValue = body,
				OldValue = files,
				ObjectId = objId,
				OperationId = "EMAIL",
				Record = email,
				TableName = "EMAIL",
				UserName = UserHelper.GetCurrentName()
			};

			return history;
		}
	}
}