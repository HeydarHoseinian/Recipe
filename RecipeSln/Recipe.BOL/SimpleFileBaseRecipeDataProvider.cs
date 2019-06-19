using Recipe.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.BOL
{
    public class SimpleFileBaseRecipeDataProvider : IRecipeDataProvider
    {
        private List<Recipe.DataStructures.RecipeModel> InMemoryRecipes { get; set; }
        public SimpleFileBaseRecipeDataProvider()
        {
            this.DataDirectory = Path.GetFullPath(Path.Combine(Path.GetFileName(Assembly.GetExecutingAssembly().Location), "../Data"));
            this.DocumentsDirectory = Path.Combine(this.DataDirectory, "Documents");
            this.RecipesDirectory = Path.Combine(this.DataDirectory, "RecipesDirectory");

            if (!Directory.Exists(this.DataDirectory))
            {
                Directory.CreateDirectory(this.DataDirectory);
            }

            if (!Directory.Exists(this.DocumentsDirectory))
            {
                Directory.CreateDirectory(this.DocumentsDirectory);
            }

            if (!Directory.Exists(this.RecipesDirectory))
            {
                Directory.CreateDirectory(this.RecipesDirectory);
            }
            InMemoryRecipes = new List<DataStructures.RecipeModel>();
            LoadExistingRecipes();
            
        }
        private string GetRecipeFullFileName(Guid id)
        {
            return Path.Combine(this.RecipesDirectory, id.ToString());
        }

        private void LoadExistingRecipes()
        {
            var files =System.IO.Directory.GetFiles(this.RecipesDirectory, "*.json");
            foreach (var f in files)
            {
                var recipe = DecerializeRecipeFile<RecipeModel>(f);
                if (recipe != null)
                {
                    InMemoryRecipes.Add(recipe);

                }
            }
        }

        private T DecerializeRecipeFile<T>(string filePathName)
        {
            string fileContent = System.IO.File.ReadAllText(filePathName);
            return DecerializeRecipeString<T>(fileContent);
        }
        private T DecerializeRecipeString<T>(string content)
        {
            return content.FromJsonString<T>();

        }

        public IEnumerable<RecipeModel> GetAll()
        {
            return (from r in InMemoryRecipes select r).ToList();
        }

        public RecipeModel GetRecipe(Guid id)
        {
            return (from r in InMemoryRecipes where r.Id== id select r).FirstOrDefault();
        }

        public RecipeModel AddRecipe(RecipeModel model)
        {
            if (model != null)
            {
                var oldRecipe = GetRecipe(model.Id);
                if (oldRecipe != null)
                {
                    UpdateRecipe(model);
                }
                else
                {
                    
                    var modelData =model.ToJsonString();
                    string fileName = GetRecipeFullFileName(model.Id);
                    File.WriteAllText(fileName, modelData);
                    InMemoryRecipes.Add(model);
                }
            }
            return model;
        }


        public RecipeModel UpdateRecipe(RecipeModel model)
        {
            var oldRecipe = GetRecipe(model.Id);
            if (oldRecipe != null)
            {
                var modelData = model.ToJsonString();
                string fileName = GetRecipeFullFileName(model.Id);
                File.WriteAllText(fileName, modelData);
                lock (model.Id.ToString())
                {
                    InMemoryRecipes.Remove(oldRecipe);
                    InMemoryRecipes.Add(model);
                }

            }
            else
            {
                AddRecipe(model);
            }
            return model;

        }

        public RecipeModel DeleteRecipe(Guid id)
        {
            var recipe =GetRecipe(id);
            if (recipe != null)
            {
                InMemoryRecipes.Remove(recipe);
                string fileName = GetRecipeFullFileName(id);
                File.Delete(fileName);
                return recipe;
            }
            return null;
        }

        public string DataDirectory { get; }
        public string DocumentsDirectory { get; }
        public string RecipesDirectory { get; }
    }
}
