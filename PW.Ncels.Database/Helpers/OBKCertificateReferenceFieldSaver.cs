using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;
using System.Reflection;

namespace PW.Ncels.Database.Helpers
{
    public class OBKCertificateReferenceFieldSaver
    {
        Dictionary<string, string> propertyNameRuList;
        NcelsEntities db = UserHelper.GetCn();

        public OBKCertificateReferenceFieldSaver()
        {
            propertyNameRuList = new Dictionary<string, string>();
            propertyNameRuList.Add(nameof(OBK_CertificateReference.Number), PW.Ncels.Database.Recources.Messages.Number);
            propertyNameRuList.Add(nameof(OBK_CertificateReference.CertificateNumber), PW.Ncels.Database.Recources.Messages.CertificateNumber);
            propertyNameRuList.Add(nameof(OBK_CertificateReference.StartDate), PW.Ncels.Database.Recources.Messages.CertificateTermStart);
            propertyNameRuList.Add(nameof(OBK_CertificateReference.EndDate), PW.Ncels.Database.Recources.Messages.CertificateTermEnd);
            propertyNameRuList.Add(nameof(OBK_CertificateReference.CertificateCountryId), PW.Ncels.Database.Recources.Messages.CertificateCountry);
            propertyNameRuList.Add(nameof(OBK_CertificateReference.CertificateOrganization), PW.Ncels.Database.Recources.Messages.ProducerName);
            propertyNameRuList.Add(nameof(OBK_CertificateReference.LastInspection), PW.Ncels.Database.Recources.Messages.LastInspection);
            propertyNameRuList.Add(nameof(OBK_CertificateReference.CertificateTypeId), PW.Ncels.Database.Recources.Messages.Type);
            propertyNameRuList.Add(nameof(OBK_CertificateReference.CertificateValidityTypeId), "Статус");
        }

        public void FieldSave(OBK_CertificateReference oldModel, OBK_CertificateReference newModel, Guid userId)
        {
            if ((newModel.Number == null && oldModel.Number != null) || (newModel.Number != null && !newModel.Number.Equals(oldModel.Number)))
            {
                LogChange(nameof(newModel.Number), newModel.Number.ToString(), userId,
                  newModel.Id, propertyNameRuList.First(x => x.Key == nameof(newModel.Number)).Value);
            }
            if ((newModel.CertificateNumber == null && oldModel.CertificateNumber != null)
                || (newModel.CertificateNumber != null && !newModel.CertificateNumber.Equals(oldModel.CertificateNumber)))
            {
                LogChange(nameof(newModel.CertificateNumber), newModel.CertificateNumber.ToString(), userId,
                  newModel.Id, propertyNameRuList.First(x => x.Key == nameof(newModel.CertificateNumber)).Value);
            }
            if ((newModel.StartDate == null && oldModel.StartDate != null)
                || (newModel.StartDate != null && !newModel.StartDate.Equals(oldModel.StartDate)))
            {
                LogChange(nameof(newModel.StartDate), newModel.StartDate.ToString(), userId,
              newModel.Id, propertyNameRuList.First(x => x.Key == nameof(newModel.StartDate)).Value);
            }
            if ((newModel.EndDate == null && oldModel.EndDate != null)
                || (newModel.EndDate != null && !newModel.EndDate.Equals(oldModel.EndDate)))
            {
                LogChange(nameof(newModel.EndDate), newModel.EndDate.ToString(), userId,
                  newModel.Id, propertyNameRuList.First(x => x.Key == nameof(newModel.EndDate)).Value);
            }
            if ((newModel.CertificateOrganization == null && oldModel.CertificateOrganization != null)
                || (newModel.CertificateOrganization != null && !newModel.CertificateOrganization.Equals(oldModel.CertificateOrganization)))
            {
                LogChange(nameof(newModel.CertificateOrganization), newModel.CertificateOrganization.ToString(), userId,
                newModel.Id, propertyNameRuList.First(x => x.Key == nameof(newModel.CertificateOrganization)).Value);
            }
            if (!newModel.CertificateTypeId.Equals(oldModel.CertificateTypeId))
            {
                LogChange(nameof(newModel.CertificateTypeId), newModel.CertificateTypeId.ToString(), userId,
                newModel.Id, propertyNameRuList.First(x => x.Key == nameof(newModel.CertificateTypeId)).Value);
            }
            if ((newModel.LastInspection == null && oldModel.LastInspection != null)
                || (newModel.LastInspection != null && !newModel.LastInspection.Equals(oldModel.LastInspection)))
            {
                LogChange(nameof(newModel.LastInspection), newModel.LastInspection.ToString(), userId,
                newModel.Id, propertyNameRuList.First(x => x.Key == nameof(newModel.LastInspection)).Value);
            }
            if ((newModel.CertificateCountryId == null && oldModel.CertificateCountryId != null)
                || (newModel.CertificateCountryId != null && !newModel.CertificateCountryId.Equals(oldModel.CertificateCountryId)))
            {
                LogChange(nameof(newModel.CertificateCountryId), newModel.CertificateCountryId.ToString(), userId,
                newModel.Id, propertyNameRuList.First(x => x.Key == nameof(newModel.CertificateCountryId)).Value);
            }
            if ((newModel.CertificateValidityTypeId == null && oldModel.CertificateValidityTypeId != null)
               || (newModel.CertificateValidityTypeId != null && !newModel.CertificateValidityTypeId.Equals(oldModel.CertificateValidityTypeId)))
            {
                LogChange(nameof(newModel.CertificateValidityTypeId), newModel.CertificateValidityTypeId.ToString(), userId,
                newModel.Id, propertyNameRuList.First(x => x.Key == nameof(newModel.CertificateValidityTypeId)).Value);
            }

        }

        public OBK_CertificateReference CopyObject(OBK_CertificateReference old)
        {
            return new OBK_CertificateReference
            {
                Id = old.Id,
                Number = old.Number,
                CertificateNumber = old.CertificateNumber,
                StartDate = old.StartDate,
                EndDate = old.EndDate,
                CertificateCountryId = old.CertificateCountryId,
                CertificateOrganization = old.CertificateOrganization,
                CertificateTypeId = old.CertificateTypeId,
                CertificateValidityTypeId = old.CertificateValidityTypeId,
                LastInspection = old.LastInspection
            };
        }

        public void LogChange(string _Controlld, string _ValueField, Guid? _ownerId,
            Guid? _certificateId, string _DisplayField)
        {
            var container = new OBK_CertificateReferenceFieldHistory
            {
                Controlld = _Controlld,
                CreateDate = DateTime.Now,
                ValueField = _ValueField,
                UserId = _ownerId,
                OBK_CertificateReferenceId = _certificateId,
                DisplayField = _DisplayField

            };
            db.OBK_CertificateReferenceFieldHistory.Add(container);
            db.SaveChanges();
        }

    }
}
