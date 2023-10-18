using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
