using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WAD.WebApp._7947.DAL.DTO
{
    public class Pizza
    {
        public int PizzaId { get; set; }

        [Required]
        [MinLength(4)]
        public string PizzaName { get; set; }

        [Required]
        public decimal Price { get; set; }


        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public PizzaSize Size { get; set; }

        [Display(Name = "Pizza Photo")]
        public byte[] PizzaBinPhoto { get; set; }

        [Display(Name = "Photo")]
        [DataType(DataType.Upload)]
        [NotMapped]
        public IFormFile SteakPhoto { get; set; }
    }
    public enum PizzaSize
    {
        Small,
        Medium,
        Large
    }
}
