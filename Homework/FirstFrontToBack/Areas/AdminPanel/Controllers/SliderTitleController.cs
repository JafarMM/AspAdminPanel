using FirstFrontToBack.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFrontToBack.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SliderTitleController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SliderTitleController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var slidertitle = _dbContext.Slider.FirstOrDefault();

            return View();
        }
    }
}
