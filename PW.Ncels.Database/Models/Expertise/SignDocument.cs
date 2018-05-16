using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.DataModel
{
    [MetadataType(typeof(SignDocumentMetaData))]
    public partial class SignDocument
    {
    }

    public class SignDocumentMetaData
    {
        [StringLength(Int32.MaxValue)]
        public string SignXml { get; set; }
    }
}
