using AppRepository.Models;
using Microsoft.AspNetCore.Identity;
using Models;

namespace AppRepository
{
    public class AccountRepo : MainRepo<User>
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        public AccountRepo
            (ApplicationDBContext applicationDBContext, UserManager<User> _userManager, SignInManager<User> _signInManager)
            : base(applicationDBContext)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public async Task<IdentityResult> SignUp(UserSignUpViewModel userViewModel)
        {
            var user = userViewModel.ToModel();
            var result = await userManager.CreateAsync(user, userViewModel.Password);
            if (result.Succeeded)
            {
                result = await userManager.AddToRoleAsync(user, userViewModel.Role);
            }
            return result;
        }

        public async Task<SignInResult> SignIn(UserSignInViewModel user)
        {
            return await signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, true);
        }

        public async void SignOut()
        {
            await signInManager.SignOutAsync();
        }

        public UserSignUpViewModel GetUser(string id)
        {
            return GetList().Where(u => u.Id == id).Select(u => u.ToViewModel()).FirstOrDefault();
        }

        public async Task<IdentityResult> ChangePassword(UserChangePasswordViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(viewModel.Id);
            if (user != null)
                return await userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "User Not Found"
            });
        }

        public async Task<string> GetForgotPasswordCode(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                return code;
            }
            return string.Empty;
        }

        public async Task<IdentityResult> ForgotPassword(UserForgotPasswordViewModel viewModel)
        {
            var user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                return await userManager.ResetPasswordAsync(user, viewModel.Code, viewModel.NewPassword);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "User Not Found"
            });
        }

    }
}
