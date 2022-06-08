using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;

        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await context.Sliders.ToListAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();

            if (slider.Photo != null)
            {
                if (!slider.Photo.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("Photo", "Please choose image file");
                    return View();
                }
                if (slider.Photo.Length > 1024 * 1024)
                {
                    ModelState.AddModelError("Photo", "Please choose image file which size under 1 MB");
                    return View();
                }

                string FileName = slider.Photo.FileName;
                string path = Path.Combine(env.WebRootPath,"assets","images","website-images");
                string FullPath = Path.Combine(path,FileName);
                using(FileStream stream = new FileStream(FullPath,FileMode.Create))
                {
                    await slider.Photo.CopyToAsync(stream);
                }
                slider.Image = FileName;
                await context.Sliders.AddAsync(slider);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Photo", "Please choose file");
                return View();
            }
           
           
        }
    }
}
