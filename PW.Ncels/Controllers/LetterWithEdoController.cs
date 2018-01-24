using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using System.IO;
using PagedList;
using PW.Ncels.Database.Repository.OBK;
using PW.Ncels.ServiceWithEdo;

namespace PW.Ncels.Controllers
{
    [Authorize()]
    public class LetterWithEdoController : ACommonController
    {
        // GET: LetterWithEdo
        NcelsEntities db = new NcelsEntities();
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "DataPisma_desc" : "";
            ViewBag.LetterContent = sortOrder == "LetterContent" ? "LetterContent_desc" : "LetterContent";
            ViewBag.Dogovor = sortOrder == "Dogovor" ? "Dogovor_desc" : "Dogovor";
            ViewBag.NomerPismo = sortOrder == "NomerPismo" ? "NomerPismo_desc" : "NomerPismo";
            ViewBag.DataPisma = sortOrder == "DataPisma" ? "DataPisma_desc" : "DataPisma";
            LetterWithEdoRepository portalEdo = new LetterWithEdoRepository(false);
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = new List<LetterModel>();

            if (!String.IsNullOrEmpty(searchString))
            {
                list = portalEdo.getSearchResult(searchString).Select(x => new LetterModel {LetterStatusId=x.LetterStatusId, ID = x.ID, contractName = x.OBK_Contract.Number, nomerPisma = x.OBK_LetterRegistration.LetterRegName, CreatedDate = x.CreatedDate, LetterContent = x.LetterContent }).ToList();
            }
            else
            {
                list = portalEdo.getLetters(sortOrder).Select(x => new LetterModel { LetterStatusId = x.LetterStatusId, ID = x.ID, contractName = x.OBK_Contract.Number, nomerPisma = x.OBK_LetterRegistration.LetterRegName, CreatedDate = x.CreatedDate, LetterContent = x.LetterContent }).ToList();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }



