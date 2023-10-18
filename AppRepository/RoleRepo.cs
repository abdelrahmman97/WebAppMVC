using AppRepository.Models;
using Microsoft.AspNetCore.Identity;
using Models;

namespace AppRepository
{
    public class RoleRepo : MainRepo<IdentityRole>
    {
        RoleManager<IdentityRole> roleManager;
        public RoleRepo(ApplicationDBContext applicationDBContext, RoleManager<IdentityRole> _roleManager)
            : base(applicationDBContext)
        {
            roleManager = _roleManager;
        }

        public async Task<IdentityResult> Create(RoleViewModel role)
        {
            return await roleManager.CreateAsync(role.ToModel());
        }

        public IdentityRole GetRole(string id){
            return GetList().Where(r => r.Id == id).FirstOrDefault();
        }
    }
}
