using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;

namespace Teme.Common.Data.OutDto
{
    public class IconRecordOutDto
    {
        //public IconRecordOutDto(IconRecord ir)
        //{

        //    id = ir.Id;
        //    UserName = ir.AuthUser.FirstName;
        //    RoleName = ir.AuthRoleId.ToString();
        //    ValueField = ir.ValueField;
        //    DisplayField = ir.DisplayField;
        //    Note = ir.Note;
        //    DateCreate = ir.DateCreate;

        //}
        public int id { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string ValueField { get; set; }
        public string DisplayField { get; set; }
        public string Note { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
