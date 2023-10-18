using Microsoft.AspNetCore.Identity;
using Models;

namespace AppRepository.Models
{
    public static class RoleExtaintions
    {
        public static IdentityRole ToModel(this RoleViewModel veiwModel)
        {
            return new IdentityRole
            {
                Name = veiwModel.Name,
            };
        }
    }
}
