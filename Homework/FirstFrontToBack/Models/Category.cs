using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFrontToBack.Models
{
    public class Category
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Bu xana mütləq dolmalıdır."),MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
