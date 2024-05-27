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
        public ActionResult Load()
        {
            List<Recipe>? recipes = new List<Recipe>();

            recipes = _context.Recipes != null ?
                _context.Recipes.ToList() : null;

            return Ok(recipes);
        }

     
        // POST: habbits/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert(Recipe recipe)
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Recipe recipe)
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
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
