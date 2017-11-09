using System.Linq;

namespace PW.Ncels.Database.Global.Config
{
    public interface IConfig
    {
        string ConnectionStrings(string connectionString);

        string Lang { get; }

        bool EnableMail { get; }


        IQueryable<MimeType> MimeTypes { get; }

        IQueryable<MailTemplate> MailTemplates { get; }

        MailSetting MailSetting { get; }

    }
}
