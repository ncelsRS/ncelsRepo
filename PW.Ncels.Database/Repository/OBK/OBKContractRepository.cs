using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Notifications;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Repository.OBK
{
    public class OBKContractRepository : ARepository
    {
        public OBKContractRepository()
        {
            AppContext = CreateDatabaseContext();
        }

        public OBKContractRepository(bool isProxy)
        {
            AppContext = CreateDatabaseContext(isProxy);
        }

        public OBKContractRepository(ncelsEntities context) : base(context) { }

        public IEnumerable<OBK_ProductInfo> GetSearchReestr(int regType, string regNumber, string tradeName, bool drugEndDateExpired)
        {
            if (regNumber == null && tradeName == null)
                return null;

            ((IObjectContextAdapter)AppContext).ObjectContext.CommandTimeout = 180;

            var reestr = from register in AppContext.sr_register
                         let obkProduct = AppContext.obk_products
                                            .Where(p => p.register_id == register.id)
                                            .OrderBy(p => p.id)
                                            .FirstOrDefault()
                         join srrdr in AppContext.sr_register_drugs on register.id equals srrdr.id into srregisterdrugsTable
                         from srregisterdrugs in srregisterdrugsTable.DefaultIfEmpty()
                         join srrmt in AppContext.sr_register_mt on register.id equals srrmt.id into srregistermtTable
                         from srregistermt in srregistermtTable.DefaultIfEmpty()
                         join srregprod in AppContext.sr_register_producers on register.id equals srregprod.register_id into srregisterproducersTable
                         from srregisterproducer in srregisterproducersTable.DefaultIfEmpty()
                         join srprod in AppContext.sr_producers on srregisterproducer.producer_id equals srprod.id into srproducersTable
                         from srproducers in srproducersTable.DefaultIfEmpty()
                         join srcn in AppContext.sr_countries on srregisterproducer.country_id equals srcn.id into srcountriesTable
                         from srcountries in srcountriesTable.DefaultIfEmpty()
                         where register.reg_type_id == regType &&
                         (string.IsNullOrEmpty(regNumber) || register.reg_number.ToLower().Contains(regNumber.ToLower())) &&
                         (string.IsNullOrEmpty(tradeName) || register.name.ToLower().Contains(tradeName.ToLower())) &&
                         (
                            (!drugEndDateExpired && register.expiration_date != null && register.expiration_date >= DateTime.Now) ||
                            drugEndDateExpired
                         )
                         &&
                         (srregisterproducer.producer_type_id == 1 || srregisterproducer.producer_type_id == null)
                         select new OBK_ProductInfo
                         {
                             ProductId = register.id,
                             RegNumber = register.reg_number,
                             RegNumberKz = register.reg_number_kz,
                             Name = register.name,
                             NameKz = register.name_kz,
                             RegTypeId = register.sr_reg_types.id,
                             RegTypeName = register.sr_reg_types.name,
                             RegDate = register.reg_date,
                             ExpireDate = register.expiration_date,
                             ProducerName = srproducers.name,
                             ProducerNameKz = srproducers.name_kz,
                             CountryName = srcountries.name,
                             CountryNameKz = srcountries.name_kz,
                             TnvedCode = obkProduct.tnved_code,
                             KpvedCode = obkProduct.kpved_code,
                             DegreeRiskId = srregistermt.degree_risk_id,
                             NdName = srregisterdrugs.nd_name,
                             NdNumber = srregisterdrugs.nd_number,
                             RegisterId = register.id
                         };

            if (reestr != null)
            {
                return reestr.ToList();
            }
            return null;
        }

        public OBKContractViewModel SaveContract2(Guid guid, OBKContractViewModel contractViewModel)
        {
            var ret = new OBKContractViewModel();

            var obkContract = AppContext.OBK_Contract.Where(o => o.Id == contractViewModel.Id).FirstOrDefault();
            if (obkContract != null)
            {
                FillContract(contractViewModel, obkContract);
                ret.Id = contractViewModel.Id;
            }
            else
            {
                OBK_Contract contract = new OBK_Contract();
                contract.Id = guid;
                contract.CreatedDate = DateTime.Now;
                contract.Number = CodeConstManager.OBK_CONTRACT_NO_NUMBER;
                contract.Status = CodeConstManager.STATUS_OBK_DRAFT_ID;

                var employeeId = UserHelper.GetCurrentEmployee().Id;
                contract.EmployeeId = employeeId;

                FillContract(contractViewModel, contract);

                AppContext.OBK_Contract.Add(contract);
                AppContext.SaveChanges();
                ret.Id = contract.Id;
                AddExtHistoryDraft(contract.Id);
                AppContext.SaveChanges();
            }
            return ret;
        }

        private void FillContract(OBKContractViewModel contractViewModel, OBK_Contract obkContract)
        {
            obkContract.Type = contractViewModel.Type != 0 ? contractViewModel.Type : 1;
            obkContract.ExpertOrganization = contractViewModel.ExpertOrganization;
            obkContract.Signer = contractViewModel.Signer;
            if (obkContract.DeclarantContactId != null)
            {
                var contactData = AppContext.OBK_DeclarantContact.Where(x => x.Id == obkContract.DeclarantContactId).FirstOrDefault();
                FillContactData(contractViewModel, contactData, obkContract);
                AppContext.SaveChanges();
            }
            else
            {
                OBK_DeclarantContact contactData = new OBK_DeclarantContact();
                contactData.Id = Guid.NewGuid();
                contactData.CreateDate = DateTime.Now;
                contactData.IsHasBossDocNumber = false;
                contactData.IsHasSignDocNumber = false;
                contactData.SignType = false;
                AppContext.OBK_DeclarantContact.Add(contactData);
                AppContext.SaveChanges();
                obkContract.DeclarantContactId = contactData.Id;
            }
            AppContext.SaveChanges();
        }

        private void FillContactData(OBKContractViewModel contractViewModel, OBK_DeclarantContact contactData, OBK_Contract obkContract)
        {
            contactData.AddressLegalRu = contractViewModel.AddressLegalRu;
            contactData.AddressLegalKz = contractViewModel.AddressLegalKz;
            contactData.AddressFact = contractViewModel.AddressFact;
            contactData.Phone = contractViewModel.Phone;
            contactData.Email = contractViewModel.Email;
            contactData.BossLastName = contractViewModel.BossLastName;
            contactData.BossFirstName = contractViewModel.BossFirstName;
            contactData.BossMiddleName = contractViewModel.BossMiddleName;
            contactData.BossPosition = contractViewModel.BossPosition;
            contactData.BossPositionKz = contractViewModel.BossPositionKz;
            contactData.BossDocType = contractViewModel.BossDocType;
            contactData.IsHasBossDocNumber = contractViewModel.IsHasBossDocNumber;
            contactData.BossDocNumber = contractViewModel.BossDocNumber;
            contactData.BossDocCreatedDate = contractViewModel.BossDocCreatedDate;
            contactData.BossDocEndDate = contractViewModel.BossDocEndDate;
            contactData.BossDocUnlimited = contractViewModel.BossDocUnlimited;
            contactData.SignerIsBoss = contractViewModel.SignerIsBoss;
            contactData.SignLastName = contractViewModel.SignLastName;
            contactData.SignFirstName = contractViewModel.SignFirstName;
            contactData.SignMiddleName = contractViewModel.SignMiddleName;
            contactData.SignPosition = contractViewModel.SignPosition;
            contactData.SignPositionKz = contractViewModel.SignPositionKz;
            contactData.SignDocType = contractViewModel.SignDocType;
            contactData.IsHasSignDocNumber = contractViewModel.IsHasSignDocNumber;
            contactData.SignDocNumber = contractViewModel.SignDocNumber;
            contactData.SignDocCreatedDate = contractViewModel.SignDocCreatedDate;
            contactData.SignDocEndDate = contractViewModel.SignDocEndDate;
            contactData.SignDocUnlimited = contractViewModel.SignDocUnlimited;
            contactData.BankIik = contractViewModel.BankIik;
            contactData.BankBik = contractViewModel.BankBik;
            contactData.CurrencyId = contractViewModel.CurrencyId;
            contactData.BankNameRu = contractViewModel.BankNameRu;
            contactData.BankNameKz = contractViewModel.BankNameKz;
            contactData.DeclarantId = obkContract.DeclarantId;
        }

        public OBK_Declarant GetOrganizationData(Guid guid)
        {
            var declarant = AppContext.OBK_Declarant.FirstOrDefault(x => x.Id == guid);
            return declarant;
        }

        /// <summary>
        /// загрузка списка заявлений
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isRegisterProject"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<object> GetContractList(ModelRequest request, bool isRegisterProject)
        {
            try
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                var v = AppContext.OBK_Contract.Where(x => x.EmployeeId == employeeId).AsQueryable();

                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    v =
                        v.Where(
                            a => a.Number.Contains(request.SearchValue));
                }


                //sort
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
                {
                    v = v.OrderByDescending(x => x.CreatedDate);
                }

                int recordsTotal = await v.CountAsync();
                var contractViews = v.Skip(request.Skip).Take(request.PageSize).Select(x => new
                {
                    x.Id,
                    x.Number,
                    x.CreatedDate,
                    Status = x.OBK_Ref_Status.NameRu,
                    DeclarantName = x.OBK_Declarant.NameRu,
                    Type = x.OBK_Ref_Type.NameRu,
                    TypeKz = x.OBK_Ref_Type.NameKz,
                    x.StartDate,
                    x.EndDate,
                    x.ParentId
                }
                );
                return
                    new
                    {
                        draw = request.Draw,
                        recordsFiltered = recordsTotal,
                        recordsTotal = recordsTotal,
                        Data = await contractViews.ToListAsync()
                    };
            }
            catch (Exception e)
            {
                return new { IsError = true, Message = e.Message };
            }
        }

        public object GetService(Guid serivce)
        {
            var service = AppContext.OBK_Ref_PriceList.Where(x => x.Id == serivce).Select(x => new { x.Id, x.Price, UnitOfMeasurementName = x.Dictionary.Name, UnitOfMeasurementId = x.Dictionary.Id }).FirstOrDefault();
            return service;
        }

        public OBKContractViewModel LoadContract(Guid id)
        {
            var OBKContract = AppContext.OBK_Contract.Where(x => x.Id == id).FirstOrDefault();

            OBKContractViewModel contractViewModel = new OBKContractViewModel();
            contractViewModel.Id = OBKContract.Id;
            contractViewModel.Type = OBKContract.Type;
            contractViewModel.ExpertOrganization = OBKContract.ExpertOrganization;
            contractViewModel.Signer = OBKContract.Signer;
            contractViewModel.Status = OBKContract.Status;
            contractViewModel.Number = OBKContract.Number;

            if (OBKContract.DeclarantContactId != null)
            {
                contractViewModel.AddressLegalRu = OBKContract.OBK_DeclarantContact.AddressLegalRu;
                contractViewModel.AddressLegalKz = OBKContract.OBK_DeclarantContact.AddressLegalKz;
                contractViewModel.AddressFact = OBKContract.OBK_DeclarantContact.AddressFact;
                contractViewModel.Phone = OBKContract.OBK_DeclarantContact.Phone;
                contractViewModel.Email = OBKContract.OBK_DeclarantContact.Email;
                contractViewModel.BossLastName = OBKContract.OBK_DeclarantContact.BossLastName;
                contractViewModel.BossFirstName = OBKContract.OBK_DeclarantContact.BossFirstName;
                contractViewModel.BossMiddleName = OBKContract.OBK_DeclarantContact.BossMiddleName;
                contractViewModel.BossPosition = OBKContract.OBK_DeclarantContact.BossPosition;
                contractViewModel.BossPositionKz = OBKContract.OBK_DeclarantContact.BossPositionKz;
                contractViewModel.BossDocType = OBKContract.OBK_DeclarantContact.BossDocType;
                contractViewModel.IsHasBossDocNumber = OBKContract.OBK_DeclarantContact.IsHasBossDocNumber;
                contractViewModel.BossDocNumber = OBKContract.OBK_DeclarantContact.BossDocNumber;
                contractViewModel.BossDocCreatedDate = OBKContract.OBK_DeclarantContact.BossDocCreatedDate;
                contractViewModel.BossDocEndDate = OBKContract.OBK_DeclarantContact.BossDocEndDate;
                contractViewModel.BossDocUnlimited = OBKContract.OBK_DeclarantContact.BossDocUnlimited;
                contractViewModel.SignerIsBoss = OBKContract.OBK_DeclarantContact.SignerIsBoss;
                contractViewModel.SignLastName = OBKContract.OBK_DeclarantContact.SignLastName;
                contractViewModel.SignFirstName = OBKContract.OBK_DeclarantContact.SignFirstName;
                contractViewModel.SignMiddleName = OBKContract.OBK_DeclarantContact.SignMiddleName;
                contractViewModel.SignPosition = OBKContract.OBK_DeclarantContact.SignPosition;
                contractViewModel.SignPositionKz = OBKContract.OBK_DeclarantContact.SignPositionKz;
                contractViewModel.SignDocType = OBKContract.OBK_DeclarantContact.SignDocType;
                contractViewModel.IsHasSignDocNumber = OBKContract.OBK_DeclarantContact.IsHasSignDocNumber;
                contractViewModel.SignDocNumber = OBKContract.OBK_DeclarantContact.SignDocNumber;
                contractViewModel.SignDocCreatedDate = OBKContract.OBK_DeclarantContact.SignDocCreatedDate;
                contractViewModel.SignDocEndDate = OBKContract.OBK_DeclarantContact.SignDocEndDate;
                contractViewModel.SignDocUnlimited = OBKContract.OBK_DeclarantContact.SignDocUnlimited;
                contractViewModel.BankIik = OBKContract.OBK_DeclarantContact.BankIik;
                contractViewModel.BankBik = OBKContract.OBK_DeclarantContact.BankBik;
                contractViewModel.CurrencyId = OBKContract.OBK_DeclarantContact.CurrencyId;
                contractViewModel.BankNameRu = OBKContract.OBK_DeclarantContact.BankNameRu;
                contractViewModel.BankNameKz = OBKContract.OBK_DeclarantContact.BankNameKz;
            }

            return contractViewModel;
        }

        public OBKContractProductViewModel SaveProduct(Guid contractId, OBKContractProductViewModel product)
        {
            var r = new OBKContractProductViewModel();
            r.ProductId = product.ProductId;
            if (product.Id != null)
            {
                OBK_RS_Products productInfo = AppContext.OBK_RS_Products.Where(x => x.Id == product.Id).FirstOrDefault();
                FillProduct(productInfo, product);
                AppContext.SaveChanges();

                DeleteSeries(productInfo.Id);
                SaveSeries(productInfo.Id, product.Series);

                DeleteMtParts(productInfo.Id);
                SaveMtParts(productInfo.Id, product.MtParts);

                SaveProductService(productInfo.Id, product.ServiceName, contractId, product.Series.Count);

                r.Id = productInfo.Id;
            }
            else
            {
                OBK_RS_Products productInfo = new OBK_RS_Products();
                productInfo.ContractId = contractId;
                FillProduct(productInfo, product);
                AppContext.OBK_RS_Products.Add(productInfo);
                AppContext.SaveChanges();

                SaveSeries(productInfo.Id, product.Series);
                SaveMtParts(productInfo.Id, product.MtParts);
                SaveProductService(productInfo.Id, product.ServiceName, contractId, product.Series.Count);

                r.Id = productInfo.Id;
            }
            return r;
        }

        public bool DeleteProduct(Guid contractId, int productId)
        {
            var product = AppContext.OBK_RS_Products.Where(x => x.Id == productId).FirstOrDefault();
            if (product != null)
            {
                AppContext.OBK_MtPart.RemoveRange(AppContext.OBK_MtPart.Where(x => x.ProductId == productId));
                AppContext.OBK_Procunts_Series.RemoveRange(AppContext.OBK_Procunts_Series.Where(x => x.OBK_RS_ProductsId == productId));
                AppContext.OBK_ContractPrice.RemoveRange(AppContext.OBK_ContractPrice.Where(x => x.ProductId == productId));
                AppContext.OBK_RS_Products.Remove(product);
                AppContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<OBKContractProductViewModel> GetProducts(Guid contractId)
        {
            return AppContext.OBK_RS_Products.Where(x => x.ContractId == contractId).Select(x => new OBKContractProductViewModel
            {
                ProductId = null,
                Id = x.Id,
                RegTypeId = x.RegTypeId,
                DegreeRiskId = x.DegreeRiskId,
                NameRu = x.NameRu,
                NameKz = x.NameKz,
                ProducerNameRu = x.ProducerNameRu,
                ProducerNameKz = x.ProducerNameKz,
                CountryNameRu = x.CountryNameRu,
                CountryNameKz = x.CountryNameKZ,
                Price = x.Price,
                DrugFormBoxCount = x.DrugFormBoxCount,
                DrugFormFullName = x.DrugFormFullName,
                DrugFormFullNameKz = x.DrugFormFullNameKz,
                RegisterId = x.RegisterId,
                RegNumber = x.RegNumber,
                RegNumberKz = x.RegNumberKz,
                RegDate = x.RegDate,
                ExpirationDate = x.ExpirationDate,
                NdName = x.NdName,
                NdNumber = x.NdNumber,
                ServiceName = AppContext.OBK_ContractPrice.Where(y => y.ProductId == x.Id).FirstOrDefault().PriceRefId,
                Series = AppContext.OBK_Procunts_Series.Where(y => y.OBK_RS_ProductsId == x.Id).Select(y => new OBKContractSeriesViewModel
                {
                    Id = y.Id,
                    Series = y.Series,
                    CreateDate = y.SeriesStartdate,
                    ExpireDate = y.SeriesEndDate,
                    Part = y.SeriesParty,
                    UnitId = y.SeriesMeasureId,
                    UnitName = y.sr_measures.short_name
                }).ToList()
                ,
                MtParts = AppContext.OBK_MtPart.Where(y => y.ProductId == x.Id).Select(y => new OBKContractMtPartViewModel
                {
                    Id = y.Id,
                    PartNumber = y.PartNumber,
                    Model = y.Model,
                    Specification = y.Specification,
                    SpecificationKz = y.SpecificationKz,
                    Name = y.Name,
                    NameKz = y.NameKz,
                    ProducerName = y.ProducerName,
                    CountryName = y.CountryName,
                    ProducerNameKz = y.ProducerNameKz,
                    CountryNameKz = y.CountryNameKz
                }).ToList()
            }).ToList();
        }

        public OBKContractServiceViewModel SaveContractPrice(Guid contractId, OBKContractServiceViewModel service)
        {
            var s = new OBKContractServiceViewModel();
            if (service.Id != null)
            {
                OBK_ContractPrice serviceInfo = AppContext.OBK_ContractPrice.Where(x => x.Id == service.Id).FirstOrDefault();
                FillContractPrice(serviceInfo, service);
                AppContext.SaveChanges();
                s.Id = serviceInfo.Id;
            }
            else
            {
                OBK_ContractPrice serviceInfo = new OBK_ContractPrice();
                serviceInfo.Id = Guid.NewGuid();
                serviceInfo.ContractId = contractId;
                FillContractPrice(serviceInfo, service);
                AppContext.OBK_ContractPrice.Add(serviceInfo);
                AppContext.SaveChanges();
                s.Id = serviceInfo.Id;
            }
            return s;
        }

        public bool DeleteContractPrice(Guid contractId, Guid? serviceId)
        {
            var service = AppContext.OBK_ContractPrice.Where(x => x.Id == serviceId).FirstOrDefault();
            if (service != null)
            {
                AppContext.OBK_ContractPrice.Remove(service);
                AppContext.SaveChanges();
                return true;
            }
            return false;
        }

        private void FillContractPrice(OBK_ContractPrice serviceInfo, OBKContractServiceViewModel service)
        {
            serviceInfo.PriceRefId = service.ServiceId;
            serviceInfo.PriceWithoutTax = service.PriceWithoutTax;
            serviceInfo.Count = service.Count;
            serviceInfo.PriceWithoutTax = service.FinalCostWithoutTax;
            serviceInfo.PriceWithTax = service.FinalCostWithTax;
            serviceInfo.ProductId = service.ProductId;
        }

        private void SaveSeries(int productId, List<OBKContractSeriesViewModel> list)
        {
            if (list != null && list.Count > 0)
            {
                foreach (OBKContractSeriesViewModel item in list)
                {
                    OBK_Procunts_Series newSerie = new OBK_Procunts_Series();
                    newSerie.Series = item.Series;
                    newSerie.SeriesStartdate = item.CreateDate;
                    newSerie.SeriesEndDate = item.ExpireDate;
                    newSerie.SeriesParty = item.Part;
                    newSerie.SeriesMeasureId = item.UnitId;
                    newSerie.OBK_RS_ProductsId = productId;
                    AppContext.OBK_Procunts_Series.Add(newSerie);
                    AppContext.SaveChanges();
                }
            }
        }

        private void DeleteSeries(int productId)
        {
            AppContext.OBK_Procunts_Series.RemoveRange(AppContext.OBK_Procunts_Series.Where(x => x.OBK_RS_ProductsId == productId));
            AppContext.SaveChanges();
        }

        private void SaveMtParts(int productId, List<OBKContractMtPartViewModel> list)
        {
            if (list != null && list.Count > 0)
            {
                foreach (OBKContractMtPartViewModel item in list)
                {
                    OBK_MtPart mtPart = new OBK_MtPart();
                    mtPart.Id = Guid.NewGuid();
                    mtPart.ProductId = productId;
                    mtPart.PartNumber = item.PartNumber;
                    mtPart.Model = item.Model;
                    mtPart.Specification = item.Specification;
                    mtPart.SpecificationKz = item.SpecificationKz;
                    mtPart.Name = item.Name;
                    mtPart.NameKz = item.NameKz;
                    mtPart.ProducerName = item.ProducerName;
                    mtPart.CountryName = item.CountryName;
                    mtPart.ProducerNameKz = item.ProducerNameKz;
                    mtPart.CountryNameKz = item.CountryNameKz;

                    AppContext.OBK_MtPart.Add(mtPart);
                    AppContext.SaveChanges();
                }
            }
        }

        private void SaveProductService(int productId, Guid serviceId, Guid contractId, int count)
        {
            var price = AppContext.OBK_ContractPrice.Where(x => x.ContractId == contractId && x.ProductId == productId).FirstOrDefault();
            if (price != null)
            {
                var info = AppContext.OBK_Ref_PriceList.Where(x => x.Id == serviceId).FirstOrDefault();
                double tax = GetTax();
                double sum = info.Price * count;
                double res = sum + sum * (tax * 0.01);

                price.Count = count;
                price.PriceRefId = serviceId;
                price.PriceRefId = serviceId;
                price.PriceWithoutTax = sum;
                price.PriceWithTax = res;
                AppContext.SaveChanges();
            }
            else
            {
                var info = AppContext.OBK_Ref_PriceList.Where(x => x.Id == serviceId).FirstOrDefault();
                double tax = GetTax();
                double sum = info.Price * count;
                double res = sum + sum * (tax * 0.01);

                OBK_ContractPrice newPrice = new OBK_ContractPrice()
                {
                    Id = Guid.NewGuid(),
                    ContractId = contractId,
                    ProductId = productId,
                    Count = count,
                    PriceRefId = serviceId,
                    PriceWithoutTax = sum,
                    PriceWithTax = res,
                };

                AppContext.OBK_ContractPrice.Add(newPrice);
                AppContext.SaveChanges();
            }
        }

        private double GetTax()
        {
            var tax = AppContext.OBK_Ref_ValueAddedTax.Where(x => x.Year == DateTime.Now.Year).FirstOrDefault();
            if (tax != null)
            {
                return tax.Value;
            }
            throw new Exception("НДС неопределен на текущий год");
        }
        private void DeleteMtParts(int productId)
        {
            AppContext.OBK_MtPart.RemoveRange(AppContext.OBK_MtPart.Where(x => x.ProductId == productId));
            AppContext.SaveChanges();
        }

        private void FillProduct(OBK_RS_Products productInfo, OBKContractProductViewModel product)
        {
            productInfo.RegTypeId = product.RegTypeId;
            productInfo.DegreeRiskId = product.DegreeRiskId;
            productInfo.NameRu = product.NameRu;
            productInfo.NameKz = product.NameKz;
            productInfo.ProducerNameRu = product.ProducerNameRu;
            productInfo.ProducerNameKz = product.ProducerNameKz;
            productInfo.CountryNameRu = product.CountryNameRu;
            productInfo.CountryNameKZ = product.CountryNameKz;
            productInfo.Price = product.Price;
            productInfo.DrugFormBoxCount = product.DrugFormBoxCount;
            productInfo.DrugFormFullName = product.DrugFormFullName;
            productInfo.DrugFormFullNameKz = product.DrugFormFullNameKz;
            productInfo.RegisterId = product.RegisterId;
            productInfo.RegNumber = product.RegNumber;
            productInfo.RegNumberKz = product.RegNumberKz;
            productInfo.RegDate = product.RegDate;
            productInfo.ExpirationDate = product.ExpirationDate;
            productInfo.NdName = product.NdName;
            productInfo.NdNumber = product.NdNumber;
        }

        public List<OBKContractServiceViewModel> GetContractPrices(Guid contractId)
        {
            var list = AppContext.OBK_ContractPrice.Where(x => x.ContractId == contractId && x.ProductId != null).Select(x => new OBKContractServiceViewModel
            {
                Id = x.Id,
                ServiceName = x.OBK_Ref_PriceList.NameRu,
                ServiceId = x.PriceRefId,
                UnitOfMeasurementId = x.OBK_Ref_PriceList.UnitId,
                UnitOfMeasurementName = x.OBK_Ref_PriceList.Dictionary.Name,
                PriceWithoutTax = x.OBK_Ref_PriceList.Price,
                Count = x.Count,
                FinalCostWithoutTax = x.PriceWithoutTax,
                FinalCostWithTax = x.PriceWithTax,
                ProductId = x.ProductId,
                ProductName = x.OBK_RS_Products.NameRu
            }
            ).ToList();
            return list;
        }

        public List<OBKContractServiceViewModel> GetContractPricesAdditional(Guid contractId)
        {
            var list = AppContext.OBK_ContractPrice.Where(x => x.ContractId == contractId && x.ProductId == null).Select(x => new OBKContractServiceViewModel
            {
                Id = x.Id,
                ServiceName = x.OBK_Ref_PriceList.NameRu,
                ServiceId = x.PriceRefId,
                UnitOfMeasurementId = x.OBK_Ref_PriceList.UnitId,
                UnitOfMeasurementName = x.OBK_Ref_PriceList.Dictionary.Name,
                PriceWithoutTax = x.OBK_Ref_PriceList.Price,
                Count = x.Count,
                FinalCostWithoutTax = x.PriceWithoutTax,
                FinalCostWithTax = x.PriceWithTax,
                ProductId = x.ProductId,
                ProductName = x.OBK_RS_Products.NameRu
            }
            ).ToList();
            return list;
        }

        public double GetContractPricesSum(Guid contractId)
        {
            double sum = 0.0;
            List<double> list = AppContext.OBK_ContractPrice.Where(x => x.ContractId == contractId).Select(x => x.PriceWithTax).ToList();

            foreach (double item in list)
            {
                sum += item;
            }

            return sum;
        }

        public OBK_Declarant FindOrganization(string bin)
        {
            var declarant = AppContext.OBK_Declarant.FirstOrDefault(x => x.Bin == bin && x.IsConfirmed && x.IsResident);
            return declarant;
        }

        public OBK_Declarant ContractDeclarantSave(Guid guid, OBKDeclarantViewModel declarantViewModel, out bool declarantAlreadyExist)
        {
            declarantAlreadyExist = false;

            if (declarantViewModel.Id != null)
            {
                OBK_Declarant declarant = AppContext.OBK_Declarant.Where(x => x.Id == declarantViewModel.Id).FirstOrDefault();
                if (declarant != null && !declarant.IsConfirmed)
                {
                    if (declarantViewModel.IsResident)
                    {
                        declarantAlreadyExist = DeclarantExist(declarantViewModel.Id, declarantViewModel.Bin);
                    }
                    else
                    {
                        declarantAlreadyExist = DeclarantExist(declarantViewModel.Id, declarantViewModel.NameRu, declarantViewModel.CountryId);
                    }

                    if (!declarantAlreadyExist)
                    {
                        declarant.OrganizationFormId = declarantViewModel.OrganizationFormId;
                        declarant.NameKz = declarantViewModel.NameKz;
                        declarant.NameRu = declarantViewModel.NameRu;
                        declarant.NameEn = declarantViewModel.NameEn;
                        declarant.CountryId = declarantViewModel.CountryId;
                        declarant.IsResident = declarantViewModel.IsResident;
                        if (declarant.IsResident)
                        {
                            declarant.Bin = declarantViewModel.Bin;
                        }
                        else
                        {
                            declarant.Bin = null;
                        }

                        AppContext.SaveChanges();
                        return declarant;
                    }
                }
            }
            else
            {
                if (declarantViewModel.IsResident)
                {
                    declarantAlreadyExist = DeclarantExist(declarantViewModel.Id, declarantViewModel.Bin);
                }
                else
                {
                    declarantAlreadyExist = DeclarantExist(declarantViewModel.Id, declarantViewModel.NameRu, declarantViewModel.CountryId);
                }

                if (!declarantAlreadyExist)
                {
                    var contract = AppContext.OBK_Contract.Where(x => x.Id == guid).FirstOrDefault();
                    if (contract != null)
                    {
                        if (contract.DeclarantId != null)
                        {
                            var existingDeclarant = AppContext.OBK_Declarant.Where(x => x.Id == contract.DeclarantId).FirstOrDefault();
                            if (!existingDeclarant.IsConfirmed)
                            {
                                AppContext.OBK_Declarant.Remove(existingDeclarant);
                                contract.DeclarantId = null;
                                AppContext.SaveChanges();
                            }
                        }

                        OBK_Declarant declarant = new OBK_Declarant();
                        declarant.Id = Guid.NewGuid();
                        declarant.IsConfirmed = false;
                        declarant.OrganizationFormId = declarantViewModel.OrganizationFormId;
                        declarant.NameKz = declarantViewModel.NameKz;
                        declarant.NameRu = declarantViewModel.NameRu;
                        declarant.NameEn = declarantViewModel.NameEn;
                        declarant.CountryId = declarantViewModel.CountryId;
                        declarant.IsResident = declarantViewModel.IsResident;
                        if (declarant.IsResident)
                        {
                            declarant.Bin = declarantViewModel.Bin;
                        }
                        else
                        {
                            declarant.Bin = null;
                        }
                        AppContext.OBK_Declarant.Add(declarant);
                        AppContext.SaveChanges();
                        contract.DeclarantId = declarant.Id;
                        var contact = AppContext.OBK_DeclarantContact.Where(x => x.Id == contract.DeclarantContactId).FirstOrDefault();
                        if (contact != null)
                        {
                            contact.DeclarantId = contract.DeclarantId;
                        }
                        AppContext.SaveChanges();
                        return declarant;
                    }
                }
            }
            return null;
        }

        private bool DeclarantExist(Guid? id, string bin)
        {
            var exist = false;
            if (!string.IsNullOrEmpty(bin))
            {
                var declrant = id != null ?
                    AppContext.OBK_Declarant.Where(x => x.IsConfirmed && x.Bin == bin && x.IsResident == true && x.Id != id).FirstOrDefault()
                    :
                    AppContext.OBK_Declarant.Where(x => x.IsConfirmed && x.Bin == bin && x.IsResident == true).FirstOrDefault();
                if (declrant != null)
                {
                    exist = true;
                }
            }
            return exist;
        }

        private bool DeclarantExist(Guid? id, string nameRu, Guid? countryId)
        {
            var exist = false;
            var declarant = id != null ?
                AppContext.OBK_Declarant.Where(x => x.IsConfirmed && x.NameRu == nameRu && x.CountryId == countryId && x.IsResident == false && x.Id != id).FirstOrDefault()
                :
                AppContext.OBK_Declarant.Where(x => x.IsConfirmed && x.NameRu == nameRu && x.CountryId == countryId && x.IsResident == false).FirstOrDefault();
            if (declarant != null)
            {
                exist = true;
            }
            return exist;
        }

        public bool SaveDeclarant(Guid contractId, Guid declarantId)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            if (contract != null)
            {
                if (contract.DeclarantId != null)
                {
                    var existingDeclarant = AppContext.OBK_Declarant.Where(x => x.Id == contract.DeclarantId).FirstOrDefault();
                    if (!existingDeclarant.IsConfirmed)
                    {
                        var contacts = AppContext.OBK_DeclarantContact.Where(x => x.DeclarantId == contract.DeclarantId);
                        foreach (var contact in contacts)
                        {
                            contact.DeclarantId = null;
                        }

                        AppContext.OBK_Declarant.Remove(existingDeclarant);
                        contract.DeclarantId = null;
                        AppContext.SaveChanges();
                    }
                }

                contract.DeclarantId = declarantId;

                if (contract.DeclarantContactId != null)
                {
                    var contact = AppContext.OBK_DeclarantContact.Where(x => x.Id == contract.DeclarantContactId).FirstOrDefault();
                    if (contact != null)
                    {
                        contact.DeclarantId = contract.DeclarantId;
                    }
                }

                AppContext.SaveChanges();
            }
            return true;
        }

        public bool DeleteDeclarant(Guid contractId)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            if (contract != null)
            {
                bool declarantRemoved = false;
                if (contract.DeclarantId != null)
                {
                    var existingDeclarant = AppContext.OBK_Declarant.Where(x => x.Id == contract.DeclarantId).FirstOrDefault();
                    if (!existingDeclarant.IsConfirmed)
                    {
                        var contacts = AppContext.OBK_DeclarantContact.Where(x => x.DeclarantId == contract.DeclarantId);
                        foreach (var contact in contacts)
                        {
                            contact.DeclarantId = null;
                        }

                        AppContext.OBK_Declarant.Remove(existingDeclarant);
                        contract.DeclarantId = null;
                        AppContext.SaveChanges();
                        declarantRemoved = true;
                    }
                }

                if (!declarantRemoved)
                {
                    contract.DeclarantId = null;
                    var contacts = AppContext.OBK_DeclarantContact.Where(x => x.DeclarantId == contract.DeclarantId);
                    foreach (var contact in contacts)
                    {
                        contact.DeclarantId = null;
                    }

                    AppContext.SaveChanges();
                }
            }
            return true;
        }

        public object GetNamesNonResidents(Guid? countryId)
        {
            var noData = new { Id = Guid.Empty, Name = "Нет данных", NameKz = "Нет данных" };
            if (countryId != null)
            {
                var obkDeclarants = AppContext.OBK_Declarant.Where(x => x.CountryId == countryId && x.IsConfirmed == true && x.IsResident == false).OrderBy(x => x.NameRu).Select(x => new { x.Id, Name = x.NameRu, x.NameKz }).ToList();
                var list = new[] { noData }.ToList().Concat(obkDeclarants);
                return list;
            }
            else
            {
                var list = new[] { noData }.ToList();
                return list;
            }
        }

        public bool ClearContactData(Guid contractId)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            if (contract != null)
            {
                if (contract.DeclarantContactId != null)
                {
                    var declarantContact = AppContext.OBK_DeclarantContact.Where(x => x.Id == contract.DeclarantContactId).FirstOrDefault();
                    if (declarantContact != null)
                    {
                        declarantContact.AddressLegalRu = null;
                        declarantContact.AddressLegalKz = null;
                        declarantContact.AddressFact = null;
                        declarantContact.Phone = null;
                        declarantContact.Email = null;
                        declarantContact.BossLastName = null;
                        declarantContact.BossFirstName = null;
                        declarantContact.BossMiddleName = null;
                        declarantContact.BossPosition = null;
                        declarantContact.BossPositionKz = null;
                        declarantContact.BossDocType = null;
                        declarantContact.IsHasBossDocNumber = false;
                        declarantContact.BossDocNumber = null;
                        declarantContact.BossDocCreatedDate = null;
                        declarantContact.BossDocEndDate = null;
                        declarantContact.BossDocUnlimited = false;
                        declarantContact.SignLastName = null;
                        declarantContact.SignFirstName = null;
                        declarantContact.SignMiddleName = null;
                        declarantContact.SignPosition = null;
                        declarantContact.SignPositionKz = null;
                        declarantContact.SignDocType = null;
                        declarantContact.IsHasSignDocNumber = false;
                        declarantContact.SignDocNumber = null;
                        declarantContact.SignDocCreatedDate = null;
                        declarantContact.SignDocEndDate = null;
                        declarantContact.SignDocUnlimited = false;
                        declarantContact.BankIik = null;
                        declarantContact.BankBik = null;
                        declarantContact.CurrencyId = null;
                        declarantContact.BankNameRu = null;
                        declarantContact.BankNameKz = null;
                        declarantContact.DeclarantId = null;
                        AppContext.SaveChanges();
                    }
                }
            }
            return true;
        }

        public OBKDeclarantViewModel GetDeclarant(Guid contractId)
        {
            OBKDeclarantViewModel declarantViewModel = null;
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            if (contract != null && contract.DeclarantId != null)
            {
                var declarant = AppContext.OBK_Declarant.Where(x => x.Id == contract.DeclarantId).FirstOrDefault();
                if (declarant != null)
                {
                    declarantViewModel = new OBKDeclarantViewModel()
                    {
                        Id = declarant.Id,
                        OrganizationFormId = declarant.OrganizationFormId,
                        NameKz = declarant.NameKz,
                        NameRu = declarant.NameRu,
                        NameEn = declarant.NameEn,
                        CountryId = declarant.CountryId,
                        Bin = declarant.Bin,
                        IsResident = declarant.IsResident,
                        IsConfirmed = declarant.IsConfirmed
                    };
                }
            }
            return declarantViewModel;
        }

        public List<OBKDrugFormViewModel> GetDrugForms(int productId)
        {
            var drugForms = AppContext.sr_drug_forms.Where(x => x.register_id == productId).Select(y => new OBKDrugFormViewModel
            {
                Id = y.id,
                RegisterId = y.register_id,
                BoxCount = y.box_count,
                FullName = y.full_name,
                FullNameKz = y.full_name_kz
            }).ToList();
            return drugForms;
        }

        public List<OBKMtPartViewModel> GetMtParts(int productId)
        {
            var meParts = AppContext.sr_register_mt_parts.Where(x => x.register_id == productId).Select(y => new OBKMtPartViewModel
            {
                Id = y.id,
                RegisterId = y.register_id,
                Name = y.name,
                NameKz = y.name_kz,
                PartNumber = y.part_number,
                Model = y.model,
                Specification = y.specification,
                SpecificationKz = y.specification_kz,
                ProducerName = y.producer_name,
                CountryName = y.country_name,
                ProducerNameKz = y.producer_name_kz,
                CountryNameKz = y.country_name_kz,

            }).ToList();
            return meParts;
        }

        public IQueryable<OBK_ContractRegisterView> GetContracts()
        {
            var emp = UserHelper.GetCurrentEmployee();

            var list = AppContext.OBK_ContractRegisterView.Where(x => x.ExecutorId == emp.Id).AsQueryable();

            return list;
        }

        public int SendContractInProcessing(Guid contractId, bool addDigitalSign)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();

            if (contract.Status == CodeConstManager.STATUS_OBK_DRAFT_ID)
            {
                contract.SendDate = DateTime.Now;
                contract.Status = CodeConstManager.STATUS_OBK_INPROCESSING;

                var stageStatus = GetStageStatusByCode(OBK_Ref_StageStatus.New);

                var obkContractStageCoz = new OBK_ContractStage()
                {
                    Id = Guid.NewGuid(),
                    ContractId = contractId,
                    StageId = CodeConstManager.STAGE_OBK_COZ,
                    StageStatusId = stageStatus.Id,
                    ParentStageId = null,
                    ResultId = null
                };

                // Руководитель ЦОЗ
                Guid bossCozGuid = new Guid("3100E850-F7D8-48A4-A5AC-4BF5D50D98D2");
                var stageExecutorCoz = new OBK_ContractStageExecutors()
                {
                    OBK_ContractStage = obkContractStageCoz,
                    ExecutorId = bossCozGuid,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
                };

                AppContext.OBK_ContractStage.Add(obkContractStageCoz);

                AppContext.OBK_ContractStageExecutors.Add(stageExecutorCoz);

                SendNotificationToCozBoss(contract, bossCozGuid);

                if (addDigitalSign)
                {
                    OBK_ContractStageExecutors stageSignerCoz = new OBK_ContractStageExecutors()
                    {
                        OBK_ContractStage = obkContractStageCoz,
                        ExecutorId = contract.Signer.Value,
                        ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER
                    };

                    AppContext.OBK_ContractStageExecutors.Add(stageSignerCoz);
                }

                AppContext.SaveChanges();

                var obkContractStageUOBK = new OBK_ContractStage()
                {
                    Id = Guid.NewGuid(),
                    ContractId = contractId,
                    StageId = CodeConstManager.STAGE_OBK_UOBK,
                    StageStatusId = stageStatus.Id,
                    ParentStageId = obkContractStageCoz.Id,
                    ResultId = null
                };

                // Руководитель УОБК
                Guid bossUobkGuid = new Guid("14D1A1F0-9501-4232-9C29-E9C394D88784");
                var stageExecutorUOBK = new OBK_ContractStageExecutors()
                {
                    OBK_ContractStage = obkContractStageUOBK,
                    ExecutorId = bossUobkGuid,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
                };

                AppContext.OBK_ContractStage.Add(obkContractStageUOBK);

                AppContext.OBK_ContractStageExecutors.Add(stageExecutorUOBK);

                SendNotificationToUobkBoss(contract, bossUobkGuid);

                if (!addDigitalSign)
                {
                    AppContext.OBK_ContractSignedDatas.RemoveRange(AppContext.OBK_ContractSignedDatas.Where(x => x.ContractId == contractId));
                }

                AddHistorySentByApplicant(contractId);

                AddExtHistoryInprocessing(contractId);

                AppContext.SaveChanges();
            }
            else if (contract.Status == CodeConstManager.STATUS_OBK_ONCORRECTION)
            {
                contract.SendDate = DateTime.Now;
                contract.Status = CodeConstManager.STATUS_OBK_INPROCESSING;

                var stages = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId).ToList();
                var stageStatusInWorkId = GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;
                foreach (var stage in stages)
                {
                    if (stage.StageId == CodeConstManager.STAGE_OBK_COZ || stage.StageId == CodeConstManager.STAGE_OBK_UOBK)
                    {
                        stage.ResultId = null;
                    }
                    stage.StageStatusId = stageStatusInWorkId;
                }

                if (!addDigitalSign)
                {
                    var stageObk = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId && x.StageId == CodeConstManager.STAGE_OBK_COZ).FirstOrDefault();
                    if (stageObk != null)
                    {
                        AppContext.OBK_ContractStageExecutors.RemoveRange(AppContext.OBK_ContractStageExecutors.Where(x => x.ContractStageId == stageObk.Id && x.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER));
                    }

                    AppContext.OBK_ContractSignedDatas.RemoveRange(AppContext.OBK_ContractSignedDatas.Where(x => x.ContractId == contractId));
                }
                else
                {
                    var stageCoz = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId && x.StageId == CodeConstManager.STAGE_OBK_COZ).FirstOrDefault();
                    if (stageCoz != null)
                    {
                        OBK_ContractStageExecutors stageSignerCoz = AppContext.OBK_ContractStageExecutors.Where(x => x.ContractStageId == stageCoz.Id && x.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER).FirstOrDefault();
                        if (stageSignerCoz != null)
                        {
                            stageSignerCoz.ExecutorId = contract.Signer.Value;
                        }
                        else
                        {
                            stageSignerCoz = new OBK_ContractStageExecutors()
                            {
                                OBK_ContractStage = stageCoz,
                                ExecutorId = contract.Signer.Value,
                                ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER
                            };

                            AppContext.OBK_ContractStageExecutors.Add(stageSignerCoz);
                        }
                    }
                }

                AddHistorySentByApplicant(contractId);

                AddExtHistoryInprocessing(contractId);

                AppContext.SaveChanges();

                SendNotificationToExecutorsFixedErrors(contract);
            }

            return contract.Status;
        }

        public int SendContractInProcessing(Guid contractId)
        {
            return SendContractInProcessing(contractId, false);
        }

        public OBK_ContractCom GetComments(Guid modelId, string idControl)
        {
            return
                AppContext.OBK_ContractCom.FirstOrDefault(
                    e => e.ControlId == idControl && modelId == e.ContractId);
        }

        public List<OBK_ContractCom> GetCommentsOfContract(Guid contractId)
        {
            return AppContext.OBK_ContractCom.Where(x => x.ContractId == contractId).ToList();
        }

        public void SaveComment(string modelId, string idControl, bool isError, string comment, string fieldValue,
            string userId, string fieldDisplay)
        {
            var entityId = new Guid(modelId);
            var model =
                AppContext.OBK_ContractCom.FirstOrDefault(
                    e => e.ControlId == idControl && e.ContractId.Equals(entityId)) ??
                new OBK_ContractCom
                {
                    DateCreate = DateTime.Now,
                    ContractId = entityId,
                    ControlId = idControl,
                };

            model.IsError = isError;
            model.OBK_ContractComRecord.Add(new OBK_ContractComRecord
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Note = comment,
                UserId = new Guid(userId),
                OBK_ContractCom = model,
                ValueField = fieldValue,
                DisplayField = fieldDisplay
            });
            if (model.Id == null || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                AppContext.OBK_ContractCom.Add(model);
            }
            AppContext.SaveChanges();
        }

        public OBK_ContractPriceCom GetCommentsPrice(Guid contractPriceId)
        {
            return
                AppContext.OBK_ContractPriceCom.FirstOrDefault(
                    e => contractPriceId == e.ContractPriceId);
        }

        public void SaveCommentPrice(string contractPriceId, bool isError, string comment, string fieldValue, string userId, string fieldDisplay)
        {
            var entityId = new Guid(contractPriceId);
            var model =
                AppContext.OBK_ContractPriceCom.FirstOrDefault(
                    e => e.ContractPriceId.Equals(entityId)) ??
                new OBK_ContractPriceCom
                {
                    DateCreate = DateTime.Now,
                    ContractPriceId = entityId
                };

            model.IsError = isError;
            model.OBK_ContractPriceComRecord.Add(new OBK_ContractPriceComRecord
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Note = comment,
                UserId = new Guid(userId),
                OBK_ContractPriceCom = model,
                ValueField = fieldValue,
                DisplayField = fieldDisplay
            });
            if (model.Id == null || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                AppContext.OBK_ContractPriceCom.Add(model);
            }
            AppContext.SaveChanges();
        }

        public OBK_RS_ProductsCom GetCommentsProduct(int productId)
        {
            return
                AppContext.OBK_RS_ProductsCom.FirstOrDefault(
                    e => productId == e.ProductId);
        }

        public void SaveCommentProduct(string productId, bool isError, string comment, string fieldValue, string userId, string fieldDisplay)
        {
            var entityId = int.Parse(productId);
            var model =
                AppContext.OBK_RS_ProductsCom.FirstOrDefault(
                    e => e.ProductId.Equals(entityId)) ??
                new OBK_RS_ProductsCom
                {
                    DateCreate = DateTime.Now,
                    ProductId = entityId
                };

            model.IsError = isError;
            model.OBK_RS_ProductsComRecord.Add(new OBK_RS_ProductsComRecord
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Note = comment,
                UserId = new Guid(userId),
                OBK_RS_ProductsCom = model,
                ValueField = fieldValue,
                DisplayField = fieldDisplay
            });
            if (model.Id == null || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                AppContext.OBK_RS_ProductsCom.Add(model);
            }
            AppContext.SaveChanges();
        }

        public OBK_Products_SeriesCom GetCommentsProductsSerie(int productSerieId)
        {
            return
               AppContext.OBK_Products_SeriesCom.FirstOrDefault(
                   e => productSerieId == e.ProductSerieId);
        }

        public void SaveCommentProductsSerie(string productSerieId, bool isError, string comment, string fieldValue, string userId, string fieldDisplay)
        {
            var entityId = int.Parse(productSerieId);
            var model =
                AppContext.OBK_Products_SeriesCom.FirstOrDefault(
                    e => e.ProductSerieId.Equals(entityId)) ??
                new OBK_Products_SeriesCom
                {
                    DateCreate = DateTime.Now,
                    ProductSerieId = entityId
                };

            model.IsError = isError;
            model.OBK_Products_SeriesComRecord.Add(new OBK_Products_SeriesComRecord
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Note = comment,
                UserId = new Guid(userId),
                OBK_Products_SeriesCom = model,
                ValueField = fieldValue,
                DisplayField = fieldDisplay
            });
            if (model.Id == null || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                AppContext.OBK_Products_SeriesCom.Add(model);
            }
            AppContext.SaveChanges();
        }

        public OBK_Ref_StageStatus GetStageStatusByCode(string code)
        {
            return AppContext.OBK_Ref_StageStatus.AsNoTracking().FirstOrDefault(e => e.Code == code);
        }

        public void SendToWork(Guid stageId, Guid executorId)
        {
            var stage = AppContext.OBK_ContractStage.Where(x => x.Id == stageId).FirstOrDefault();
            var executor = AppContext.Employees.Where(x => x.Id == executorId).FirstOrDefault();

            var stageExecutor = new OBK_ContractStageExecutors()
            {
                OBK_ContractStage = stage,
                ExecutorId = executorId,
                ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
            };

            AppContext.OBK_ContractStageExecutors.Add(stageExecutor);

            stage.StageStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;

            var contract = AppContext.OBK_Contract.Where(x => x.Id == stage.ContractId).FirstOrDefault();

            // Записывать историю если статус В Работу проставляется впервые, когда его назначают на первого исполнителя
            if (contract.Status != CodeConstManager.STATUS_OBK_WORK)
            {
                AddExtHistoryWork(contract.Id);
            }

            contract.Status = CodeConstManager.STATUS_OBK_WORK;

            AddHistorySentToWork(contract.Id);

            AppContext.SaveChanges();

            SendNotificationToExecutorWork(executorId, contract);
        }

        public string GetContractTemplatePath(Guid contractId)
        {
            string templateName = null;
            var contract = AppContext.OBK_Contract.FirstOrDefault(e => e.Id == contractId);
            if (contract == null)
                return null;
            switch (contract.Type)
            {
                case 1:
                    templateName = "ObkContractSerial.mrt";
                    break;
                case 2:
                    templateName = "ObkContractParty.mrt";
                    break;
                case 3:
                    templateName = "ObkContractDeclaration.mrt";
                    break;
            }
            return System.Web.HttpContext.Current.Server.MapPath("~/Reports/Mrts/OBK/" + templateName);
        }

        public string GetPriceCount(Guid contractId)
        {
            var count = AppContext.OBK_ContractPrice.Where(e => e.ContractId == contractId).ToList();
            return count.Sum(e => e.PriceWithTax).ToString();
        }

        public IQueryable<object> GetSigners()
        {
            string[] signerCodes = { "ncels_deputyceo", "ncels_ceo" };
            var items = AppContext.Employees.Where(e => signerCodes.Contains(e.Position.Code)).Select(e => new
            {
                e.Id,
                Name = e.Position.ShortName + " " + e.ShortName
            });
            return items;
        }

        public IQueryable<object> GetExpertOrganizations()
        {
            var items = AppContext.Units.Where(x => x.Code == "00").Select(x => new
            {
                Id = x.Id,
                Name = x.ShortName
            });
            return items;
        }

        public bool MeetsRequirements(Guid contractId)
        {
            var employee = UserHelper.GetCurrentEmployee();
            var employeeId = employee.Id;
            var contractViews = AppContext.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId).ToList();
            foreach (var contractView in contractViews)
            {
                var stage = AppContext.OBK_ContractStage.Where(x => x.Id == contractView.ContractStageId).FirstOrDefault();
                stage.ResultId = CodeConstManager.OBK_RESULT_ID_MEETS_REQUIREMENTS;
                if (stage.StageId == CodeConstManager.STAGE_OBK_UOBK)
                {
                    CreateStageDef(contractId, stage);
                }

                AppContext.SaveChanges();
            }
            AddHistoryMeetsRequirements(contractId);
            AppContext.SaveChanges();

            SendNotificationToCozExecutor(contractViews);

            return true;
        }

        public bool DoestNotMeetRequirements(Guid contractId)
        {
            var employee = UserHelper.GetCurrentEmployee();
            var employeeId = employee.Id;
            var contractViews = AppContext.OBK_ContractRegisterView.Where(x => x.ContractId == contractId && x.ExecutorId == employeeId).ToList();
            foreach (var contractView in contractViews)
            {
                var stage = AppContext.OBK_ContractStage.Where(x => x.Id == contractView.ContractStageId).FirstOrDefault();
                stage.ResultId = CodeConstManager.OBK_RESULT_ID_DOES_NOT_MEET_REQUIREMENTS;
                if (stage.StageId == CodeConstManager.STAGE_OBK_UOBK)
                {
                    CreateStageDef(contractId, stage);
                }

                AppContext.SaveChanges();
            }
            AddHistoryDoesNotMeetRequirements(contractId);
            AppContext.SaveChanges();

            SendNotificationToCozExecutor(contractViews);

            return true;
        }

        private void CreateStageDef(Guid contractId, OBK_ContractStage parentStage)
        {
            var stageStatusInWork = GetStageStatusByCode(OBK_Ref_StageStatus.InWork);

            var stageChild = AppContext.OBK_ContractStage.Where(x => x.ParentStageId == parentStage.Id).FirstOrDefault();

            if (stageChild == null)
            {
                OBK_ContractStage stageDef = new OBK_ContractStage()
                {
                    Id = Guid.NewGuid(),
                    ContractId = contractId,
                    StageId = CodeConstManager.STAGE_OBK_DEF,
                    StageStatusId = stageStatusInWork.Id,
                    ParentStageId = parentStage.Id,
                    ResultId = null
                };

                // Руководитель ДЭФ
                var bossDefGuid = new Guid("C9746027-F617-4791-8EEE-1CD80F2EDD5B");
                var stageExecutor = new OBK_ContractStageExecutors()
                {
                    OBK_ContractStage = stageDef,
                    ExecutorId = bossDefGuid,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                };

                AppContext.OBK_ContractStage.Add(stageDef);

                AppContext.OBK_ContractStageExecutors.Add(stageExecutor);

                SendNotificationToDefExecutorNew(contractId, bossDefGuid);
            }
            else
            {
                stageChild.StageStatusId = stageStatusInWork.Id;
                AppContext.OBK_ContractStageExecutors.RemoveRange(AppContext.OBK_ContractStageExecutors.Where(x => x.ContractStageId == stageChild.Id));
                stageChild.ResultId = null;

                // Руководитель ДЭФ
                var bossDefGuid = new Guid("C9746027-F617-4791-8EEE-1CD80F2EDD5B");
                var stageExecutor = new OBK_ContractStageExecutors()
                {
                    OBK_ContractStage = stageChild,
                    ExecutorId = bossDefGuid,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                };

                AppContext.OBK_ContractStageExecutors.Add(stageExecutor);

                SendNotificationToDefExecutorFixedErrors(contractId, bossDefGuid);
            }
        }

        public bool ReturnToApplicant(Guid contractId)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            contract.Status = CodeConstManager.STATUS_OBK_ONCORRECTION;

            var stages = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId).ToList();
            var stageStatus = GetStageStatusByCode(OBK_Ref_StageStatus.OnCorrection);
            foreach (var dtage in stages)
            {
                dtage.StageStatusId = stageStatus.Id;
            }

            AddHistoryReturned(contractId);

            AddExtHistoryOncorrection(contractId);

            AppContext.SaveChanges();

            SendNotificationToApplicantReturned(contract);

            return true;
        }

        public bool SendToBossForApproval(Guid contractId)
        {
            var stages = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId).ToList();
            var stageStatus = GetStageStatusByCode(OBK_Ref_StageStatus.OnAgreement);
            foreach (var dtage in stages)
            {
                dtage.StageStatusId = stageStatus.Id;
            }

            AddHistorySentToApproval(contractId);

            AppContext.SaveChanges();

            return true;
        }

        public bool ApproveContract(Guid contractId)
        {
            var digitalSign = AppContext.OBK_ContractSignedDatas.Where(x => x.ContractId == contractId).FirstOrDefault();
            var stages = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId).ToList();

            OBK_Ref_StageStatus stageStatus;
            if (digitalSign == null)
            {
                stageStatus = GetStageStatusByCode(OBK_Ref_StageStatus.RequiresRegistration);
            }
            else
            {
                stageStatus = GetStageStatusByCode(OBK_Ref_StageStatus.RequiresSigning);
            }
            foreach (var dtage in stages)
            {
                dtage.StageStatusId = stageStatus.Id;
            }
            AddHistoryApproved(contractId);
            AppContext.SaveChanges();
            SendNotificationToCozExecutorApproved(contractId);
            if (digitalSign == null)
            {
                SendNotificationToApplicantApproved(contractId);
            }
            else
            {
                SendNotificationToSignerApproved(contractId);
            }
            return true;
        }

        public bool RefuseApprovement(Guid contractId, string reason)
        {
            var stages = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId).ToList();
            var stageStatus = GetStageStatusByCode(OBK_Ref_StageStatus.NotAgreed);
            foreach (var dtage in stages)
            {
                dtage.StageStatusId = stageStatus.Id;
            }

            AddHistoryRefused(contractId, reason);

            AppContext.SaveChanges();

            return true;
        }

        public OBK_Ref_ContractHistoryStatus GetContractHistoryStatusByCode(string code)
        {
            return AppContext.OBK_Ref_ContractHistoryStatus.Where(x => x.Code == code).FirstOrDefault();
        }

        private string GetParentUnitName(Employee employee)
        {
            string unitName = null;
            if (employee.Units != null && employee.Units.Count > 0)
            {
                foreach (var unit in employee.Units)
                {
                    if (unit.Parent != null)
                    {
                        unitName = unit.Parent.ShortName;
                    }
                    break;
                }
            }
            return unitName;
        }

        public string GetRefuseReason(Guid contractId)
        {
            OBK_Ref_ContractHistoryStatus status = GetContractHistoryStatusByCode(OBK_Ref_ContractHistoryStatus.Refused);
            var history = AppContext.OBK_ContractHistory.Where(x => x.ContractId == contractId && x.StatusId == status.Id).OrderByDescending(x => x.Created).FirstOrDefault();
            if (history != null)
            {
                return history.RefuseReason;
            }
            return null;
        }

        public string RegisterContract(Guid contractId)
        {
            string number = GetLastNumberOfContract();
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            var digitalSign = AppContext.OBK_ContractSignedDatas.Where(x => x.ContractId == contractId).FirstOrDefault();

            contract.Number = number;
            contract.StartDate = DateTime.Now;
            AppContext.SaveChanges();

            if (digitalSign != null)
            {
                contract.Status = CodeConstManager.STATUS_OBK_INVOCE_GENERATING;

                var stages = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId).ToList();
                var stageStatus = GetStageStatusByCode(OBK_Ref_StageStatus.Active);
                foreach (var dtage in stages)
                {
                    dtage.StageStatusId = stageStatus.Id;
                }

                // Формирование вложения
                Stream stream = GetContractTemplatePdf(contractId);
                SaveAttach(stream, contractId);

                AddExtHistoryActive(contractId);
            }
            AddHistoryRegistered(contractId);
            AppContext.SaveChanges();
            SendNotificationToApplicantRegistered(contract);
            SendNotificationToAccountantRegistered(contract);
            return contract.Number;
        }

        private void SaveAttach(Stream stream, Guid contractId)
        {
            string code = "";
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            if (contract.OBK_Declarant.IsResident)
            {
                var codeId = AppContext.Dictionaries.Where(x => x.Type == CodeConstManager.ATTACH_CONTRACT_FILE_RESIDENT && x.Code == "0").Select(x => x.Id).FirstOrDefault();
                if (codeId == Guid.Empty)
                {
                    throw new Exception("CodeId cannot be found");
                }
                code = codeId.ToString();
            }
            else
            {
                var codeId = AppContext.Dictionaries.Where(x => x.Type == CodeConstManager.ATTACH_CONTRACT_FILE_NON_RESIDENT && x.Code == "0").Select(x => x.Id).FirstOrDefault();
                if (codeId == Guid.Empty)
                {
                    throw new Exception("CodeId cannot be found");
                }
                code = codeId.ToString();
            }
            string path = contractId.ToString();
            bool saveMetadata = true;
            string originField = null;

            var list = FileHelper.GetAttachListByDoc(AppContext, path, code);
            foreach (var item in list)
            {
                Type t = item.GetType();
                PropertyInfo p = t.GetProperty("AttachName");
                object attachName = p.GetValue(item, null);
                FileHelper.DeleteAttach(path, code, attachName.ToString());
            }
            string fileName = "Договор.pdf";
            SaveAttach(code, path, stream, fileName, saveMetadata, originField, AppContext);
        }

        public static object SaveAttach(string code, string path, Stream stream, string fileName, bool saveMetadata, string originFileId, ncelsEntities db, string lang = "", string comment = "", int? numOfPages = null)
        {
            if (!saveMetadata && string.IsNullOrEmpty(code) && string.IsNullOrEmpty(path))
                throw new ArgumentException("Невозможно сохранить файл без привязки к объекту, без категории и без метаданных");
            if (saveMetadata && ((string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(path)) || (!string.IsNullOrEmpty(code) && string.IsNullOrEmpty(path))))
                throw new ArgumentException("Невозможно сохранить файл с метаданными и привязкой к объекту или категории");

            Guid fileId = Guid.NewGuid();
            string actualFileName = fileName;
            string savedFileName = saveMetadata
                ? string.Format("{0}{1}", fileId, Path.GetExtension(actualFileName))
                : actualFileName;
            try
            {
                var ownId = UserHelper.GetCurrentEmployee().Id;
                int? currentStageId = null;
                var pathGuid = Guid.Parse(path);
                var stage =
                    db.EXP_ExpertiseStage.Where((x => x.Executors.Select(y => y.Id).Contains(ownId) && !x.IsHistory && x.EXP_DIC_StageStatus.Code == "inWork" && x.DeclarationId == pathGuid));
                if (stage.Any())
                {
                    currentStageId = stage.First().StageId;
                }
                string root = ConfigurationManager.AppSettings["AttachPath"];
                string fullName = Path.Combine(root, FileHelper.Root, path ?? "", code ?? "", savedFileName);
                DirectoryInfo info = new DirectoryInfo(Path.Combine(root, FileHelper.Root, path ?? "", code ?? ""));
                if (!info.Exists)
                    info.Create();
                using (var fileStream = File.Create(fullName))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
                if (saveMetadata)
                {
                    var fileLink = new FileLink()
                    {
                        Id = fileId,
                        CreateDate = DateTime.Now,
                        CategoryId = !string.IsNullOrEmpty(code) ? (Guid?)Guid.Parse(code) : null,
                        DocumentId = !string.IsNullOrEmpty(path) ? (Guid?)Guid.Parse(path) : null,
                        FileName = actualFileName,
                        OwnerId = ownId,
                        Version = 1,
                        Comment = comment,
                        Language = lang,
                        PageNumbers = numOfPages,
                        StageId = currentStageId

                    };
                    db.FileLinks.Add(fileLink);
                    db.SaveChanges();
                }
                if (saveMetadata && (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(path)))
                    return new
                    {
                        Id = string.Format("fileId={0}", string.Format("{0}{1}", fileId, Path.GetExtension(savedFileName))),
                        MetadataId = fileId,
                        Version = 1
                    };
                if (saveMetadata && !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(path))
                    return new
                    {
                        Id = string.Format("id={0}&path={1}&fileId={2}", code, path, string.Format("{0}{1}", fileId, Path.GetExtension(savedFileName))),
                        MetadataId = fileId,
                        Version = 1
                    };
                return new
                {
                    Id = string.Format("id={0}&path={1}&name={2}", code, path, actualFileName),
                    MetadataId = (string)null,
                    Version = (string)null
                };
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Ок";
        }

        private string GetLastNumberOfContract()
        {
            string number = "1";
            var numbers = AppContext.OBK_Contract.Select(x => x.Number).ToList();
            int temp;
            int contractNumber = numbers.Select(n => int.TryParse(n, out temp) ? temp : 0).Max();
            contractNumber++;
            number = contractNumber.ToString();
            return number;
        }

        public bool UploadContract(Guid contractId)
        {
            var stages = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId).ToList();
            var stageStatus = GetStageStatusByCode(OBK_Ref_StageStatus.Active);
            foreach (var dtage in stages)
            {
                dtage.StageStatusId = stageStatus.Id;
            }
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            contract.Status = CodeConstManager.STATUS_OBK_INVOCE_GENERATING;
            AddHistoryAttached(contractId);
            AddExtHistoryActive(contractId);
            AppContext.SaveChanges();
            SendNotificationToApplicantUploaded(contract);
            return true;
        }

        public string GetDataForSign(Guid contractId)
        {
            var contract = AppContext.OBK_Contract.FirstOrDefault(e => e.Id == contractId);
            var obkDeclarant = AppContext.OBK_Declarant.FirstOrDefault(x => x.Id == contract.DeclarantId);
            var obkDeclarantCountry = AppContext.Dictionaries.FirstOrDefault(x => x.Id == obkDeclarant.CountryId);
            var obkDeclarantContact = AppContext.OBK_DeclarantContact.FirstOrDefault(x => x.Id == contract.DeclarantContactId);
            var obkDeclarantOrganizationForm = AppContext.Dictionaries.FirstOrDefault(x => x.Id == obkDeclarant.OrganizationFormId);
            var signerEmployee = AppContext.Employees.FirstOrDefault(x => x.Id == contract.Signer.Value);
            var unitSigner = AppContext.UnitSigners.FirstOrDefault(x => x.SignerId == contract.Signer.Value);
            string unitSignerDocumentTypeNameRu = null, unitSignerDocumentTypeNameKz = null;
            if (unitSigner != null)
            {
                var unitSignerDocumentType = AppContext.OBK_Ref_ContractDocumentType.FirstOrDefault(x => x.Id == unitSigner.DocumentType);
                unitSignerDocumentTypeNameRu = unitSignerDocumentType.NameRu;
                unitSignerDocumentTypeNameKz = unitSignerDocumentType.NameKz;
            }

            var unit_signer = AppContext.Units.FirstOrDefault(x => x.EmployeeId == contract.Signer.Value);
            var bossDocType = AppContext.Dictionaries.FirstOrDefault(x => x.Id == obkDeclarantContact.BossDocType);
            var unit = AppContext.Units.FirstOrDefault(x => x.Id == contract.ExpertOrganization);
            var unitAddress = AppContext.UnitsAddresses.FirstOrDefault(x => x.UnitsId == unit.Id);
            var currenciesTenge = AppContext.Dictionaries.Where(x => x.Code == "398").Select(x => x.Id).ToList();
            var unitBank = AppContext.UnitsBanks.FirstOrDefault(x => x.UnitsId == unit.Id && x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsDeleted == false && x.CurrencyId != null && currenciesTenge.Contains(x.CurrencyId.Value));

            List<OBKContractProduct> products = AppContext.OBKContractProductsViews.Where(x => x.ContractId == contractId).
                Select(x =>
                new OBKContractProduct()
                {
                    NameRu = x.NameRu,
                    ProdSeries = x.ProdSeries,
                    ProdSeriesEndDate = x.ProdSeriesEndDate,
                    ProdSeriesParty = x.ProdSeriesParty,
                    ProdCountryNameRu = x.ProdCountryNameRu,
                    ProdProducerNameRu = x.ProdProducerNameRu,
                    ProdShortName = x.ProdShortName
                }).ToList();

            var dataForSign = new OBKContractSignData()
            {
                SignerFullName = signerEmployee.FullName,
                SignerDocTypeNameRu = unitSignerDocumentTypeNameRu,
                SignerDocTypeNameKz = unitSignerDocumentTypeNameKz,
                SignerDocNumber = unitSigner?.DocNumber,
                DeclarantOrganizationFormNameRu = obkDeclarantOrganizationForm.Name,
                DeclarantOrganizationFormNameKz = obkDeclarantOrganizationForm.NameKz,
                DeclarantNameRu = obkDeclarant.NameRu,
                DeclarantNameKz = obkDeclarant.NameKz,
                DeclarantBossPositionRu = obkDeclarantContact.BossPosition,
                DeclarantBossPositionKz = obkDeclarantContact.BossPositionKz,
                DeclarantBossFirstName = obkDeclarantContact.BossFirstName,
                DeclarantBossLastName = obkDeclarantContact.BossLastName,
                DeclarantBossMiddleName = obkDeclarantContact.BossMiddleName,
                DeclarantBossDocTypeRu = bossDocType.Name,
                DeclarantBossDocTypeKz = bossDocType.NameKz,
                ExpertOrganizationNameRu = unit.Name,
                ExpertOrganizationNameKz = unit.NameKz,
                ExpertOrganizationAddressNameRu = unitAddress?.AddressNameRu,
                ExpertOrganizationAddressNameKz = unitAddress?.AddressNameKz,
                ExpertOrganizationKbe = unitBank?.KBE,
                ExpertOrganizationBin = unit.Bin,
                ExpertOrganizationBankSwift = unitBank?.SWIFT,
                ExpertOrganizationBankIik = unitBank?.IIK,
                ExpertOrganizationBankNameRu = unitBank?.BankNameRu,
                ExpertOrganizationBankNameKz = unitBank?.BankNameKz,
                SignerPositionRu = signerEmployee.DisplayName,
                SignerPositionKz = unit_signer.NameKz,
                SignerShortName = signerEmployee.ShortName,
                DeclarantCountryNameRu = obkDeclarantCountry.Name,
                DeclarantCountryNameKz = obkDeclarantCountry.NameKz,
                DeclarantAddressLegalRu = obkDeclarantContact.AddressLegalRu,
                DeclarantAddressLegalKz = obkDeclarantContact.AddressLegalKz,
                DeclarantBin = obkDeclarant.Bin,
                DeclarantBankBik = obkDeclarantContact.BankBik,
                DeclarantBankIik = obkDeclarantContact.BankIik,
                DeclarantBankNameRu = obkDeclarantContact.BankNameRu,
                DeclarantBankNameKz = obkDeclarantContact.BankNameKz,
                Products = products
            };
            var xmlData = SerializeHelper.SerializeDataContract(dataForSign);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public int SignContractApplicant(Guid contractId, string signedData)
        {
            var signData = AppContext.OBK_ContractSignedDatas.Where(x => x.ContractId == contractId).FirstOrDefault();
            if (signData != null)
            {
                signData.ApplicantSignDate = DateTime.Now;
                signData.ApplicantSign = signedData;
                signData.CeoSignDate = null;
                signData.CeoSign = null;
            }
            else
            {
                signData = new OBK_ContractSignedDatas();
                signData.ContractId = contractId;
                signData.ApplicantSignDate = DateTime.Now;
                signData.ApplicantSign = signedData;
                signData.CeoSignDate = null;
                signData.CeoSign = null;
                AppContext.OBK_ContractSignedDatas.Add(signData);
            }
            AppContext.SaveChanges();
            var status = SendContractInProcessing(contractId, true);
            return status;
        }

        public bool SignContractCeo(Guid contractId, string signedData)
        {
            var signData = AppContext.OBK_ContractSignedDatas.Where(x => x.ContractId == contractId).FirstOrDefault();
            if (signData != null)
            {
                signData.CeoSignDate = DateTime.Now;
                signData.CeoSign = signedData;
            }
            else
            {
                signData = new OBK_ContractSignedDatas();
                signData.ContractId = contractId;
                signData.ApplicantSignDate = null;
                signData.ApplicantSign = null;
                signData.CeoSignDate = DateTime.Now;
                signData.CeoSign = signedData;
                AppContext.OBK_ContractSignedDatas.Add(signData);
            }

            var stages = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId).ToList();
            OBK_Ref_StageStatus stageStatus = GetStageStatusByCode(OBK_Ref_StageStatus.RequiresRegistration);
            foreach (var dtage in stages)
            {
                dtage.StageStatusId = stageStatus.Id;
            }
            AddHistorySigned(contractId);
            AppContext.SaveChanges();

            SendNotificationToCozExecutorSigned(contractId);

            return true;
        }

        private Stream GetContractTemplatePdf(Guid id)
        {
            var db = new ncelsEntities();
            string name = "Договор_на_проведение_оценки_безопасности_и_качества.pdf";
            StiReport report = new StiReport();
            try
            {
                report.Load(GetContractTemplatePath(id));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["ContractId"].ValueObject = id;

                var price = new OBKContractRepository().GetPriceCount(id);
                report.Dictionary.Variables["PriceCount"].ValueObject = price;

                var priceText = RuDateAndMoneyConverter.ToTextTenge(Convert.ToDouble(price), false);
                report.Dictionary.Variables["PriceCountName"].ValueObject = priceText;

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }

            Stream stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Word2007, stream);
            stream.Position = 0;

            Aspose.Words.Document doc = new Aspose.Words.Document(stream);

            try
            {
                var signData = db.OBK_ContractSignedDatas.Where(x => x.ContractId == id).FirstOrDefault();
                if (signData != null && signData.ApplicantSign != null && signData.CeoSign != null)
                {
                    doc.InserQrCodesToEnd("ApplicantSign", signData.ApplicantSign);
                    doc.InserQrCodesToEnd("CeoSign", signData.CeoSign);
                }
            }
            catch (Exception ex)
            {

            }

            var file = new MemoryStream();
            doc.Save(file, Aspose.Words.SaveFormat.Pdf);
            file.Position = 0;

            return file;
        }

        private void AddHistorySentByApplicant(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.SentByApplicant;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistorySentToWork(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.SentToWork;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistoryMeetsRequirements(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.MeetsRequirements;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistoryDoesNotMeetRequirements(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.DoesNotMeetRequirements;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistoryReturned(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Returned;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistorySentToApproval(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.SentToApproval;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistoryRefused(Guid contractId, string reason)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Refused;
            AddHistory(contractId, historyStatusCode, reason);
        }

        private void AddHistoryApproved(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Approved;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistorySigned(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Signed;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistoryRegistered(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Registered;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistoryAttached(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Attached;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistory(Guid contractId, string historyStatusCode, string reason = null)
        {
            var currentEmployee = UserHelper.GetCurrentEmployee();

            var status = GetContractHistoryStatusByCode(historyStatusCode);

            var unitName = GetParentUnitName(currentEmployee);

            var history = new OBK_ContractHistory()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                RefuseReason = reason,
                ContractId = contractId,
                EmployeeId = currentEmployee.Id,
                UnitName = unitName,
                StatusId = status.Id,
            };
            AppContext.OBK_ContractHistory.Add(history);
        }

        private void AddExtHistoryDraft(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractExtHistoryStatus.Draft;
            var currentEmployee = UserHelper.GetCurrentEmployee();
            AddExtHistory(contractId, historyStatusCode, currentEmployee.Id);
        }

        private void AddExtHistoryInprocessing(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractExtHistoryStatus.Inprocessing;
            var currentEmployee = UserHelper.GetCurrentEmployee();
            AddExtHistory(contractId, historyStatusCode, currentEmployee.Id);
        }

        private void AddExtHistoryWork(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractExtHistoryStatus.Work;
            AddExtHistory(contractId, historyStatusCode);
        }

        private void AddExtHistoryOncorrection(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractExtHistoryStatus.Oncorrection;
            AddExtHistory(contractId, historyStatusCode);
        }

        private void AddExtHistoryActive(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractExtHistoryStatus.Active;
            AddExtHistory(contractId, historyStatusCode);
        }

        private void AddExtHistory(Guid contractId, string historyStatusCode, Guid? employeeId = null)
        {
            var status = GetContractExtHistoryStatusByCode(historyStatusCode);

            var history = new OBK_ContractExtHistory()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ContractId = contractId,
                StatusId = status.Id,
                EmployeeId = employeeId
            };
            AppContext.OBK_ContractExtHistory.Add(history);
        }

        private OBK_Ref_ContractExtHistoryStatus GetContractExtHistoryStatusByCode(string code)
        {
            return AppContext.OBK_Ref_ContractExtHistoryStatus.Where(x => x.Code == code).FirstOrDefault();
        }

        public IQueryable<OBK_ContractHistoryView> GetHistory(Guid contractId)
        {
            return AppContext.OBK_ContractHistoryView.AsNoTracking().Where(x => x.ContractId == contractId);
        }

        public IEnumerable<object> GetExtHistory(Guid? contractId)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            var shortName = contract?.Unit?.ShortName;

            var list = AppContext.OBK_ContractExtHistory.Where(x => x.ContractId == contractId).OrderBy(x => x.Created).AsEnumerable()
                .Select(x => new
                {
                    Created = x.Created.ToString(DateHelper.DATE_TIME_FORMAT),
                    StatusNameRu = x.OBK_Ref_ContractExtHistoryStatus.NameRu,
                    StatusNameKz = x.OBK_Ref_ContractExtHistoryStatus.NameKz,
                    Author = x.Employee != null ? x.Employee.DisplayName : shortName,
                    StatusDescriptionRu = x.OBK_Ref_ContractExtHistoryStatus.DescriptionRu,
                    StatusDescriptionKz = x.OBK_Ref_ContractExtHistoryStatus.DescriptionKz
                }).ToList();

            return list;
        }

        public List<OBK_Instruction> GetInstructions(int registerId)
        {
            List<OBK_Instruction> list = new List<OBK_Instruction>();
            var queryString = "select * from register_instructions inner join instruction_types on instruction_types.id = register_instructions.instruction_type_id where register_id = @register_id";
            var conString = GetConnectionString();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(queryString, con);
                command.Parameters.AddWithValue("@register_id", registerId);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = (int)reader["id"];
                    var name = reader["name"] != null ? reader["name"].ToString() : null;
                    var name_kz = reader["name_kz"] != null ? reader["name_kz"].ToString() : null;
                    var instruction = new OBK_Instruction { Id = id, Name = name, NameKz = name_kz };
                    list.Add(instruction);
                }
                reader.Close();
            }
            return list;
        }

        public byte[] GetInstructionFile(int instructionid)
        {
            byte[] fileBytes = null;
            var conString = GetConnectionString();
            var queryString = "select * from register_instructions_files where id = @instructionid";
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(queryString, con);
                command.Parameters.AddWithValue("@instructionid", instructionid);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    fileBytes = reader["file_rus"] != null ? (byte[])reader["file_rus"] : null;
                    break;
                }
                reader.Close();
            }
            return fileBytes;
        }

        private string GetConnectionString()
        {
            var conString = ConfigurationManager.ConnectionStrings["register_portal"].ToString();
            return conString;
        }

        #region Notifications
        private void SendNotificationToUobkBoss(OBK_Contract contract, Guid bossUobkGuid)
        {
            string organizationForm = "", orgName = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                organizationForm = orgForm.Name;
            }
            orgName = contract.OBK_Declarant?.NameRu;
            var message = string.Format("Поступил новый договор от {0} \"{1}\"", organizationForm, orgName);
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, bossUobkGuid);
        }

        private void SendNotificationToCozBoss(OBK_Contract contract, Guid bossCozGuid)
        {
            string organizationForm = "", orgName = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                organizationForm = orgForm.Name;
            }
            orgName = contract.OBK_Declarant?.NameRu;
            var message = string.Format("Поступил новый договор от {0} \"{1}\"", organizationForm, orgName);
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, bossCozGuid);
        }

        private void SendNotificationToExecutorWork(Guid executorId, OBK_Contract contract)
        {
            string organizationForm = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                organizationForm = orgForm.Name;
            }
            var orgName = contract.OBK_Declarant?.NameRu;
            var message = string.Format("Поступил новый договор от {0} \"{1}\"", organizationForm, orgName);
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, executorId);
        }

        private void SendNotificationToDefExecutorNew(Guid contractId, Guid bossDefGuid)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            string organizationForm = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                organizationForm = orgForm.Name;
            }
            var orgName = contract.OBK_Declarant?.NameRu;

            var message = string.Format("Поступил новый договор от {0} \"{1}\"", organizationForm, orgName);
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contractId, bossDefGuid);
        }

        private void SendNotificationToCozExecutor(List<OBK_ContractRegisterView> contractViews)
        {
            foreach (var contractView in contractViews)
            {
                var stage = AppContext.OBK_ContractStage.Where(x => x.Id == contractView.ContractStageId).FirstOrDefault();
                if (stage.StageId != CodeConstManager.STAGE_OBK_COZ)
                {
                    string nameStage = "";
                    if (stage.StageId == CodeConstManager.STAGE_OBK_UOBK)
                    {
                        nameStage = "УОБК";
                    }
                    else if (stage.StageId == CodeConstManager.STAGE_OBK_DEF)
                    {
                        nameStage = "ДЭФ";
                    }

                    var contract = AppContext.OBK_Contract.Where(x => x.Id == contractView.ContractId).FirstOrDefault();
                    string organizationForm = "";
                    if (contract.OBK_Declarant.OrganizationFormId != null)
                    {
                        var orgForm = AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                        organizationForm = orgForm.Name;
                    }
                    var orgName = contract.OBK_Declarant?.NameRu;
                    var stageCoz = AppContext.OBK_ContractStage.Where(x => x.ContractId == stage.ContractId && x.StageId == CodeConstManager.STAGE_OBK_COZ).FirstOrDefault();
                    var stageCozExecutor = AppContext.OBK_ContractStageExecutors.Where(x => x.ContractStageId == stageCoz.Id && x.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR).FirstOrDefault();

                    var message = string.Format("Договор от {0} \"{1}\" обработан специалистом {2}", organizationForm, orgName, nameStage);
                    new NotificationManager().SendNotification(message, ObjectType.ObkContract, contractView.ContractId, stageCozExecutor.ExecutorId);
                }
            }
        }

        private void SendNotificationToApplicantReturned(OBK_Contract contract)
        {
            string type = contract.OBK_Ref_Type.NameRu;
            string message = string.Format("Договор с типом {0} возвращен на доработку", type);
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, contract.EmployeeId.Value);
        }

        private void SendNotificationToExecutorsFixedErrors(OBK_Contract contract)
        {
            string organizationForm = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                organizationForm = orgForm.Name;
            }
            var orgName = contract.OBK_Declarant?.NameRu;

            var message = string.Format("Договор от {0} \"{1}\" исправлен заявителем", organizationForm, orgName);

            var stages = AppContext.OBK_ContractStage.Where(x => x.ContractId == contract.Id).ToList();

            foreach (var stage in stages)
            {
                if (stage.StageId == CodeConstManager.STAGE_OBK_COZ)
                {
                    foreach (var executor in stage.OBK_ContractStageExecutors.ToList())
                    {
                        if (executor.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR)
                        {
                            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, executor.ExecutorId);
                        }
                    }
                }
                else if (stage.StageId == CodeConstManager.STAGE_OBK_UOBK)
                {
                    foreach (var executor in stage.OBK_ContractStageExecutors.ToList())
                    {
                        if (executor.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR)
                        {
                            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, executor.ExecutorId);
                        }
                    }
                }
            }
        }

        private void SendNotificationToDefExecutorFixedErrors(Guid contractId, Guid bossDefGuid)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            string organizationForm = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                organizationForm = orgForm.Name;
            }
            var orgName = contract.OBK_Declarant?.NameRu;

            var message = string.Format("Договор от {0} \"{1}\" исправлен заявителем", organizationForm, orgName);
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contractId, bossDefGuid);
        }

        private void SendNotificationToCozExecutorApproved(Guid contractId)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            string organizationForm = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                organizationForm = orgForm.Name;
            }
            var orgName = contract.OBK_Declarant?.NameRu;
            var message = string.Format("Договор от {0} \"{1}\" согласован Руководителем ЦОЗ", organizationForm, orgName);

            var stageCoz = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId && x.StageId == CodeConstManager.STAGE_OBK_COZ).FirstOrDefault();
            foreach (var executor in stageCoz.OBK_ContractStageExecutors)
            {
                if (executor.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR)
                {
                    new NotificationManager().SendNotification(message, ObjectType.ObkContract, contractId, executor.ExecutorId);
                }
            }
        }

        private void SendNotificationToApplicantApproved(Guid contractId)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            string type = contract.OBK_Ref_Type.NameRu;
            var message = string.Format("Договор с типом {0} согласован. Необходимо предоставить договор с подписью в ЦОЗ", type);
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contractId, contract.EmployeeId.Value);
        }

        private void SendNotificationToSignerApproved(Guid contractId)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            var orgName = contract.OBK_Declarant?.NameRu;
            var message = string.Format("Вам поступил договор от {0} на подписание", orgName);
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contractId, contract.Signer.Value);
        }

        private void SendNotificationToCozExecutorSigned(Guid contractId)
        {
            var contract = AppContext.OBK_Contract.Where(x => x.Id == contractId).FirstOrDefault();
            string organizationForm = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                organizationForm = orgForm.Name;
            }
            var orgName = contract.OBK_Declarant?.NameRu;
            var message = string.Format("Договор от {0} \"{1}\" подписан и требует регистрации", organizationForm, orgName);
            var stageCoz = AppContext.OBK_ContractStage.Where(x => x.ContractId == contractId && x.StageId == CodeConstManager.STAGE_OBK_COZ).FirstOrDefault();
            foreach (var executor in stageCoz.OBK_ContractStageExecutors)
            {
                if (executor.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR)
                {
                    new NotificationManager().SendNotification(message, ObjectType.ObkContract, contractId, executor.ExecutorId);
                }
            }
        }

        private void SendNotificationToApplicantRegistered(OBK_Contract contract)
        {
            var message = string.Format("Договор зарегистрирован. № {0}, дата начала действия {1}. Необходимо произвести оплату работ по предоставленному счету на оплату", contract.Number, contract.StartDate?.ToString("dd.MM.yyyy"));
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, contract.EmployeeId.Value);
        }

        private void SendNotificationToApplicantUploaded(OBK_Contract contract)
        {
            var message = string.Format("Договор зарегистрирован. № {0}, дата начала действия {1}. Договор можно просмотреть во вложениях", contract.Number, contract.StartDate?.ToString("dd.MM.yyyy"));
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, contract.EmployeeId.Value);
        }

        private void SendNotificationToAccountantRegistered(OBK_Contract contract)
        {
            /* 
             *  КОД ЗАКОММЕНТИРОВАН ТАК КАК ПОКА НЕ ИЗВЕСТНО КАК ОПРЕДЕЛЯТЬ БУХГАЛТЕРА
            */
            //var accountantId = Guid.Empty;
            //var orgName = contract.OBK_Declarant?.NameRu;
            //var message = string.Format("Зарегистрирован новый договор под № {0}, заявитель \"{1}\"", contract.Number, orgName);
            //new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, accountantId);
        }
        #endregion

        #region Factory
        public object GetFactories(Guid contractId)
        {
            return AppContext.OBK_ContractFactory.Where(x => x.ContractId == contractId).
                 Select(x => new OBKFactoryViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Location = x.Location
                 }).ToList();
        }

        public Guid? AddFactory(Guid contractId, OBKFactoryViewModel factory)
        {
            var newFactory = new OBK_ContractFactory
            {
                Id = Guid.NewGuid(),
                ContractId = contractId,
                Name = factory.Name,
                Location = factory.Location
            };
            AppContext.OBK_ContractFactory.Add(newFactory);
            AppContext.SaveChanges();
            UpdateAdditionalPrice(contractId);
            return newFactory.Id;
        }

        public bool DeleteFactory(OBKFactoryViewModel factory)
        {
            if (factory.Id != null)
            {
                var foundFactory = AppContext.OBK_ContractFactory.Where(x => x.Id == factory.Id).FirstOrDefault();
                if (foundFactory != null)
                {
                    var contractId = foundFactory.ContractId;
                    AppContext.OBK_ContractFactory.Remove(foundFactory);
                    AppContext.SaveChanges();
                    UpdateAdditionalPrice(contractId);
                    return true;
                }
            }
            return false;
        }

        private void UpdateAdditionalPrice(Guid contractId)
        {
            var factories = AppContext.OBK_ContractFactory.Where(x => x.ContractId == contractId).ToList();
            if (factories.Count > 0)
            {
                OBK_ContractPrice contractPrice = AppContext.OBK_ContractPrice.Where(x => x.ContractId == contractId && x.ProductId == null).FirstOrDefault();
                if (contractPrice == null)
                {
                    contractPrice = new OBK_ContractPrice();
                    contractPrice.Id = Guid.NewGuid();
                    contractPrice.ContractId = contractId;
                    contractPrice.ProductId = null;
                    FillContractPriceAdditional(factories.Count, contractPrice);
                    AppContext.OBK_ContractPrice.Add(contractPrice);
                    AppContext.SaveChanges();
                }
                else
                {
                    FillContractPriceAdditional(factories.Count, contractPrice);
                    AppContext.SaveChanges();
                }
            }
            else
            {
                OBK_ContractPrice contractPrice = AppContext.OBK_ContractPrice.Where(x => x.ContractId == contractId && x.ProductId == null).FirstOrDefault();
                if (contractPrice != null)
                {
                    AppContext.OBK_ContractPrice.Remove(contractPrice);
                    AppContext.SaveChanges();
                }
            }
        }

        private void FillContractPriceAdditional(int count, OBK_ContractPrice price)
        {
            // Документ/Экспертиза
            Guid serviceType = new Guid("9106d5e8-35dc-4178-8882-b30166de4c80");

            var type = int.Parse(CodeConstManager.OBK_SA_SERIAL);
            var info = AppContext.OBK_Ref_PriceList.Where(x => x.TypeId == type && x.ServiceTypeId == serviceType).FirstOrDefault();
            if (info != null)
            {
                double tax = GetTax();
                double sum = info.Price * count;
                double res = sum + sum * (tax * 0.01);
                price.Count = count;
                price.PriceRefId = info.Id;
                price.PriceWithoutTax = sum;
                price.PriceWithTax = res;
            }
        }
        #endregion

        public object GetActiveContracts()
        {
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var list = AppContext.OBK_Contract.Where(x => x.EmployeeId == employeeId && x.Status != CodeConstManager.STATUS_OBK_ONCORRECTION && x.Status != CodeConstManager.STATUS_OBK_WORK && x.Status != CodeConstManager.STATUS_OBK_INPROCESSING && x.Status != CodeConstManager.STATUS_OBK_DRAFT_ID).Select(x => new { Id = x.Id, Name = x.Number }).ToList();
            return list;
        }

        public OBKContractAdditionViewModel SaveContractAddition(Guid guid, OBKContractViewModel contractViewModel, OBKContractAdditionViewModel contractAdditionVideModel)
        {
            var obkContract = AppContext.OBK_Contract.Where(o => o.Id == contractAdditionVideModel.Id).FirstOrDefault();
            if (obkContract != null)
            {
                FillContractAddition(obkContract, contractViewModel, contractAdditionVideModel);
                AppContext.SaveChanges();
            }
            else
            {
                OBK_Contract contractAddition = new OBK_Contract();
                contractAddition.Id = guid;
                contractAddition.CreatedDate = DateTime.Now;
                contractAddition.Number = CodeConstManager.OBK_CONTRACT_NO_NUMBER;
                contractAddition.Status = CodeConstManager.STATUS_OBK_DRAFT_ID;

                var employeeId = UserHelper.GetCurrentEmployee().Id;
                contractAddition.EmployeeId = employeeId;

                FillContractAddition(contractAddition, contractViewModel, contractAdditionVideModel);

                AppContext.OBK_Contract.Add(contractAddition);
                AppContext.SaveChanges();
                contractAdditionVideModel.Id = contractAddition.Id;
            }
            return contractAdditionVideModel;
        }

        public object GetContractAddition(Guid id)
        {
            var contractAddition = AppContext.OBK_Contract.Where(x => x.Id == id).FirstOrDefault();
            if (contractAddition != null)
            {
                OBKContractAdditionViewModel contractAdditionViewModel = new OBKContractAdditionViewModel();
                contractAdditionViewModel.Id = contractAddition.Id;
                contractAdditionViewModel.ContractAdditionTypeId = contractAddition.ContractAdditionType;
                contractAdditionViewModel.ContractId = contractAddition.ParentId;

                OBKContractViewModel contractViewModel = new OBKContractViewModel();
                contractViewModel.Id = contractAddition.Id;
                contractViewModel.Type = contractAddition.Type;
                contractViewModel.ExpertOrganization = contractAddition.ExpertOrganization;
                contractViewModel.Signer = contractAddition.Signer;
                contractViewModel.Status = contractAddition.Status;
                contractViewModel.Number = contractAddition.Number;

                if (contractAddition.DeclarantContactId != null)
                {
                    contractViewModel.AddressLegalRu = contractAddition.OBK_DeclarantContact.AddressLegalRu;
                    contractViewModel.AddressLegalKz = contractAddition.OBK_DeclarantContact.AddressLegalKz;
                    contractViewModel.AddressFact = contractAddition.OBK_DeclarantContact.AddressFact;
                    contractViewModel.Phone = contractAddition.OBK_DeclarantContact.Phone;
                    contractViewModel.Email = contractAddition.OBK_DeclarantContact.Email;
                    contractViewModel.BossLastName = contractAddition.OBK_DeclarantContact.BossLastName;
                    contractViewModel.BossFirstName = contractAddition.OBK_DeclarantContact.BossFirstName;
                    contractViewModel.BossMiddleName = contractAddition.OBK_DeclarantContact.BossMiddleName;
                    contractViewModel.BossPosition = contractAddition.OBK_DeclarantContact.BossPosition;
                    contractViewModel.BossPositionKz = contractAddition.OBK_DeclarantContact.BossPositionKz;
                    contractViewModel.BossDocType = contractAddition.OBK_DeclarantContact.BossDocType;
                    contractViewModel.IsHasBossDocNumber = contractAddition.OBK_DeclarantContact.IsHasBossDocNumber;
                    contractViewModel.BossDocNumber = contractAddition.OBK_DeclarantContact.BossDocNumber;
                    contractViewModel.BossDocCreatedDate = contractAddition.OBK_DeclarantContact.BossDocCreatedDate;
                    contractViewModel.BossDocEndDate = contractAddition.OBK_DeclarantContact.BossDocEndDate;
                    contractViewModel.BossDocUnlimited = contractAddition.OBK_DeclarantContact.BossDocUnlimited;
                    contractViewModel.SignerIsBoss = contractAddition.OBK_DeclarantContact.SignerIsBoss;
                    contractViewModel.SignLastName = contractAddition.OBK_DeclarantContact.SignLastName;
                    contractViewModel.SignFirstName = contractAddition.OBK_DeclarantContact.SignFirstName;
                    contractViewModel.SignMiddleName = contractAddition.OBK_DeclarantContact.SignMiddleName;
                    contractViewModel.SignPosition = contractAddition.OBK_DeclarantContact.SignPosition;
                    contractViewModel.SignPositionKz = contractAddition.OBK_DeclarantContact.SignPositionKz;
                    contractViewModel.SignDocType = contractAddition.OBK_DeclarantContact.SignDocType;
                    contractViewModel.IsHasSignDocNumber = contractAddition.OBK_DeclarantContact.IsHasSignDocNumber;
                    contractViewModel.SignDocNumber = contractAddition.OBK_DeclarantContact.SignDocNumber;
                    contractViewModel.SignDocCreatedDate = contractAddition.OBK_DeclarantContact.SignDocCreatedDate;
                    contractViewModel.SignDocEndDate = contractAddition.OBK_DeclarantContact.SignDocEndDate;
                    contractViewModel.SignDocUnlimited = contractAddition.OBK_DeclarantContact.SignDocUnlimited;
                    contractViewModel.BankIik = contractAddition.OBK_DeclarantContact.BankIik;
                    contractViewModel.BankBik = contractAddition.OBK_DeclarantContact.BankBik;
                    contractViewModel.CurrencyId = contractAddition.OBK_DeclarantContact.CurrencyId;
                    contractViewModel.BankNameRu = contractAddition.OBK_DeclarantContact.BankNameRu;
                    contractViewModel.BankNameKz = contractAddition.OBK_DeclarantContact.BankNameKz;
                }

                return new { contractAddition = contractAdditionViewModel, contract = contractViewModel };
            }
            return null;
        }

        private void FillContractAddition(OBK_Contract contractAddition, OBKContractViewModel contractViewModel, OBKContractAdditionViewModel contractAdditionVideModel)
        {
            contractAddition.ContractAdditionType = contractAdditionVideModel.ContractAdditionTypeId;
            contractAddition.ParentId = contractAdditionVideModel.ContractId;

            if (contractAdditionVideModel.ContractId != null)
            {
                var parentContract = AppContext.OBK_Contract.Where(x => x.Id == contractAdditionVideModel.ContractId).FirstOrDefault();
                contractAddition.Type = parentContract.Type;
                contractAddition.ExpertOrganization = parentContract.ExpertOrganization;
                contractAddition.Signer = parentContract.Signer;

                contractAddition.DeclarantId = parentContract.DeclarantId;
                if (contractAddition.OBK_DeclarantContact == null)
                {
                    contractAddition.OBK_DeclarantContact = new OBK_DeclarantContact();
                    contractAddition.OBK_DeclarantContact.Id = Guid.NewGuid();
                }
                FillContactData(contractAddition, parentContract);
                if (contractAdditionVideModel.ContractAdditionTypeId != null)
                {
                    var contractAdditionType = AppContext.Dictionaries.Where(x => x.Id == contractAdditionVideModel.ContractAdditionTypeId).FirstOrDefault();
                    if (contractAdditionType != null)
                    {
                        switch (contractAdditionType.Code)
                        {
                            case "1":
                                FillContactDataAddress(contractAddition.OBK_DeclarantContact, contractViewModel);
                                break;
                            case "2":
                                FillContactDataBoss(contractAddition.OBK_DeclarantContact, contractViewModel);
                                break;
                            case "3":
                                FillContactDataBank(contractAddition.OBK_DeclarantContact, contractViewModel);
                                break;
                        }
                    }
                }
            }
        }

        private void FillContactData(OBK_Contract contractAddition, OBK_Contract parentContract)
        {
            contractAddition.OBK_DeclarantContact.DeclarantId = parentContract.OBK_DeclarantContact.DeclarantId;
            contractAddition.OBK_DeclarantContact.AddressLegalRu = parentContract.OBK_DeclarantContact.AddressLegalRu;
            contractAddition.OBK_DeclarantContact.AddressLegalKz = parentContract.OBK_DeclarantContact.AddressLegalKz;
            contractAddition.OBK_DeclarantContact.AddressFact = parentContract.OBK_DeclarantContact.AddressFact;
            contractAddition.OBK_DeclarantContact.Phone = parentContract.OBK_DeclarantContact.Phone;
            contractAddition.OBK_DeclarantContact.Email = parentContract.OBK_DeclarantContact.Email;
            contractAddition.OBK_DeclarantContact.BankNameRu = parentContract.OBK_DeclarantContact.BankNameRu;
            contractAddition.OBK_DeclarantContact.BankNameKz = parentContract.OBK_DeclarantContact.BankNameKz;
            contractAddition.OBK_DeclarantContact.BankIik = parentContract.OBK_DeclarantContact.BankIik;
            contractAddition.OBK_DeclarantContact.BankBik = parentContract.OBK_DeclarantContact.BankBik;
            contractAddition.OBK_DeclarantContact.CurrencyId = parentContract.OBK_DeclarantContact.CurrencyId;
            contractAddition.OBK_DeclarantContact.BossFio = parentContract.OBK_DeclarantContact.BossFio;
            contractAddition.OBK_DeclarantContact.BossPosition = parentContract.OBK_DeclarantContact.BossPosition;
            contractAddition.OBK_DeclarantContact.BossLastName = parentContract.OBK_DeclarantContact.BossLastName;
            contractAddition.OBK_DeclarantContact.BossFirstName = parentContract.OBK_DeclarantContact.BossFirstName;
            contractAddition.OBK_DeclarantContact.BossMiddleName = parentContract.OBK_DeclarantContact.BossMiddleName;
            contractAddition.OBK_DeclarantContact.BossDocNumber = parentContract.OBK_DeclarantContact.BossDocNumber;
            contractAddition.OBK_DeclarantContact.BossDocType = parentContract.OBK_DeclarantContact.BossDocType;
            contractAddition.OBK_DeclarantContact.IsHasBossDocNumber = parentContract.OBK_DeclarantContact.IsHasBossDocNumber;
            contractAddition.OBK_DeclarantContact.BossDocCreatedDate = parentContract.OBK_DeclarantContact.BossDocCreatedDate;
            contractAddition.OBK_DeclarantContact.SignType = parentContract.OBK_DeclarantContact.SignType;
            contractAddition.OBK_DeclarantContact.SignLastName = parentContract.OBK_DeclarantContact.SignLastName;
            contractAddition.OBK_DeclarantContact.SignFirstName = parentContract.OBK_DeclarantContact.SignFirstName;
            contractAddition.OBK_DeclarantContact.SignMiddleName = parentContract.OBK_DeclarantContact.SignMiddleName;
            contractAddition.OBK_DeclarantContact.SignPosition = parentContract.OBK_DeclarantContact.SignPosition;
            contractAddition.OBK_DeclarantContact.SignDocType = parentContract.OBK_DeclarantContact.SignDocType;
            contractAddition.OBK_DeclarantContact.IsHasSignDocNumber = parentContract.OBK_DeclarantContact.IsHasSignDocNumber;
            contractAddition.OBK_DeclarantContact.SignDocNumber = parentContract.OBK_DeclarantContact.SignDocNumber;
            contractAddition.OBK_DeclarantContact.SignDocCreatedDate = parentContract.OBK_DeclarantContact.SignDocCreatedDate;
            contractAddition.OBK_DeclarantContact.CreateDate = parentContract.OBK_DeclarantContact.CreateDate;
            contractAddition.OBK_DeclarantContact.SignDocEndDate = parentContract.OBK_DeclarantContact.SignDocEndDate;
            contractAddition.OBK_DeclarantContact.SignDocUnlimited = parentContract.OBK_DeclarantContact.SignDocUnlimited;
            contractAddition.OBK_DeclarantContact.BossDocEndDate = parentContract.OBK_DeclarantContact.BossDocEndDate;
            contractAddition.OBK_DeclarantContact.BossDocUnlimited = parentContract.OBK_DeclarantContact.BossDocUnlimited;
            contractAddition.OBK_DeclarantContact.SignerIsBoss = parentContract.OBK_DeclarantContact.SignerIsBoss;
            contractAddition.OBK_DeclarantContact.SignPositionKz = parentContract.OBK_DeclarantContact.SignPositionKz;
            contractAddition.OBK_DeclarantContact.BossPositionKz = parentContract.OBK_DeclarantContact.BossPositionKz;
        }

        private void FillContactDataAddress(OBK_DeclarantContact declarantContact, OBKContractViewModel contractViewModel)
        {
            declarantContact.AddressFact = contractViewModel.AddressFact;
            declarantContact.AddressLegalKz = contractViewModel.AddressLegalKz;
            declarantContact.AddressLegalRu = contractViewModel.AddressLegalRu;
            declarantContact.Phone = contractViewModel.Phone;
            declarantContact.Email = contractViewModel.Email;
        }

        private void FillContactDataBoss(OBK_DeclarantContact declarantContact, OBKContractViewModel contractViewModel)
        {
            declarantContact.BossLastName = contractViewModel.BossLastName;
            declarantContact.BossFirstName = contractViewModel.BossFirstName;
            declarantContact.BossMiddleName = contractViewModel.BossMiddleName;
            declarantContact.BossPosition = contractViewModel.BossPosition;
            declarantContact.BossPositionKz = contractViewModel.BossPositionKz;
            declarantContact.BossDocType = contractViewModel.BossDocType;
            declarantContact.BossDocUnlimited = contractViewModel.BossDocUnlimited;
            declarantContact.IsHasBossDocNumber = contractViewModel.IsHasBossDocNumber;
            declarantContact.BossDocNumber = contractViewModel.BossDocNumber;
            declarantContact.BossDocCreatedDate = contractViewModel.BossDocCreatedDate;
            declarantContact.BossDocEndDate = contractViewModel.BossDocEndDate;
        }

        private void FillContactDataBank(OBK_DeclarantContact declarantContact, OBKContractViewModel contractViewModel)
        {
            declarantContact.BankIik = contractViewModel.BankIik;
            declarantContact.BankBik = contractViewModel.BankBik;
            declarantContact.CurrencyId = contractViewModel.CurrencyId;
            declarantContact.BankNameKz = contractViewModel.BankNameKz;
            declarantContact.BankNameRu = contractViewModel.BankNameRu;
        }
    }
}
