using FirstFrontToBack.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFrontToBack.DataAccesLayer
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }


        public DbSet<Slider> Slider { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<ExpertsTitle> ExpertsTitles { get; set; }
        public DbSet<Experts> Experts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bio> Bio { get; set; }
    }
}
