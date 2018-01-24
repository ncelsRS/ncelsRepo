using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Repository.Expertise;


namespace PW.Ncels.Database.Repository.OBK
{
    public class LetterWithEdoRepository:ARepository
    {

        public LetterWithEdoRepository()
        {
            AppContext = CreateDatabaseContext();
        }

        public LetterWithEdoRepository(bool isProxy)
        {
            AppContext = CreateDatabaseContext(isProxy);
        }

        public LetterWithEdoRepository(NcelsEntities context) : base(context)
        {
        }

        public IQueryable<OBK_LetterPortalEdo> getSearchResult(string searchString)
        {
          return AppContext.OBK_LetterPortalEdo.Where(s => s.LetterContent.Contains(searchString) || s.OBK_Contract.Number.Contains(searchString) || s.OBK_LetterRegistration.LetterRegName.Contains(searchString));
        }

        public IQueryable<OBK_LetterFromEdo> getSearchResultIncoming(string searchString)
        {
            return AppContext.OBK_LetterFromEdo.Where(s => s.LetterText.Contains(searchString) || s.UserEdo.Contains(searchString) || s.OBK_LetterPortalEdo.OBK_Contract.Number.Contains(searchString) || s.LetterRegNomer.Contains(searchString));
        }

        public IQueryable<OBK_LetterPortalEdo> getLetters(string sortOrder)
        {

            var letters = from s in AppContext.OBK_LetterPortalEdo
                           select s;
            switch (sortOrder)
            {
                case "DataPisma_desc":
                    return letters.OrderByDescending(s => s.CreatedDate);
                case "LetterContent":
                    return letters.OrderBy(s => s.LetterContent);
                case "LetterContent_desc":
                    return letters.OrderByDescending(s => s.LetterContent);
                case "Dogovor_desc":
                    return letters.OrderByDescending(s => s.OBK_Contract.Number);
                case "Dogovor":
                    return letters.OrderBy(s => s.OBK_Contract.Number);
                case "NomerPismo_desc":
                    return letters.OrderByDescending(s => s.OBK_LetterRegistration.LetterRegName);
                case "NomerPismo":
                    return letters.OrderBy(s => s.OBK_LetterRegistration.LetterRegName);
                default:
                    return letters.OrderBy(s => s.CreatedDate);
            }
        }


        public IQueryable<OBK_LetterFromEdo> getLettersIncoming(string sortOrder)
        {

            var letters = from s in AppContext.OBK_LetterFromEdo
                          select s;
            switch (sortOrder)
            {
                case "DataPisma_desc":
                    return letters.OrderByDescending(s => s.LetterRegDate);
                case "Sender":
                    return letters.OrderBy(s => s.UserEdo);
                case "Sender_desc":
                    return letters.OrderByDescending(x => x.UserEdo);

                case "Otvet_desc":
                    return letters.OrderByDescending(x => x.OBK_LetterPortalEdo.OBK_Contract.Number); 

                case "Otvet":
                    return letters.OrderBy(x => x.OBK_LetterPortalEdo.OBK_Contract.Number);

                case "Nomer_desc":
                    return letters.OrderByDescending(s => s.LetterRegNomer);

                case "Nomer":
                    return letters.OrderBy(s => s.LetterRegNomer);

                case "Content_desc":
                    return letters.OrderByDescending(s => s.LetterText);

                case "Content":
                    return letters.OrderBy(s => s.LetterText);

                default:
                    return letters.OrderBy(s => s.LetterRegDate);
            }
        }
    }
}
