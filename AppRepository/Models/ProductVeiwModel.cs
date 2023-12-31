﻿namespace AppRepository.Models
{
    public class ProductVeiwModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public List<string> Images { get; set; }

    }
}
