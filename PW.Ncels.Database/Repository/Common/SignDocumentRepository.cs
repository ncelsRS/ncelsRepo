using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.Common
{
    public class SignDocumentRepository : ARepository
    {
        public void SaveSignDocument(Guid authorId,string xml, object model)
        {
            var signDocument = new SignDocument();
            if (model != null)
            {
                signDocument.ClassName = model.GetType().FullName;
                var entity = model as IEntity;
                if (entity != null)
                {
                    signDocument.ObjectId = entity.Id.ToString();
                }
            }
          
            signDocument.CreatedTime = DateTime.Now;
            signDocument.EmployeeId = authorId;
            signDocument.SignXml = xml;
            AppContext.SignDocuments.Add(signDocument);
            AppContext.SaveChanges();   
        }
    }
}
