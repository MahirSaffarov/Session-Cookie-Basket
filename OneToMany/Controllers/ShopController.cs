using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;
using OneToMany.Data;
using OneToMany.Models;
using OneToMany.ViewModels;

namespace OneToMany.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        public ShopController(AppDbContext context,IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products.Include(m => m.ProductImage).Where(m => !m.SoftDeleted).Take(4).ToListAsync();
            int count = await _context.Products.Where(m => !m.SoftDeleted).CountAsync();
            ViewBag.productCount = count;
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> ShowMoreOrLess(int skip)
        {
            IEnumerable<Product> products = await _context.Products.Include(m => m.ProductImage).Where(m => !m.SoftDeleted).Skip(skip).Take(4).ToListAsync();

            return PartialView("_ProductsPartial",products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if(id is null) return BadRequest();

            Product products = await _context.Products.FindAsync(id);

            if (products == null) return NotFound();

            List<BasketVM> basket = GetbasketDatas();

            AddProductToBasket(basket, products);

            return RedirectToAction(nameof(Index));
        }
        private List<BasketVM> GetbasketDatas()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            return basket;
        }
        private void AddProductToBasket(List<BasketVM> basket, Product products)
        {
            BasketVM existProduct = basket.FirstOrDefault(m => m.Id == products.Id);

            if (existProduct == null)
            {
                basket.Add(new BasketVM
                {
                    Id = products.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }
    }
}
