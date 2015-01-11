using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Recipes.Models;
using Recipes.Domain;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        private readonly RecipeApplicationService service;

        public RecipesController(RecipeApplicationService service)
        {
            this.service = service;
        }

        [Route("create")]
        [HttpPost]
        public int Create([FromBody]CreateRecipeCommand cmd)
        {
            Console.WriteLine("creating recipe...{0}", cmd);
            var newId = service.When(cmd.ToInternal());
            return newId;
        }

        [HttpGet]
        public IEnumerable<NameId> Get()
        {
            return new [] { 
                new NameId{ id= 1, name= "Spaghetti Carbonara" },
                new NameId{ id= 2, name= "Risotto Milanese" },
                new NameId{ id= 3, name= "Saltinbocca all Romana" },
            };
        }

        [HttpGet("{id}")]
        public RecipeDto Get(int id)
        {
            return new RecipeDto{
                id= 1,
                name= "Spaghetti Carbonara",
                category= "Italian",
                level= "easy",
                cost= "low"
            };
        }
    }
}
