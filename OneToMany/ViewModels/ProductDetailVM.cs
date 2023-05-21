using OneToMany.Models;

namespace OneToMany.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductImage> Images { get; set; }
        public decimal DiscountPrice { get; set; }
        public byte Percent { get; set; }
    }
}
