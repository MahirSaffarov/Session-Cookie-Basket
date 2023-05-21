using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OneToMany.Data;
using OneToMany.ViewModels;

namespace OneToMany.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        public CartController(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }
        public async Task<IActionResult> Index()
        {
            List<BasketDetailVM> basketList = new();

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

                foreach (var item in basketDatas)
                {
                    var dbProduct = await _context.Products.Include(m=>m.ProductImage).FirstOrDefaultAsync(m=>m.Id == item.Id);

                    if(dbProduct != null)
                    {
                        BasketDetailVM basketDetail = new()
                        {
                            Id = dbProduct.Id,
                            Name = dbProduct.Name,
                            Image = dbProduct.ProductImage.Where(m => m.IsMain).FirstOrDefault().Image,
                            Count = item.Count,
                            Price = dbProduct.Price,
                            TotalPrice = item.Count * dbProduct.Price
                        };
                        basketList.Add(basketDetail);
                    }
                }
            }

            return View(basketList);
        }
        public IActionResult Delete(int id)
        {
            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

                var productToRemove = basketDatas.FirstOrDefault(m => m.Id == id);

                if (productToRemove != null)
                {
                    basketDatas.Remove(productToRemove);

                    _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));
                }
            }

            return RedirectToAction("Index");
        }

    }
}
