
using AppRepository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AppRepository
{
    public class UserRepo : MainRepo<User>
    {
        UserManager<User> userManager;
        public UserRepo(ApplicationDBContext applicationDBContext, UserManager<User> _userManager) : base(applicationDBContext)
        {
            userManager = _userManager;
        }

        //public List<User> List()
        //{
        //    return GetList().Include(r => r.Role).ToList();
        //}

        public async Task<User> GetUserByID(string id)
        {
            //return GetList().Where(u => u.Id == id).Select(u => u.ToViewModel()).FirstOrDefault();
            return await userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> AddRoleToUser(string id, string role)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return await userManager.AddToRoleAsync(user, role);
            }
            return new IdentityResult();
        }
    }
}
