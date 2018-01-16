using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Constants
{
    public static class EmpCodeConsts
    {
        /// <summary>
        /// Список документов по ТЭМИ для калькулятора "Тип изменения"
        /// </summary>
        public const string ATTACH_CONTRACT_FILE = "sysAttachEMPContract";
        /// <summary>
        /// Список документов по ТЭМИ для калькулятора по классу опасности
        /// </summary>
        public const string ATTACH_CONTRACT_DEGREERISK_FILE = "sysAttachDegreeRiskEMPContract";
        /// <summary>
        /// Список документов по ТЭМИ ЕАЭС ГП
        /// </summary>
        public const string ATTACH_CONTRACT_FILE_EAES_GP = "sysAttachEMPContractEAESGP";
        /// <summary>
        /// Список документов по ТЭМИ ЕАЭС РГ
        /// </summary>
        public const string ATTACH_CONTRACT_FILE_EAES_RG = "sysAttachEMPContractEAESRG";
    }
}
