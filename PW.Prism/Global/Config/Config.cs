using System.Configuration;
using System.Linq;
using Aspose.Pdf;

namespace PW.Prism.Global.Config
{
    public class Config : IConfig
    {

	    private static IConfig _config;
	    public static IConfig Instance()
	    {
		    if (_config == null)
		    {
			    _config  = new Config();
		    }
		    return _config;
	    }
        public string ConnectionStrings(string connectionString)
        {
            return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        }
        public string Lang
        {
            get
            {
                return ConfigurationManager.AppSettings["Culture"] as string;
            }
        }

        public bool EnableMail
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["EnableMail"]);
            }
        }


        public IQueryable<MimeType> MimeTypes
        {
            get
            {
                MimeTypesConfigSection configInfo = (MimeTypesConfigSection)ConfigurationManager.GetSection("mimeConfig");
                return configInfo.MimeTypes.OfType<MimeType>().AsQueryable<MimeType>();
            }
        }

        public IQueryable<MailTemplate> MailTemplates
        {
            get
            {
                MailTemplateConfigSection configInfo = (MailTemplateConfigSection)ConfigurationManager.GetSection("mailTemplateConfig");
                return configInfo.MailTemplates.OfType<MailTemplate>().AsQueryable<MailTemplate>();
            }
        }

        public MailSetting MailSetting
        {
            get 
            { 
                return (MailSetting)ConfigurationManager.GetSection("mailConfig");
            }
        }


    
    }
}