
using System.ComponentModel.DataAnnotations;

namespace AppRepository.Models
{
    public class UserChangePasswordViewModel
    {
        public string Id { get; set; } = "";
        [Required, StringLength(50, MinimumLength = 8), Display(Name = "Current Password"), DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required, StringLength(50, MinimumLength = 8), Display(Name = "New Password"), DataType(DataType.Password), Compare("ConfirmNewPassword")]
        public string NewPassword { get; set; }

        [Required, StringLength(50, MinimumLength = 8), Display(Name = "Confirm New Password"), DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
