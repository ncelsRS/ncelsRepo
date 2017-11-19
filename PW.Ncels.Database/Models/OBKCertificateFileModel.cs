using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models
{
    public    class OBKCertificateFileModel
    {
        public string AttachPath { get; set; }
        public List<UploadInitialFile> AttachFiles { get; set; }
    }
}
