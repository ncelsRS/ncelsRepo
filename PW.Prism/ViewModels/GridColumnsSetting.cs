using System.Collections.Generic;

namespace PW.Prism.ViewModels
{
    public class GridColumnsSetting
    {
        public GridColumnsSetting()
        {
            Columns=new List<GridColumnModel>();
        }
        public List<GridColumnModel> Columns { get; set; }
    }
}