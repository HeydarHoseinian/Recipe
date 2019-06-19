using Recipe.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.BOL
{
    public interface IRecipeDataProvider
    {
        IEnumerable<RecipeModel> GetAll(); 
        RecipeModel GetRecipe(Guid id);
        RecipeModel AddRecipe(RecipeModel model);
        RecipeModel UpdateRecipe(RecipeModel model);
        RecipeModel DeleteRecipe(Guid id);

    }
}
