using DbOps.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace DbOps
{
    public class Operations
    {
        SQLiteConnection connection = new SQLiteConnection();

        public void OpenConnection()
        {
            //gets the path of the application
            var AppDir = System.Environment.CurrentDirectory;
            //sets connection string
            connection.ConnectionString = $@"Data Source = {AppDir}\DB\mydatabase.sqlite";
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public SQLiteCommand NewCommand(string query)
        {
            //creates a new command
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;
            command.CommandText = query;
            return command;
        }


        public DataTable NewAdapter(SQLiteCommand command)
        {
            //creates a new adapter
            //fills that adapter
            //returns the datatable associated
            //with that adapter
            DataSet dataSet = new DataSet();
            var adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dataSet, "table");
            return dataSet.Tables["table"];
        }

        public List<Recipe> GetRecipes()
        {
            //gets a recipe by its id
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                //gets recipe
                OpenConnection();
                var command = NewCommand("SELECT * FROM Recipe");
                var recipeTable = NewAdapter(command);

                if (recipeTable.Rows.Count > 0)
                {
                    for(int i = 0; i < recipeTable.Rows.Count; i++)
                    {
                        Recipe recipe = new Recipe();
                        recipe.Id = Convert.ToInt32(recipeTable.Rows[i]["id"].ToString());
                        recipe.Title = recipeTable.Rows[i]["title"].ToString();
                        recipe.Description = recipeTable.Rows[i]["description"].ToString();
                        recipe.Instructions = recipeTable.Rows[i]["instructions"].ToString();
                        //gets ingredients
                        recipe.Ingredients = GetIngredients(recipe);
                        recipes.Add(recipe);
                    }
                }
            }
            catch { }
            finally
            {
                CloseConnection();
            }
            return recipes;
        }

        public Recipe GetRecipe(int id)
        {
            //gets a recipe by its id
            Recipe recipe = new Recipe();
            try
            {
                //gets recipe
                OpenConnection();
                var command = NewCommand("SELECT * FROM Recipe WHERE id = @id");
                command.Parameters.AddWithValue("@id", id);
                var recipeTable = NewAdapter(command);

                if (recipeTable.Rows.Count > 0)
                {
                    recipe.Id = Convert.ToInt32(recipeTable.Rows[0]["id"].ToString());
                    recipe.Title = recipeTable.Rows[0]["title"].ToString();
                    recipe.Description = recipeTable.Rows[0]["description"].ToString();
                    recipe.Instructions = recipeTable.Rows[0]["instructions"].ToString();
                    //gets ingredients
                    recipe.Ingredients = GetIngredients(recipe);
                }
            }
            catch { }
            finally
            {
                CloseConnection();
            }
            return recipe;
        }

        public void AddRecipe(Recipe recipe)
        {
            //adds a recipe
            try
            {
                
                OpenConnection();
                //get next id availible in table
                var id = GetNextId("recipe");
                //inserts the recipe
                var command = NewCommand("INSERT INTO Recipe (id, title, description, instructions) VALUES(@id, @title, @description, @instructions)");
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@title", recipe.Title);
                command.Parameters.AddWithValue("@description", recipe.Description);
                command.Parameters.AddWithValue("@instructions", recipe.Instructions);
                command.ExecuteNonQuery();

                //inserts new ingredients
                recipe.Ingredients.ForEach(i =>
                {
                    command = NewCommand("INSERT INTO Ingredient (id, recipeid, name, amount) VALUES(@id, @recipeid, @name, @amount)");
                    command.Parameters.AddWithValue("@id", GetNextId("ingredient"));
                    command.Parameters.AddWithValue("@recipeid", id);
                    command.Parameters.AddWithValue("@name", i.Name);
                    command.Parameters.AddWithValue("@amount", i.Amount);
                    command.ExecuteNonQuery();
                });
            }
            catch { }
            finally
            {
                CloseConnection();
            }
        }

        public List<int> GetRecipeIngredientIds(Recipe recipe)
        {
            List<int> ingredientIds = new List<int>();
            //gets old ingredient ids
            recipe.Ingredients.ForEach(i =>
            {
                ingredientIds.Add(i.Id);
            });
            return ingredientIds;
        }


        public void UpdateRecipe(Recipe recipe)
        {
            try
            {
                OpenConnection();
                //updates recipe
                var command = NewCommand("UPDATE Recipe SET Title = @title, Description = @description, Instructions = @instructions WHERE Id = @id");
                command.Parameters.AddWithValue("@title", recipe.Title);
                command.Parameters.AddWithValue("@description", recipe.Description);
                command.Parameters.AddWithValue("@instructions", recipe.Instructions);
                command.Parameters.AddWithValue("@id", recipe.Id);
                command.ExecuteNonQuery();
                CloseConnection();
                //update ingredients
                UpdateIngredients(recipe);
            }
            catch { }
            finally
            {
                CloseConnection();
            }
            
        }

        private void UpdateIngredients(Recipe recipe)
        {
            //list of ids that are in both old and new recipe
            List<int> sameIds = new List<int>();
            //gets all old ingredients
            var oldRecipe = GetRecipe(recipe.Id);

            //gets old recipe ingredient ids
            var listOldIds = GetRecipeIngredientIds(oldRecipe);
            //gets new recipe ids
            var listNewIds = GetRecipeIngredientIds(recipe);

            //checks to see which ingredients need to be deleted
            listOldIds.ForEach(o =>
            {
                //if old ingredient isnt in the new list of ingredients
                if (!listNewIds.Contains(o))
                {
                    //deletes ingredient
                    deleteIngredient(o);
                }
            });

            //loops through new recipe ids
            recipe.Ingredients.ForEach(i =>
            {
                //if the id is 0 the ingredient is new
                //checks if ingredient is new
                if (i.Id == 0)
                {
                    //if new ingredient add that ingredient

                    addIngredient(i,recipe.Id);


                }
                //if ingredient is found in both old and new list of recipes
                //it gets updated with the new ingredient information
                if (listOldIds.Contains(i.Id))
                {
                    //updates the current ingredient
                    //with the new ingredient information
                    updateIngredient(i);
                }
            });

         
        }

        private void updateIngredient(Ingredient i)
        {
            try
            {
                OpenConnection();
                //updates ingredient
                var command = NewCommand("UPDATE Ingredient SET Name = @name, amount = @amount WHERE Id = @id");
                command.Parameters.AddWithValue("@name", i.Name);
                command.Parameters.AddWithValue("@amount", i.Amount);
                command.Parameters.AddWithValue("@id", i.Id);
                command.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                CloseConnection();
            }
        }

        private void addIngredient(Ingredient ingredient,int recipeId)
        {
            //adds ingredient
            try
            {
                    OpenConnection();
                    //inserts the recipe
                    var command = NewCommand("INSERT INTO Ingredient (id, recipeid, name, amount) VALUES(@id, @recipeid, @name, @amount)");
                    command.Parameters.AddWithValue("@id", GetNextId("ingredient"));
                    command.Parameters.AddWithValue("@recipeid", recipeId);
                    command.Parameters.AddWithValue("@name", ingredient.Name);
                    command.Parameters.AddWithValue("@amount", ingredient.Amount);
                    command.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                CloseConnection();
            }
        }

        private void deleteIngredient(int id)
        {
            try
            {
                OpenConnection();
                //deletes ingredient from the recipe
                var command = NewCommand("DELETE FROM Ingredient WHERE Id = @id");
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                CloseConnection();
            }
        }

        public void DeleteRecipe(int id)
        {
            try
            {
                OpenConnection();
                //deletes all ingredients of recipe
                var command = NewCommand("DELETE FROM Ingredient WHERE RecipeId = @id");
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                //deletes recipe
                command = NewCommand("DELETE FROM Recipe WHERE Id = @id");
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                CloseConnection();
            }
        }

        public List<Ingredient> GetIngredients(Recipe recipe)
        {
            //get recipe's Ingredients
            var command = NewCommand("SELECT * FROM Ingredient WHERE RecipeId = @id");
            command.Parameters.AddWithValue("@id", recipe.Id);
            var ingredientTable = NewAdapter(command);
            if (ingredientTable.Rows.Count > 0)
            {
                //gets ingredients for the recipe
                for (int i = 0; i < ingredientTable.Rows.Count; i++)
                {
                    Ingredient ingredient = new Ingredient();
                    ingredient.Id = Convert.ToInt32(ingredientTable.Rows[i]["id"].ToString());
                    ingredient.RecipeId = recipe.Id;
                    ingredient.Name = ingredientTable.Rows[i]["name"].ToString();
                    ingredient.Amount = ingredientTable.Rows[i]["amount"].ToString();
                    recipe.Ingredients.Add(ingredient);
                }
            }
            return recipe.Ingredients;
        }

        private int GetNextId(string table)
        {
            try
            {
                //gets max id of a table
                //then returns the next id availible
                string query = $"Select Max(Id) From {table}";
                var command = NewCommand(query);
                var dbTable = NewAdapter(command);
                if (dbTable.Rows.Count > 0)
                {
                    string tableData = dbTable.Rows[0][0].ToString();

                    if (!String.IsNullOrEmpty(tableData))
                    {
                        return Convert.ToInt32(tableData) + 1;
                    }
                }
            }
            catch { return 0; }
            return 1;
        }
    }
}
