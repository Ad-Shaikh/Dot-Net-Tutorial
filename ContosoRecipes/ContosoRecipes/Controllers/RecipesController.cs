using ContosoRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ContosoRecipes.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RecipesController : Controller
    {

        [HttpGet]
        public ActionResult GetRecipes([FromQuery]int count)
        {
            Recipe[] recipes =
            {
                new() {Title = "Oxtail"},
                new() {Title = "Curry Chicken"},
                new() {Title = "Dumplings"}
                
            };
                
            if (!recipes.Any())
            {
                return NotFound();
            }
            return Ok(recipes.Take(count));
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteRecipes(string id) 
        {
            bool badThings =  false;

            if (badThings)
            {
                return BadRequest();
            }
            
            return NoContent();

        }
        
    }
}
