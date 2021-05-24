using FirstFrontToBack.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFrontToBack.ViewComponents
{
    public class ExpertsViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ExpertsViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var experts = await _dbContext.Experts.ToListAsync();

            return View(experts);
        }
    }
}
