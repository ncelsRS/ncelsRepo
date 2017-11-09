using System;

namespace PW.Ncels.Database.Models
{
	public class MailSenderModel
	{
		public Guid Id { get; set; }
		public string DocumenetNumber { get; set; }
		public string DocumenetDate { get; set; }
		public string Text { get; set; }
		public string Files { get; set; }

		public string Title { get; set; }
		public string Email { get; set; }
	}
}