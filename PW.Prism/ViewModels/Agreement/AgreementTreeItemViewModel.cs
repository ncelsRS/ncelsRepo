using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Prism.ViewModels.Agreement
{
    public class AgreementTreeItemViewModel
    {

        public AgreementTreeItemViewModel()
        {
            Childrens = new List<AgreementTreeItemViewModel>();
        }

        public string Id { get; set; }

        public int OrderN { get; set; }

        public string Executor { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public string CreatedDatetime { get; set; }

        public string ExecutedDatetime { get; set; }

        public string Image
        {
            get
            {
                return string.Format("../../Content/images/TaskType_31.png");
            }
        }

        public string ParentId { get; set; }

        public bool HasChildren
        {
            get { return Childrens != null && Childrens.Any(); }
        }

        public bool IsAllowEdit { get; set; } = false;

        public IList<AgreementTreeItemViewModel> Childrens { get; set; }
        
        public static AgreementTreeItemViewModel Find(IEnumerable<AgreementTreeItemViewModel> tree, string id)
        {
            AgreementTreeItemViewModel node = null;
            if (id == null)
                return null;

            foreach (AgreementTreeItemViewModel item in tree)
            {
                if (item.Id == id)
                {
                    node = item;
                    break;
                }
                if (item.HasChildren)
                {
                    node = Find(item.Childrens, id);
                    if (node != null)
                    {
                        break;
                    }
                }
            }
            return node;
        }
    }
}