﻿using FirstFrontToBack.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFrontToBack.ViewComponents
{
    public class ProductsViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ProductsViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count = 12)
        {
            var products = await _dbContext.Products.Include(x => x.Category)
                .OrderByDescending(x => x.Id).Take(count).ToListAsync();

            //var products = _dbContext.Products.ToList();

            //return View(await Task.FromResult(products));
            return View(products);
        }
    }
}
