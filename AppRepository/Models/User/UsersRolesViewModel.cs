using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRepository.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public List<UserRole> UserRoles { get; set;}
    }

    public class UserRole
    {
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
