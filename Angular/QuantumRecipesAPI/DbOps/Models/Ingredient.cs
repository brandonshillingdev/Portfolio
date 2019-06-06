using System;
using System.Collections.Generic;
using System.Text;

namespace DbOps.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
        }

        public Ingredient(int id, int recipeId, string name, string amount)
        {
            Id = id;
            RecipeId = recipeId;
            Name = name;
            Amount = amount;
        }

        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
    }
}
