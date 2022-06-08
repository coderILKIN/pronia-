using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SizeController : Controller
    {
        private readonly AppDbContext context;
        public SizeController(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Size> sizes = await context.Sizes.ToListAsync();
            return View(sizes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Size size)
        {
            //return Json(size);
            //return Content(size.Name);
            if (!ModelState.IsValid) return View();
            await context.Sizes.AddAsync(size);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            Size size = await context.Sizes.FirstOrDefaultAsync(s=>s.Id == id);
            if (size == null) return NotFound();
            return View(size);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Size size = await context.Sizes.FirstOrDefaultAsync(s=>s.Id == id);
            if (size == null) return NotFound();
            return View(size);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, Size size)
        {
            Size existedSize = await context.Sizes.FirstOrDefaultAsync(s=>s.Id==id);
            if (existedSize == null) return NotFound();
            if (id != size.Id) return BadRequest();
            existedSize.Name = size.Name;
            await context.SaveChangesAsync();
            //return Content(size.Name);
            //return Content(existedSize.Name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Size size = await context.Sizes.FirstOrDefaultAsync(s=>s.Id == id);
            if (size == null) return NotFound();
            return View(size);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            Size size = await context.Sizes.FirstOrDefaultAsync(s=>s.Id == id);
            if (size == null) return NotFound();
            context.Remove(size);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
