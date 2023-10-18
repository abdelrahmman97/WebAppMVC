using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AppRepository.Models
{
    public class UserSignInViewModel
    {
        [Required, StringLength(50, MinimumLength = 3)]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required, StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; } = false;

        public string ReturnUrl { get; set; }
    }
}
