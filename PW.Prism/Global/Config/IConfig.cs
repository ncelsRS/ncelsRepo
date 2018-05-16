using System.Linq;

namespace PW.Prism.Global.Config
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
