using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism {
	public partial class ReportForm : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {


			string id = Request["Id"];
			string isPrint = Request["isPrint"];
			if (!string.IsNullOrEmpty(id)) {
				ncelsEntities context = UserHelper.GetCn();
				Guid guid = Guid.Parse(id);

				Document doc =  context.Documents.FirstOrDefault(o => o.Id == guid);
				if (doc != null) {
					//if (doc.Template != null) {
					//	StiReport report = new StiReport();
					//	MemoryStream stream = new MemoryStream(doc.Template.Report);
					//	report.Load(stream);
					//	stream.Close();
					//	foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>()) {
					//		data.ConnectionString = UserHelper.GetCnString();
					//	}


					//	report.Dictionary.Variables["DocumentId"].Value = guid.ToString();
					//	report.Dictionary.Variables["EmployeeId"].Value = UserHelper.GetCurrentEmployee().Id.ToString();
					//	report.Dictionary.Variables["UnitId"].Value = UserHelper.GetCurrentEmployee().Position.Id.ToString();
					//	StiWebViewer1.Report = report;
					//	if (isPrint == "1") {
					//		StiWebViewer1.ShowToolBar = false;
					//		StiWebViewer1.PrintToDirect();
					//	}
					//	return;
					//}

					Template template = null;
					StiReport reportCard = new StiReport();
					switch (doc.DocumentType) {
						case 0:
                            //reportCard.Load(Server.MapPath("Reports/CardInc.mrt"));
                            reportCard = ReportHelper.GetReport(new Guid(id), context);
                            template = context.Templates.FirstOrDefault(o => o.TemplateType == 2 && o.Type == 0);
							break;
						case 1:
							reportCard.Load(Server.MapPath("Reports/CardOut.mrt"));
							template = context.Templates.FirstOrDefault(o => o.TemplateType == 2 && o.Type == 1);
							break;
						case 2:
                            //reportCard.Load(Server.MapPath("Reports/CardCit.mrt"));
                            reportCard = ReportHelper.GetReport(new Guid(id), context);
                            template = context.Templates.FirstOrDefault(o => o.TemplateType == 2 && o.Type == 2);
							break;
						case 3:
							reportCard.Load(Server.MapPath("Reports/CardOrd.mrt"));
							template = context.Templates.FirstOrDefault(o => o.TemplateType == 2 && o.Type == 3);
							break;
						case 4:
							//reportCard.Load(Server.MapPath("Reports/CardProject.mrt"));
                            reportCard = ReportHelper.GetReport(new Guid(id), context);
                            template = context.Templates.FirstOrDefault(o => o.TemplateType == 2 && o.Type == 3);
							break;	
						case 5:
							reportCard.Load(Server.MapPath("Reports/CardInner.mrt"));
							template = context.Templates.FirstOrDefault(o => o.TemplateType == 2 && o.Type == 3);
							break;
						default:
							return;
					}
					//if (template != null) {
						//if (reportCard == null)
						//{
						//	reportCard = new StiReport();
						//	MemoryStream stream = new MemoryStream(template.Report);
						//	reportCard.Load(stream);
						//	stream.Close();
						//}
						foreach (var data in reportCard.Dictionary.Databases.Items.OfType<StiSqlDatabase>()) {
							data.ConnectionString = UserHelper.GetCnString();
						}
				    if (reportCard.Dictionary.Variables.Contains("DocumentId"))
				    {
				        reportCard.Dictionary.Variables["DocumentId"].Value = doc.Id.ToString();
				    }
				    if (reportCard.Dictionary.Variables.Contains("EmployeeId"))
				    {
				        reportCard.Dictionary.Variables["EmployeeId"].Value = UserHelper.GetCurrentEmployee().Id.ToString();
				    }
				    if (reportCard.Dictionary.Variables.Contains("UnitId"))
				    {
				        reportCard.Dictionary.Variables["UnitId"].Value = UserHelper.GetCurrentEmployee().Position.Id.ToString();
				    }
				    StiWebViewer1.Report = reportCard;
						if (isPrint == "1") {
							StiWebViewer1.ShowToolBar = false;
							StiWebViewer1.PrintToDirect();
						}
					//}
				}
			}

		}
	}
}