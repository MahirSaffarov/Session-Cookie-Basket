using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany.Data;
using OneToMany.Models;
using OneToMany.ViewModels;

namespace OneToMany.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Expert> experts = await _context.Experts.Where(m => !m.SoftDeleted).ToListAsync();
            ContactVM contactVM = new ContactVM()
            {
                Experts = experts  
            };
            return View(contactVM);
        }
    }
}
