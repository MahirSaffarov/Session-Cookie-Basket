using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany.Data;
using OneToMany.Models;
using OneToMany.ViewModels;
using System.Diagnostics;

namespace OneToMany.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders= await _context.Sliders.Where(m=>!m.SoftDeleted).ToListAsync();
            SliderInfo sliderInfo = await _context.SliderInfos.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();
            IEnumerable<Blog> blogs = await _context.Blogs.Where(m=>!m.SoftDeleted).OrderByDescending(m=>m.Id).Take(3).ToListAsync();
            IEnumerable<Category> categories = await _context.Categories.Where(m => !m.SoftDeleted).ToListAsync();
            IEnumerable<Product> products = await _context.Products.Include(m=>m.ProductImage).Where(m => !m.SoftDeleted).ToListAsync();
            IEnumerable<Expert> experts = await _context.Experts.Where(m => !m.SoftDeleted).ToListAsync();
            IEnumerable<Instagram> instagrams = await _context.Instagrams.Where(m => !m.SoftDeleted).ToListAsync();

            HomeVM model = new HomeVM()
            {
                Slider = sliders,
                SliderInfo = sliderInfo,
                Blogs = blogs,
                Categories = categories,
                Product = products,
                Experts = experts,
                Instagrams = instagrams
            };

            return View(model);
        }
    }
}