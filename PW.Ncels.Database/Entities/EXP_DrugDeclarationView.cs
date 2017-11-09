namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class EXP_DrugDeclarationView
    {

        public string Name
        {
            get { return NameRu; }
        }

        public bool IsSelect { get; set; }
    }
}