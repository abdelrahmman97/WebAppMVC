using System.ComponentModel.DataAnnotations;


namespace AppRepository.Models
{
    public class UserForgotPasswordViewModel
    {
        public string Code { get; set; } = "";
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 8), Display(Name = "New Password"), DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
