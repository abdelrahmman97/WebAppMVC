
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ProductAttachment
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string ImageUrl { get; set; }

        public virtual Product Product { get; set; }
    }
}
