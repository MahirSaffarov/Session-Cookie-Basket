using OneToMany.Models;

namespace OneToMany.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Slider { get; set; }
        public SliderInfo SliderInfo { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Product{ get; set; }
        public IEnumerable<Expert> Experts { get; set; }
        public IEnumerable<Instagram> Instagrams { get; set; }


    }
}
