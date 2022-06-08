using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;
using Pronia.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;


        public HomeController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM model = new HomeVM
            {
                Sliders = await context.Sliders.OrderBy(s=>s.Order).ToListAsync(),
                Plants = await context.Plants.Include(p=> p.PlantImages).ToListAsync()
            };
            return View(model);
        }
    }
}
