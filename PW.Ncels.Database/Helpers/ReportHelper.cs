using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Ncels.Database.Helpers
{
	public class ReportHelper
	{

		public static List<DictionaryInfo> GetList()
		{
			XmlReader reader = XmlReader.Create(HttpContext.Current.Server.MapPath(@"..\Content\Xml\Reports.xml"));
			XDocument doc = XDocument.Load(reader);
			XElement xElement = doc.Element("Dictionaries");
			if (xElement == null)
				return new List<DictionaryInfo>();
			return xElement.
				Elements().Select(o => new DictionaryInfo {
					NameRu = o.Attribute("Name").Value,
					TypeRu = o.Attribute("Type").Value,
					NameKz = o.Attribute("NameKz").Value,
					TypeKz = o.Attribute("TypeKz").Value,
					Description = o.Attribute("Description").Value,
					Report = o.Attribute("Report").Value
				}).OrderBy(o => o.Name).ToList();
		}

        public static List<DictionaryInfo> GetListExpertise()
        {
            XmlReader reader = XmlReader.Create(HttpContext.Current.Server.MapPath(@"..\Content\Xml\ReportsExpertise.xml"));
            XDocument doc = XDocument.Load(reader);
            XElement xElement = doc.Element("Dictionaries");
            if (xElement == null)
                return new List<DictionaryInfo>();
            return xElement.
                Elements().Select(o => new DictionaryInfo
                {
                    NameRu = o.Attribute("Name").Value,
                    TypeRu = o.Attribute("Type").Value,
                    NameKz = o.Attribute("NameKz").Value,
                    TypeKz = o.Attribute("TypeKz").Value,
                    Description = o.Attribute("Description").Value,
                    Report = o.Attribute("Report").Value
                }).OrderBy(o => o.Name).ToList();
        }


        public static List<DictionaryInfo> GetListDepartment() {
			XmlReader reader = XmlReader.Create(HttpContext.Current.Server.MapPath(@"..\Content\Xml\ReportsDepartament.xml"));
			XDocument doc = XDocument.Load(reader);
			XElement xElement = doc.Element("Dictionaries");
			if (xElement == null)
				return new List<DictionaryInfo>();
			return xElement.
				Elements().Select(o => new DictionaryInfo {
					NameRu = o.Attribute("Name").Value,
					TypeRu = o.Attribute("Type").Value,
					NameKz = o.Attribute("NameKz").Value,
					TypeKz = o.Attribute("TypeKz").Value,
					Description = o.Attribute("Description").Value,
					Report = o.Attribute("Report").Value
				}).OrderBy(o => o.Name).ToList();
		}
		public static List<DictionaryInfo> GetListDepartmentTMC() {
			XmlReader reader = XmlReader.Create(HttpContext.Current.Server.MapPath(@"..\Content\Xml\ReportsDepartamentTMC.xml"));
			XDocument doc = XDocument.Load(reader);
			XElement xElement = doc.Element("Dictionaries");
			if (xElement == null)
				return new List<DictionaryInfo>();
			return xElement.
				Elements().Select(o => new DictionaryInfo {
					NameRu = o.Attribute("Name").Value,
					TypeRu = o.Attribute("Type").Value,
					NameKz = o.Attribute("NameKz").Value,
					TypeKz = o.Attribute("TypeKz").Value,
					Description = o.Attribute("Description").Value,
					Report = o.Attribute("Report").Value
				}).OrderBy(o => o.Name).ToList();
		}

		public static List<DictionaryInfo> GetListReport(string type) {
			XmlReader reader = XmlReader.Create(HttpContext.Current.Server.MapPath(@"..\Content\Xml\" + type + ".xml"));
			XDocument doc = XDocument.Load(reader);
			XElement xElement = doc.Element("Dictionaries");
			if (xElement == null)
				return new List<DictionaryInfo>();
			return xElement.
				Elements().Select(o => new DictionaryInfo {
					NameRu = o.Attribute("Name").Value,
					TypeRu = o.Attribute("Type").Value,
					NameKz = o.Attribute("NameKz").Value,
					TypeKz = o.Attribute("TypeKz").Value,
					Description = o.Attribute("Description").Value,
					Report = o.Attribute("Report").Value
				}).OrderBy(o => o.Name).ToList();
		}

	    public static StiReport GetReport(Guid id, ncelsEntities db)
	    {
            var project = db.ProjectsViews.FirstOrDefault(m => m.Id == id);
            StiReport report = new StiReport();
            if (project != null) {
                if (project.IsRegisterProject != null && project.IsRegisterProject.Value)
                {
                    return GetExaminationDrug(id, db, project);
                    switch (project.Type) {
                        case 0:
                            report.Load(HttpContext.Current.Server.MapPath("~/Reports/Mrts/RegisterLs.mrt"));
                            break;
                        case 1:
                            report.Load(HttpContext.Current.Server.MapPath("~/Reports/Mrts/ReRegisterLs.mrt"));
                            break;
                        case 2:
                            report.Load(HttpContext.Current.Server.MapPath("~/Reports/Mrts/ChRegisterLs.mrt"));
                            break;
                    }
                    var registerProject = db.RegisterProjects.First(m => m.Id == id);
                    if (registerProject.ContractId != null) {
                        var contract = db.Contracts.FirstOrDefault(m => m.Id == registerProject.ContractId.Value);
                        report.Dictionary.Variables["ManufacturerId"].ValueObject = contract.ManufacturerOrganizationId;
                        report.Dictionary.Variables["HolderId"].ValueObject = contract.HolderOrganizationId;
                        report.Dictionary.Variables["ProxyId"].ValueObject = contract.ApplicantOrganizationId;
                    }
                    report.Dictionary.Variables["RegisterProjectId"].ValueObject = id;
                }
                else {
                    switch (project.Type) {
                        case 0:
                            report.Load(HttpContext.Current.Server.MapPath("~/Reports/Mrts/PriceLs.mrt"));
                            break;
                        case 1:
                            report.Load(HttpContext.Current.Server.MapPath("~/Reports/Mrts/PriceImn.mrt"));
                            break;
                        case 2:
                            report.Load(HttpContext.Current.Server.MapPath("~/Reports/Mrts/RePriceLs.mrt"));
                            break;
                        case 3:
                            report.Load(HttpContext.Current.Server.MapPath("~/Reports/Mrts/RePriceImn.mrt"));
                            break;
                    }
                    var priceProject = db.PriceProjects.FirstOrDefault(m => m.Id == id);
                    report.Dictionary.Variables["PriceProjectsId"].ValueObject = id;
                    report.Dictionary.Variables["ManufacturerId"].ValueObject = priceProject.ManufacturerOrganizationId;
                    report.Dictionary.Variables["HolderId"].ValueObject = priceProject.HolderOrganizationId;
                    report.Dictionary.Variables["ProxyId"].ValueObject = priceProject.ProxyOrganizationId;
                    report.Dictionary.Variables["DocumentUrl"].ValueObject = HttpContext.Current.Request.Url.AbsoluteUri;
                }
            }
            else {
                report.Load(HttpContext.Current.Server.MapPath("~/Reports/Mrts/Contract.mrt"));
                report.Dictionary.Variables["ContractId"].ValueObject = id;
                var contract = db.Contracts.FirstOrDefault(m => m.Id == id);
                if (contract != null) {
                    report.Dictionary.Variables["ManufacturerOrganizationId"].ValueObject = contract.ManufacturerOrganizationId;
                    report.Dictionary.Variables["PayerOrganizationId"].ValueObject = contract.PayerOrganizationId;
                    report.Dictionary.Variables["ApplicantOrganizationId"].ValueObject = contract.ApplicantOrganizationId;
                }
            }

            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>()) {
                data.ConnectionString = UserHelper.GetCnString();
            }
	        return report;
	    }
        private static StiReport GetExaminationDrug(Guid id, ncelsEntities db, ProjectsView project)
        {
            var report = new StiReport();
            report.Load(HttpContext.Current.Server.MapPath("~/Reports/Mrts/ExaminationDrug.mrt"));
            var kindRegistration = "";
            switch (project.Type)
            {
                case 0:
                    kindRegistration = "Регистрация";
                    break;
                case 1:
                    kindRegistration = "Перерегистрация";
                    break;
                case 2:
                    kindRegistration = "Внесение изменений";
                    break;
            }
            report.Dictionary.Variables["KindRegistration"].ValueObject = kindRegistration;

            var registerProject = db.RegisterProjects.First(m => m.Id == id);
            if (registerProject.ContractId != null)
            {
                var contract = db.Contracts.FirstOrDefault(m => m.Id == registerProject.ContractId.Value);
                report.Dictionary.Variables["ManufacturerId"].ValueObject = contract.ManufacturerOrganizationId;
                report.Dictionary.Variables["HolderId"].ValueObject = contract.HolderOrganizationId;
                report.Dictionary.Variables["ProxyId"].ValueObject = contract.ApplicantOrganizationId;
            }
            report.Dictionary.Variables["RegisterProjectId"].ValueObject = id;
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            return report;
        }

        public static MemoryStream GetReportPdf(Guid id, ncelsEntities db)
	    {
	        StiReport report = GetReport(id, db);
            report.Render(false);
            var stream = new MemoryStream();

            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
	        return stream;
	    }
	}
}