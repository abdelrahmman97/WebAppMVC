using Models;

namespace AppRepository.Models
{
    public static class UserExtaintion
    {
        public static User ToModel(this UserSignUpViewModel viewModel)
        {
            return new User
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                Picture = viewModel.Picture,
                PhoneNumber = viewModel.PhoneNumber,
                NationalID = viewModel.NationalID,

            };
        }

        public static UserSignUpViewModel ToViewModel(this User user)
        {
            return new UserSignUpViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Picture = user.Picture,
                PhoneNumber = user.PhoneNumber,
                NationalID = user.NationalID,
            };
        }
	}
}
