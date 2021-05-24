using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFrontToBack.Models
{
    public class Slider
    {
        public int Id { get; set; }

        [Required,MaxLength(100)]
        public string Title { get; set; }

        [Required,MaxLength(300)]
        public string Description { get; set; }

        public string Logo { get; set; }
    }
}
