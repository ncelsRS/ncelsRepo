using System.ComponentModel.DataAnnotations;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(ReestrDirectionToPayViewMetadata))]
    public partial class ReestrDirectionToPayView
    {
        public class ReestrDirectionToPayViewMetadata
        {

        }

        public string Name
        {
            get
            {
                return DeclarationNameRu + ", " + DosageRu + ", " + DrugFormRu;
            }
        }

        public string PayerName
        {
            get
            {
                return PayerNameRu;
            }
        }

        public string ContractInfo
        {
            get
            {
                return string.Format("№ {0} от {1:dd.MM.yyyy}",
                    ContractNumber, ContractDate);
            }
        }

        public string TypeName
        {
            get { return TypeNameRu; }
        }

        public string CurrencyName
        {
            get { return "KZT"; }
        }
    }
}