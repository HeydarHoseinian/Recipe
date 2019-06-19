using Recipe.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Recipe.Definitions.ApplicationConstants;

namespace WebRecipeApp.Controllers
{
    public class RecipeController : Controller
    {
        public IRecipeDataProvider DataProvider { get; set; }
        public RecipeController()
        {
            DataProvider = BOLLayer.Instance.GetService<IRecipeDataProvider>();
        }
        // GET: Recipe
        public ActionResult Search(int searchType = RecipeSearchTypes.Search_GlobalRecipes)
        {            
            return View();
        }
    }
}