using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WAD.WebApp._7947.DAL.DTO
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}
