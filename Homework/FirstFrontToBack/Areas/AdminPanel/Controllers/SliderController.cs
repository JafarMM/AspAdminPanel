using FirstFrontToBack.DataAccesLayer;
using FirstFrontToBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFrontToBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SliderController : Controller
    {
         

        private readonly AppDbContext _dbContext;

        public SliderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var sliderimages = _dbContext.SliderImages.ToList();
            return View(sliderimages);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var sliderimages = _dbContext.SliderImages.Find(id);

            if (sliderimages == null)
                return NotFound();

            return View(sliderimages);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var sliderimages = await _dbContext.SliderImages.FindAsync(id);

            if (sliderimages == null)
                return NotFound();

            return View(sliderimages);
        }

        public async Task<IActionResult> Update(int? id, SliderImage sliderImage)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
                return NotFound();

            if (id != sliderImage.Id)
                return BadRequest();

            var dbsliderimages = await _dbContext.SliderImages.FindAsync(id);
            if (dbsliderimages == null)
                return NotFound();

            var isExist = await _dbContext.SliderImages.AnyAsync(x => x.Image.ToLower() == sliderImage.Image.ToLower()
            && x.Id !=id);
            if (isExist)
            {
                ModelState.AddModelError("Image", "Bu adda image var");
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderImage sliderImage)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!sliderImage.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Shekil yuklemeyiniz rica olunur.....");
            }

            if(sliderImage.Photo.Length >= 2048 * 1000)
            {
                ModelState.AddModelError("Photo", "Shekliniz 2mb dan yuksekdir");
                    return View();
            }

            var filename = $"{Guid.NewGuid()}_{sliderImage.Photo.FileName}";
            var filepath = Path.Combine(filename);

            return Content(sliderImage.Photo.FileName);
        }
    }
}
