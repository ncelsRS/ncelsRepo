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

namespace PW.Ncels.Controllers
{
    [Authorize()]
    public class LetterWithEdoController : ACommonController
    {
        // GET: LetterWithEdo
        ncelsEntities db = new ncelsEntities();
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            //ActionClient client = new ActionClient();
            //InParameters parameters = new InParameters();
            //parameters.date_letter = DateTime.Now.ToString();
            //parameters.id_contact = "121212";
            //parameters.id_edo = "";
            //parameters.id_letter_obk = "1";
            //parameters.id_letter_user = "sdsd";
            //parameters.letter_text = "Askaru";
            //parameters.user_obk = "Grebnikova";
            //List<Attachment> lists = new List<Attachment>();
            //Attachment attach = new Attachment();
            //sbyte[] signed = Array.ConvertAll(new Byte[154], b => unchecked((sbyte)b));
            //attach.content = signed;
            //attach.name = "Abzal.docx";
            //attach.sign = "xml";
            //lists.Add(attach);
            //parameters.attachments = lists.ToArray();
            // var datasend=client.sendINDocument(parameters);




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
                list = portalEdo.getSearchResult(searchString).Select(x => new LetterModel { ID = x.ID, contractName = x.OBK_Contract.Number, nomerPisma = x.OBK_LetterRegistration.LetterRegName, CreatedDate = x.CreatedDate, LetterContent = x.LetterContent }).ToList();
            }
            else
            {
                list = portalEdo.getLetters(sortOrder).Select(x => new LetterModel { ID = x.ID, contractName = x.OBK_Contract.Number, nomerPisma = x.OBK_LetterRegistration.LetterRegName, CreatedDate = x.CreatedDate, LetterContent = x.LetterContent }).ToList();
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
            using (ncelsEntities db= new ncelsEntities())
            {
                var employee = db.Employees.FirstOrDefault(x => x.Login == User.Identity.Name);
                model.AuthorID = employee.Id;
                if (employee != null)
                    ViewData["Contract"] = new SelectList(db.OBK_Contract.Where(x=>x.EmployeeId==employee.Id).ToList(), "Id", "Number");
            }
            return View(model);
        }


        public ActionResult SignViewStart(LetterModel model)
        {
            using (ncelsEntities db = new ncelsEntities())
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
            using (ncelsEntities db = new ncelsEntities())
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
            using (ncelsEntities db = new ncelsEntities())
            {
                try {
                    OBK_LetterAttachments attach = db.OBK_LetterAttachments.Where(x => x.ID == id).FirstOrDefault();
                    attach.SignXmlBigData = xmlAuditForm;
                    db.SaveChanges();
                    var countSigned = db.OBK_LetterAttachments.Count(x => x.SignXmlBigData == null || x.SignXmlBigData=="");
                    counter = countSigned;
                    if (counter == 0)
                    {
                        OBK_LetterPortalEdo partal = db.OBK_LetterPortalEdo.Where(x => x.ID == attach.LetterId).FirstOrDefault();
                        partal.LetterStatusId = 2;
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
                using (ncelsEntities db = new ncelsEntities())
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
                    data.LetterStatusId = 1;//Pismo prosto sohranen na base
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
                    var dataDocs = db.OBK_LetterAttachments.Where(x => x.LetterId == model.ID).Select(x => new AttachDoc
                    {
                        AttachmentName = x.AttachmentName,
                        ID = x.ID,
                        isSigned = string.IsNullOrEmpty(x.SignXmlBigData) ? false : true,
                    }).ToList();
                    model.listDoc = dataDocs;
                    db.SaveChanges();


                }

            }
            return View("SignViewStart",model);
        }



        [HttpGet]
        public ActionResult EditGet(long id)
        {
            LetterModel model = new LetterModel();
            using (ncelsEntities db = new ncelsEntities())
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
            using (ncelsEntities db = new ncelsEntities())
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
            using (ncelsEntities db = new ncelsEntities())
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
            using (ncelsEntities db = new ncelsEntities())
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
            using (ncelsEntities db = new ncelsEntities())
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