namespace OneToMany.Models
{
    public class Discount :BaseEntity
    {
        public string Name { get; set; }
        public byte Percent { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
