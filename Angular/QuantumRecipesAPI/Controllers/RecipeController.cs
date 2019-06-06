using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbOps;
using DbOps.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuantumRecipesAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        Operations ops = new Operations();

        // GET api/Recipe/GetRecipe
        [HttpGet]
        public ActionResult<Recipe> GetRecipe(int id)
        {
            return ops.GetRecipe(id);
        }

        // GET api/Recipe/GetRecipes 
        [HttpGet]
        public ActionResult<List<Recipe>> GetRecipes()
        {
            return ops.GetRecipes();
        }

        // Post api/Recipe/AddRecipe
        [HttpPost]
        public void AddRecipe(Recipe recipe)
        {
            ops.AddRecipe(recipe);
        }

        // GET api/Recipe/DeleteRecipe
        [HttpDelete]
        public void DeleteRecipe(int id)
        {
            ops.DeleteRecipe(id);
        }

        // GET api/Recipe/UpdateRecipe
        [HttpPut]
        public void UpdateRecipe(Recipe recipe)
        {
            ops.UpdateRecipe(recipe);
        }
    }
}
