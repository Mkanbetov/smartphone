namespace Smartphone.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Характеристики
        public int Memory { get; set; }          // GB
        public int Battery { get; set; }         // mAh
        public string? ImageUrl { get; set; }   // путь к фото

        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        public Brand? Brand { get; set; }
        public Category? Category { get; set; }


    }
}