        public ActionResult Incoming(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Data = String.IsNullOrEmpty(sortOrder) ? "DataPisma_desc" : "";
            ViewBag.Sender = sortOrder == "Sender" ? "Sender_desc" : "Sender";
            ViewBag.Otvet = sortOrder == "Otvet" ? "Otvet_desc" : "Otvet";
            ViewBag.Nomer = sortOrder == "Nomer" ? "Nomer_desc" : "Nomer";
            ViewBag.Content = sortOrder == "Content" ? "Content_desc" : "Content";
            LetterWithEdoRepository portalEdo = new LetterWithEdoRepository(false);
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var list = new List<LetterModelFromEdo>();
            if (!String.IsNullOrEmpty(searchString))
            {
                list = portalEdo.getSearchResultIncoming(searchString).Select(x => new LetterModelFromEdo { ID = x.ID, nomerDogovora = x.OBK_LetterPortalEdo.OBK_Contract.Number, ObkLetterNumber = x.OBK_LetterPortalEdo.OBK_LetterRegistration.LetterRegName, LetterRegNomer = x.LetterRegNomer, LetterRegDate = x.LetterRegDate, UserEdo=x.UserEdo,LetterText=x.LetterText }).ToList();
            }
            else
            {

                list = portalEdo.getLettersIncoming(sortOrder).OrderByDescending(s => s.LetterRegDate).Select(x => new LetterModelFromEdo { ID = x.ID, nomerDogovora = x.OBK_LetterPortalEdo.OBK_Contract.Number, ObkLetterNumber = x.OBK_LetterPortalEdo.OBK_LetterRegistration.LetterRegName, LetterRegNomer = x.LetterRegNomer, LetterRegDate = x.LetterRegDate, UserEdo = x.UserEdo, LetterText = x.LetterText }).ToList();

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult AddLetter()
        {
            LetterModel model = new LetterModel();
            using (NcelsEntities db= new NcelsEntities())
            {
                var employee = db.Employees.FirstOrDefault(x => x.Login == User.Identity.Name);
                model.AuthorID = employee.Id;
                var Contracts =db.OBK_Contract.Where(x => x.EmployeeId == employee.Id).ToList();
                if (employee != null)
                    ViewData["Contract"] = new SelectList(Contracts, "Id", "Number");
                if (Contracts.Count != 0)
                    ViewBag.AtLeastOne = true;
                else
                    ViewBag.AtLeastOne = false;
            }
            return View(model);
        }


        public ActionResult SignViewStart(LetterModel model)
        {
            using (NcelsEntities db = new NcelsEntities())
            {
                var data = db.OBK_LetterAttachments.Where(x => x.LetterId == model.ID).Select(x => new AttachDoc {
                    AttachmentName = x.AttachmentName,
                    ID = x.ID,
                    isSigned = string.IsNullOrEmpty(x.SignXmlBigData)? false :true,              
                }).ToList();
                model.listDoc = data;
            }
            return View(model);
        }


        public ActionResult SignOperation(long id)
        {
            using (NcelsEntities db = new NcelsEntities())
            {
                    LetterModelSign sign = new LetterModelSign();
                    var data = db.OBK_LetterAttachments.Where(x => x.ID == id).Select(x => new { x.ContentFile }).FirstOrDefault();
                    sign.attachment = data.ContentFile;
                    bool IsSuccess = true;
                    string preambleXml = string.Empty;
                    try
                    {
                        preambleXml = SerializeHelper.SerializeDataContract(sign);
                        preambleXml = preambleXml.Replace("utf-16", "utf-8");
                    }
                    catch (Exception)
                    {
                        IsSuccess = false;
                    }

                    return Json(new
                    {
                        IsSuccess,
                        preambleXml
                    }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult SignForm(long id, string xmlAuditForm)
        {
            bool success = true;
            var counter = 0;
            using (NcelsEntities db = new NcelsEntities())
            {
                try {
                    OBK_LetterAttachments attach = db.OBK_LetterAttachments.Where(x => x.ID == id).FirstOrDefault();
                    attach.SignXmlBigData = xmlAuditForm;
                    db.SaveChanges();
                    var countSigned = db.OBK_LetterAttachments.Count((x => (x.SignXmlBigData == null || x.SignXmlBigData=="") && x.LetterId== attach.LetterId));
                    counter = countSigned;
                    if (counter == 0)
                    {
                        OBK_LetterPortalEdo partal = db.OBK_LetterPortalEdo.Where(x => x.ID == attach.LetterId).FirstOrDefault();
                        OBK_LetterRegistration regPartal = db.OBK_LetterRegistration.Where(x => x.ID == partal.OBKLetterRegID).FirstOrDefault();
                        partal.LetterStatusId = 2;
                        db.SaveChanges();
                        //CallWebServiceWithEDO
                        var employee = db.Employees.FirstOrDefault(x => x.Login == User.Identity.Name);
                        ActionClient client = new ActionClient();
                        InParameters parameters = new InParameters();
                        parameters.date_letter = partal.CreatedDate.ToString("dd.MM.yyyy");
                        parameters.id_contact = partal.ID.ToString();
                        parameters.id_edo = "";
                        parameters.id_letter_obk = regPartal.ID.ToString();
                        parameters.id_letter_user = regPartal.LetterRegDate.Value.ToString("dd.MM.yyyy");
                        parameters.letter_text = partal.LetterContent;
                        parameters.user_obk = employee.LastName;


                        OBK_LetterAttachments attachFile = db.OBK_LetterAttachments.Where(x => x.LetterId == partal.ID).FirstOrDefault();
                        List<Attachment> lists = new List<Attachment>();
                        Attachment attachs = new Attachment();
                        sbyte[] signed = Array.ConvertAll(attachFile.ContentFile, b => unchecked((sbyte)b));
                        attachs.content = Convert.ToBase64String(attachFile.ContentFile);
                        attachs.name = attachFile.AttachmentName;
                        attachs.sign = attachFile.SignXmlBigData;
                        lists.Add(attachs);
                        parameters.attachments = lists.ToArray();
                        var datasend = client.sendINDocument(parameters);

                        if (datasend)
                        {

                        }
                    }
                }
                catch (Exception e)
                {
                    success = false;
                }
            }
            return Json(new { success, counter }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLetter(LetterModel model)
        {
            if (model != null)
            {
                using (NcelsEntities db = new NcelsEntities())
                {
                    OBK_LetterRegistration letterRegistartin = new OBK_LetterRegistration();
                    letterRegistartin.LetterRegDate = model.LetterRegDate;
                    letterRegistartin.LetterRegName = model.RegName;
                    db.OBK_LetterRegistration.Add(letterRegistartin);
                    db.SaveChanges();


                    OBK_LetterPortalEdo data = new OBK_LetterPortalEdo();
                    data.LetterContent = model.LetterContent;
                    data.CreatedDate = DateTime.Now;
                    data.ContractId = model.ContractId;
                    data.AuthorID = model.AuthorID;
                    data.LetterContent = model.LetterContent;
                    data.OBKLetterRegID = letterRegistartin.ID;
                    data.LetterStatusId = model.LetterStatusId;//Pismo prosto sohranen na base
                    db.OBK_LetterPortalEdo.Add(data);
                    db.SaveChanges();

                    foreach (HttpPostedFileBase file in model.files)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            string InputFileName = Path.GetFileName(file.FileName);
                            OBK_LetterAttachments attach = new OBK_LetterAttachments();
                            attach.AttachmentName = InputFileName;
                            attach.ContentFile = StreamToByteArray(file.InputStream);
                            attach.LetterId = data.ID;
                            db.OBK_LetterAttachments.Add(attach);
                        }

                    }
                    model.ID = data.ID;
                    db.SaveChanges();
                    if (model.LetterStatusId == 2)
                    {
                        var dataDocs = db.OBK_LetterAttachments.Where(x => x.LetterId == model.ID).Select(x => new AttachDoc
                        {
                            AttachmentName = x.AttachmentName,
                            ID = x.ID,
                            isSigned = string.IsNullOrEmpty(x.SignXmlBigData) ? false : true,
                        }).ToList();
                        model.listDoc = dataDocs;
                       
                    }
                    db.SaveChanges();

                }

            }
            if (model.LetterStatusId == 2)
            {
                if (model.listDoc.Count != 0)
                    return View("SignViewStart", model);
                else
                {
                    //WebSerice
                    return RedirectToAction("Index");
                }
            }
            else
                return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult EditGet(long id)
        {
            LetterModel model = new LetterModel();
            using (NcelsEntities db = new NcelsEntities())
            {
                OBK_LetterPortalEdo dataFromDB = db.OBK_LetterPortalEdo.Where(x => x.ID == id).FirstOrDefault();
                if (dataFromDB != null)
                {
                    model.ID = dataFromDB.ID;
                    model.LetterContent = dataFromDB.LetterContent;
                    model.ContractId = dataFromDB.ContractId;
                    model.RegName = dataFromDB.OBK_LetterRegistration.LetterRegName;
                    model.LetterRegDate = dataFromDB.OBK_LetterRegistration.LetterRegDate;
                    model.AuthorID = dataFromDB.AuthorID;
                    model.LetterStatusId = dataFromDB.LetterStatusId;
                    ViewData["Contract"] = new SelectList(db.OBK_Contract.Where(x => x.EmployeeId == dataFromDB.AuthorID).ToList(), "Id", "Number", dataFromDB.ContractId);
                    var dataDocs = db.OBK_LetterAttachments.Where(x => x.LetterId == model.ID).Select(x => new AttachDoc
                    {
                        AttachmentName = x.AttachmentName,
                        ID = x.ID,
                        isSigned = string.IsNullOrEmpty(x.SignXmlBigData) ? false : true,
                    }).ToList();
                    model.listDoc = dataDocs;
                }

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DetailsIncoming(long id)
        {
            LetterModelFromEdo model = new LetterModelFromEdo();
            using (NcelsEntities db = new NcelsEntities())
            {
                OBK_LetterFromEdo dataFromDB = db.OBK_LetterFromEdo.Where(x => x.ID == id).FirstOrDefault();
                if (dataFromDB != null)
                {
                    model.ID = dataFromDB.ID;
                    model.nomerDogovora = dataFromDB.OBK_LetterPortalEdo.OBK_Contract.Number;
                    model.ObkLetterNumber = dataFromDB.OBK_LetterPortalEdo.OBK_LetterRegistration.LetterRegName;
                    model.LetterRegNomer = dataFromDB.LetterRegNomer;
                    model.LetterRegDate = dataFromDB.LetterRegDate;
                    model.UserEdo = dataFromDB.UserEdo;
                    model.LetterText = dataFromDB.LetterText;
                    model.senderOrg = "НЦЭЛС";
                }

            }
            return View(model);
        }


        [HttpGet]
        public ActionResult DeleteIncoming(long id)
        {
            LetterModelFromEdo model = new LetterModelFromEdo();
            model.ID = id;

            return View(model);
        }


        [HttpPost]
        public ActionResult DeleteIncoming(LetterModelFromEdo model)
        {
            using (NcelsEntities db = new NcelsEntities())
            {
                OBK_LetterFromEdo edo = db.OBK_LetterFromEdo.Where(x => x.ID == model.ID).FirstOrDefault();
                if (edo != null)
                {
                    db.OBK_LetterFromEdo.Remove(edo);
                    db.SaveChanges();
                }
            }
                return RedirectToAction("Incoming");
        }

        [HttpGet]
        public ActionResult DeleteOutgoing(long id)
        {
            LetterModel model = new LetterModel();
            model.ID = id;

            return View(model);
        }


        [HttpPost]
        public ActionResult DeleteOutgoing(LetterModel model)
        {
            using (NcelsEntities db = new NcelsEntities())
            {
                OBK_LetterPortalEdo edo = db.OBK_LetterPortalEdo.Where(x => x.ID == model.ID).FirstOrDefault();
                if (edo != null)
                {
                    db.OBK_LetterPortalEdo.Remove(edo);
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand(String.Format("delete from OBK_LetterAttachments where LetterId={0}", model.ID));
               //     db.OBK_LetterRegistration.Remove(db.OBK_LetterRegistration.Where(x=>x.ID==edo.OBKLetterRegID).FirstOrDefault());
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(LetterModel model)
        {
            using (NcelsEntities db = new NcelsEntities())
            {
                OBK_LetterPortalEdo dataFromDB = db.OBK_LetterPortalEdo.Where(x => x.ID == model.ID).FirstOrDefault();
                if (dataFromDB != null)
                {
                    dataFromDB.LetterContent = model.LetterContent;
                    dataFromDB.ContractId = model.ContractId;
                    dataFromDB.OBK_LetterRegistration.LetterRegName = model.RegName;
                    dataFromDB.OBK_LetterRegistration.LetterRegDate = model.LetterRegDate;
                    db.SaveChanges();


                    foreach (HttpPostedFileBase file in model.files)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            string InputFileName = Path.GetFileName(file.FileName);
                            OBK_LetterAttachments attach = new OBK_LetterAttachments();
                            attach.AttachmentName = InputFileName;
                            attach.ContentFile = StreamToByteArray(file.InputStream);
                            attach.LetterId = dataFromDB.ID;
                            db.OBK_LetterAttachments.Add(attach);
                            db.SaveChanges();
                        }

                    }
                }

            }
            return RedirectToAction("Index");
        }

        public byte[] StreamToByteArray(Stream instream)
        {
            byte[] data;
            using (Stream inputStream = instream)
            {
                MemoryStream mem = inputStream as MemoryStream;
                if (mem == null)
                {
                    mem = new MemoryStream();
                    inputStream.CopyTo(mem);
                }

                data = mem.ToArray();
            }
            return data;
        }




    }
}