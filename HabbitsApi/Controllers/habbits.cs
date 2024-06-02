using HabbitsApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace HabbitsApi.Controllers
{
    
    public class Habbits : ControllerBase
    {
        EleniContext _context;
        public Habbits(EleniContext eleniContext)
        {
            _context = eleniContext;
        }
        // GET: habbits/Details/5
        [HttpGet]
        [Route("/habbits")]
        public ActionResult Load()
        {
            List<Recipe>? recipes = new List<Recipe>();

            recipes = _context.Recipes != null ?
                _context.Recipes.ToList() : null;

            return Ok(recipes);
        }

     
        // POST: habbits/Create
        [HttpPost]
        [Route("/habbits")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert([FromBody] Recipe recipe)
        {
            if (recipe == null)
                return BadRequest();

            try
            {
                    _context.Recipes.Add(recipe);
                    await _context.SaveChangesAsync();
                
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        //[ValidateAntiForgeryToken]
        [Route("/habbits")]
        public async Task<ActionResult> Edit([FromBody]Recipe recipe)
        {
            if (recipe == null)
                return BadRequest();

            try
            {
                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();

            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: habbits/Delete/5
        [HttpDelete]
        //[ValidateAntiForgeryToken]
        [Route("/habbits")]

        public async Task<ActionResult> Delete([FromQuery]int id)
        {
            try
            {
                var recipe = _context.Recipes.FirstOrDefault(x => x.Id == id);
                if (recipe == null)
                    return NotFound();

                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
