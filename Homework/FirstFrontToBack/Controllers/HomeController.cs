using FirstFrontToBack.DataAccesLayer;
using FirstFrontToBack.ViewModels;
using FirstFrontToBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FirstFrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("demo", "Test");
            //Response.Cookies.Append("demo1","Test1");

            var slider = _dbContext.Slider.FirstOrDefault();
            var sliderImages = _dbContext.SliderImages.ToList();
            var expertsTitle = _dbContext.ExpertsTitles.FirstOrDefault();
            var categories = _dbContext.Categories.ToList();
            //var products = _dbContext.Products.Include(x => x.Category).ToList();
         

            var homeViewModel = new HomeViewModel
            {
                Slider = slider,
                SliderImages = sliderImages,
                ExpertsTitle = expertsTitle,
                Categories = categories,
                //Products = products
                 

            };
            return View(homeViewModel);
        }
        public IActionResult AddToBasket(int? id)
        {
            if (id == null)
                return NotFound();
            var product = _dbContext.Products.First(x=> x.Id==id);
            if (product == null)
                return NotFound();


            List<BasketViewModel> productList;
            var basketCookie = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basketCookie))
            {
                productList = new List<BasketViewModel>();
            }
            else
            {
                productList = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketCookie);
            }

            var existProduct = productList.FirstOrDefault(x => x.Id == id);
            
            if (existProduct == null)
            {
                 
                productList.Add(new BasketViewModel { Id=product.Id});
            }
            else
            {
                existProduct.Count++;
            }

           
            var productJson = JsonConvert.SerializeObject(productList);
            Response.Cookies.Append("basket",productJson);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int? id)
        {
            if (id == null)
                return NotFound();
            var product = _dbContext.Products.First(x => x.Id == id);
            if (product == null)
                return NotFound();


            List<BasketViewModel> productList;
            var basketCookie = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basketCookie))
            {
                productList = new List<BasketViewModel>();
            }
            else
            {
                productList = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketCookie);
            }

            var existProduct = productList.FirstOrDefault(x => x.Id == id);
             

            if (existProduct.Count > 1)
            {
                existProduct.Count--;
            }
            else
            {
                productList.Remove(existProduct);
            }
            
            var productJson = JsonConvert.SerializeObject(productList);
            Response.Cookies.Append("basket", productJson);
            if (productList.Count == 0)
            {
                Response.Cookies.Delete("basket");
            }
            


            return RedirectToAction("Basket");
        }
        public IActionResult Basket()
        {
             
            var cookiebasket = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(cookiebasket))
                return View();
            var basketViewModels= JsonConvert.DeserializeObject<List<BasketViewModel>>(cookiebasket);
            var result = new List<BasketViewModel>();
            foreach (var basketViewModel in basketViewModels)
            {
                var dbProduct = _dbContext.Products.Find(basketViewModel.Id);
                if (dbProduct == null)
                    continue;
                basketViewModel.Price = dbProduct.Price;
                basketViewModel.Image = dbProduct.Image;
                basketViewModel.Name = dbProduct.Name;
                result.Add(basketViewModel);
                 
                
            }

            var basket = JsonConvert.SerializeObject(result);
            Response.Cookies.Append("basket", basket);
            var basketview = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);
            return View(basketview);
        }
    }
}
