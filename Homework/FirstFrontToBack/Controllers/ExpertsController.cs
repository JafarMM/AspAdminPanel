using FirstFrontToBack.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFrontToBack.Controllers
{
    public class ExpertsController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ExpertsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            var experts = _dbContext.Experts.FirstOrDefault(x => x.Id == id);

            return View(experts);
        }
    }
}
