using System.ComponentModel.DataAnnotations;
using PW.Ncels.Database.Recources;


namespace PW.Ncels.Database.Models
{

    public class ResetPasswordModel {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(Messages), Name = "RegisterModel_Email_Email_address")]
        public string Email { get; set; }
    }

    public class ChangePasswordModel
    {

		public string DisplayName { get; set; }
        [Required]
        [DataType(DataType.Password)]
		[Display(ResourceType = typeof(Messages), Name = "ChangePasswordModel_OldPassword_Текущий_пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (Messages), ErrorMessageResourceName = "ChangePasswordModel_NewPassword_The__0__must_be_at_least__2__characters_long_", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Messages), Name = "ChangePasswordModel_NewPassword_Новый_пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
		[Display(ResourceType = typeof (Messages), Name = "ChangePasswordModel_ConfirmPassword_Подтверждения_новый_пароль")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (Messages), ErrorMessageResourceName = "ChangePasswordModel_ConfirmPassword_Пароли_не_совпадают")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(ResourceType = typeof (Messages), Name = "LogOnModel_UserName_User_name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Messages), Name = "LogOnModel_Password_Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof (Messages), Name = "LogOnModel_RememberMe_Remember_me_")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(ResourceType = typeof (Messages), Name = "LogOnModel_UserName_User_name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof (Messages), Name = "RegisterModel_Email_Email_address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (Messages), ErrorMessageResourceName = "ChangePasswordModel_NewPassword_The__0__must_be_at_least__2__characters_long_", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Messages), Name = "LogOnModel_Password_Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Messages), Name = "RegisterModel_ConfirmPassword_Confirm_password")]
		[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof (Messages), ErrorMessageResourceName = "RegisterModel_ConfirmPassword_The_password_and_confirmation_password_do_not_match_")]
        public string ConfirmPassword { get; set; }
    }
}
