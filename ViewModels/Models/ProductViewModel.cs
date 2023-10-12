using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMVC
{
    public class ProductViewModel
    {
        public int ID { get; set; }

        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Product Name reqiured")]
        [StringLength(50, ErrorMessage = "Must be more 5 letter", MinimumLength = 5)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "number")]
        [Required(ErrorMessage = "Valid Price reqiured")]
        public double Price { get; set; }

        [Column(TypeName = "number")]
        [Required(ErrorMessage = "Valid Quntity reqiured")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Product image required")]
        public IFormFileCollection Images { get; set; }
        public List<string> ProductAttachments { get; set; } = new List<string>();

    }
}
