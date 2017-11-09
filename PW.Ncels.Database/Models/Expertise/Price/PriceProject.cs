using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;
using PW.Ncels.Database.Entities;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Recources;
using PW.Ncels.Database.Repository;

namespace PW.Ncels.Database.DataModel
{
    [Serializable]
    [XmlRoot()]
    /// <summary>
    /// Заявление на ЛС
    /// </summary>
    public partial class PriceProject : IEntity
    {
        [OnSerializing]
        internal void OnSerializedMethod(StreamingContext context)
        {
            PriceProjectsHistories = null;
        }

    }
}
