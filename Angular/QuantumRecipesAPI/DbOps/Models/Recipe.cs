using System;
using System.Collections.Generic;
using System.Text;

namespace DbOps.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
