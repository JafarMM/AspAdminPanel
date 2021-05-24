using FirstFrontToBack.DataAccesLayer;
using FirstFrontToBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFrontToBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var category = _dbContext.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

         [HttpPost]
         [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.Categories.AnyAsync(x => x.Name.ToLower() == category.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name","Bu adda kateqoriya var");
            }

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
                return NotFound();

            if (id != category.Id)
                return BadRequest();

            var dbcategory = _dbContext.Categories.FindAsync(id);
            if (dbcategory == null)
                return NotFound();

            var isExist = await _dbContext.Categories.AnyAsync(x => x.Name.ToLower() == category.Name.ToLower()
           && x.Id != id );
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya var");
                return View();
            }
            return Json(category);
        }
    }
}
