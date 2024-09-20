namespace shop.WebApi.Models
{
    public class Product
    {
        public int Id { get; set; } // Clave primaria
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}