using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany.Data;
using OneToMany.Models;
using OneToMany.ViewModels;

namespace OneToMany.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _context.Products.Include(m=>m.Discount).Include(m => m.ProductImage).Include(m => m.Category).Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m=>m.Id == id);

            if (product == null) return NotFound();

            ProductDetailVM model = new()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category.Name,
                Description = product.Description,
                Price = product.Price,
                Images = product.ProductImage.ToList(),
                DiscountPrice = product.Price - (product.Price * product.Discount.Percent) / 100,
                Percent = product.Discount.Percent
            };
            return View(model);
        }
    }
}
