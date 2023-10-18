using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AppRepository.Models
{
    public class UserSignUpViewModel
    {
        public string Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required, StringLength(14, MinimumLength = 14)]
        [DisplayName("National ID")]
        public string NationalID { get; set; }

        public string Picture { get; set; } = "notfound.png";

        [Required, StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required, StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }


        [Required, StringLength(15, MinimumLength = 11)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }


        [Required, StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required, StringLength(20, MinimumLength = 8)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
		public string Role { get; set; }
	}
}
