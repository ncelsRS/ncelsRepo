using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Controllers
{
    
    public class DictionariesController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();
        // GET: Dictionaries

        [HttpGet]
        public async Task<ActionResult> GetReference(string type, bool orderByName=false)
        {
            var query = db.Dictionaries.Where(o => o.Type == type).Select(o => new {o.Id, o.Name, o.Code, o.NameKz});
            if (orderByName)
                query = query.OrderBy(m => m.Name);
            else
                query = query.OrderBy(m => m.Code);
            return Json(await query.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        public async Task<ActionResult> GetDicByOrderYear(string type)
        {
            return Json(await db.Dictionaries.Where(o => o.Type == type).Select(o => new { o.Id, o.Name, o.Code, o.NameKz, o.Year }).OrderBy(m => m.Year).ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        public async Task<ActionResult> GetCurrencies()
        {
            var dict = await db.Dictionaries.Where(o => o.Type == "Currency").ToListAsync();
            var list = dict.Select(o => new { o.Id, o.Name, o.Code, o.NameKz, o.Year, ordVal = GetCurrencyOrderVal(o.Code) });
            return Json(list.OrderBy(m => m.ordVal).ThenBy(m => m.Year), JsonRequestBehavior.AllowGet);
        }

        private static int GetCurrencyOrderVal(string code)
        {
            switch (code)
            {
                case "398":
                    return 1;
                case "840":
                    return 2;
                case "978":
                    return 3;
                case "826":
                    return 4;
                case "203":
                    return 5;
                case "810":
                    return 6;
                case "974":
                    return 7;
                case "804":
                    return 8;
                case "792":
                    return 9;
                default:
                    return 1000;
            }
        }

        [Authorize()]
        [HttpGet]
        public async Task<ActionResult> GetMyProjects(int type)
        {
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            switch (type)
            {
                case 1:
                    var prices = db.PriceProjectsViews
                          .Join(db.Documents, p => p.Id, d => d.Id, (p, d) => new { p, d })
                        .Where(m => m.p.OwnerId == employeeId).Select(m => new
                        {
                            Id = m.p.Id.ToString(),
                            Number = m.d.Number ?? "б/н",
                            m.p.CreatedDate,
                            Name = m.p.NameRu
                        }).ToListAsync();
                    return Json(await prices, JsonRequestBehavior.AllowGet);
                case 2:
                    var project = db.RegisterProjectsViews
                          .Join(db.Documents, r => r.Id, d => d.Id, (r, d) => new { r, d })
                        .Where(m => m.r.OwnerId == employeeId).Select(m => new
                        {
                            Id = m.r.Id.ToString(),
                            Number = m.d.Number ?? "б/н",
                            m.r.CreatedDate,
                            Name = m.r.NameRu
                        }).ToListAsync();
                    return Json(await project, JsonRequestBehavior.AllowGet);
                case 3:
                    var contract = db.ContractsViews
                          .Join(db.Documents, c => c.Id, d => d.Id, (c, d) => new { c, d })
                        .Where(m => m.c.Status > 0).Select(m => new
                        {
                            Id = m.c.Id.ToString(),
                            Number = m.d.Number ?? "б/н",
                            Name = "Договор",
                            CreatedDate = m.c.CreatedDate ?? DateTime.Now

                        }).ToListAsync();
                    return Json(await contract, JsonRequestBehavior.AllowGet);
                default:
                    return null;
            }
        }

        [Authorize()]
        [HttpGet]
        public async Task<ActionResult> GetParts(int id)
        {
            return Json(await db.sr_register_mt_parts.Where(o => o.register_id == id).Select(o => new { Id = o.id, Name = o.name }).OrderBy(m => m.Name).ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        public async Task<ActionResult> GetRePriceReoson(Guid priceProjectId)
        {
            var rePriceDic3 = DictionaryHelper.GetDicIdByCode("RePrice", "2");
            var date = DateTime.Now.AddMonths(-6);
            bool hasRePrice =
                db.PriceProjects.Any(m => m.PriceProjectId == priceProjectId && m.RePriceDicId == rePriceDic3 && m.CreatedDate > date && m.Status > 0);
            if (hasRePrice)
            {
                return Json(await db.Dictionaries.Where(o => o.Type == "RePrice" && o.Code != "2").Select(o => new { o.Id, o.Name, o.Code, o.NameKz }).OrderBy(m => m.Code).ToListAsync(), JsonRequestBehavior.AllowGet);
            }
            return Json(await db.Dictionaries.Where(o => o.Type == "RePrice").Select(o => new { o.Id, o.Name, o.Code, o.NameKz }).OrderBy(m => m.Code).ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        public async Task<ActionResult> GetDicIdByCode(string dicType, string code){
            var val = DictionaryHelper.GetDicIdByCode(dicType, code);
            return Json(val, JsonRequestBehavior.AllowGet);
        }

        [Authorize()]
        [HttpGet]
        public ActionResult Organizations(string search, int page)
        {
            search = search.ToLower();
            var skip = (page - 1) * 20;
            var query = db.Organizations.Where(
                e => e.OriginalOrgId == null && 
                    e.NameRu.ToLower().Contains(search) || e.NameRu.ToLower().Contains(search) ||
                    e.NameRu.ToLower().Contains(search));
            var pageCount = query.Count() / 20;
            var orgs = query
                .OrderBy(e => e.NameRu).Skip(skip).Take(20)
                .Select(e => new
                {
                    e.Id,
                    Name = e.NameRu
                });
            return Json(new
            {
                lastPage = pageCount,
                items = orgs.ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrganization(Guid id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Organizations.AsNoTracking().FirstOrDefault(e => e.Id == id), JsonRequestBehavior.AllowGet);
        }
    }
}