using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using PW.Ncels.Database.Constants;

namespace PW.Ncels.Models
{
    public class EcpUserModel
    {
        public long Id { get; set; }

        [Display(Name = "БИН/ИИН")]
        [DisplayFormat(ConvertEmptyStringToNull = false, NullDisplayText = "")]
        [Required]
        public string BINIIN
        { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required]
        public string Phone
        { get; set; }

        [Display(Name = "Эл. почта")]
        [Required]
        public string Email
        { get; set; }

        [Display(Name = "Имя")]
        [Required]
        public string FirstName
        { get; set; }


        [Display(Name = "Фамилия")]
        [Required]
        public string LastName
        { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName
        { get; set; }

        [Display(Name = "Организация")]
        public string JuridicalName
        { get; set; }

        public PersonTypeEnum TypePerson { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

       

    }
}