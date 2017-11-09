using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class MaterialDirectionRepository : ARepositoryGeneric<EXP_MaterialDirections>
    {
        public readonly string MaterialDirectionCode = "MaterialDirection";

        public MaterialDirectionRepository(bool isProxy = true):base(isProxy)
        { }

        public void CreateMaterialDirection(Guid drugDeclarationId)
        {
            var statusId = AppContext.Dictionaries.Where(d => d.Type == Dictionary.MaterialDirectionStatusDic.DicCode && d.Code == Dictionary.MaterialDirectionStatusDic.Created)
                .Select(d => d.Id).FirstOrDefault();

            EXP_MaterialDirections mdirection = new EXP_MaterialDirections()
            {
                DrugDeclarationId = drugDeclarationId,
                CreateDate = DateTime.Now,
                Number = $"{DateTime.Now.Year}/{Registrator.GetNumber(MaterialDirectionCode)}",
                StatusId = statusId
            };
            AppContext.EXP_MaterialDirections.Add(mdirection);
            AppContext.SaveChanges();
        }

        public void CreateMaterialDirectionAsync(Guid drugDeclarationId, Employee getExecutorByDicStageId)
        {
            var statusId =
                AppContext.Dictionaries.Where(
                        d => d.Type == Dictionary.MaterialDirectionStatusDic.DicCode &&
                             d.Code == Dictionary.MaterialDirectionStatusDic.Created)
                    .Select(d => d.Id).FirstOrDefault();

            var number =  $"{GetPrefixFromType(drugDeclarationId)}-{Registrator.GetNumber(MaterialDirectionCode)}/{DateTime.Now.Year}";


            EXP_MaterialDirections mdirection = new EXP_MaterialDirections()
            {
                Id = Guid.NewGuid(),
                DrugDeclarationId = drugDeclarationId,
                CreateDate = DateTime.Now,
                ExecutorEmployeeId = getExecutorByDicStageId.Id,
                Number = number,
                StatusId = statusId
            };
            AppContext.EXP_MaterialDirections.Add(mdirection);
            AppContext.SaveChanges();
        }


        private string GetPrefixFromType(Guid drugDeclarationId)
        {
            var typeCode = AppContext.EXP_DrugDeclaration.Where(dd => dd.Id == drugDeclarationId)
                .Select(dd => dd.EXP_DIC_Type.Code)
                .FirstOrDefault();

            string prefix = "Р";
            switch (typeCode)
            {
                case TypeCodeConts.ReRegistration:
                    prefix = "ПР";
                    break;
                case TypeCodeConts.Alteration:
                    prefix = "ВИ";
                    break;
            }
            return prefix;
        }

        public virtual IQueryable<EXP_MaterialDirectionsView> GetMaterialDirectionsViewAsQuarable(
            Expression<Func<EXP_MaterialDirectionsView, bool>> filter = null,
            Func<IQueryable<EXP_MaterialDirectionsView>, IOrderedQueryable<EXP_MaterialDirectionsView>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<EXP_MaterialDirectionsView> query = AppContext.EXP_MaterialDirectionsView;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }
    }
}
