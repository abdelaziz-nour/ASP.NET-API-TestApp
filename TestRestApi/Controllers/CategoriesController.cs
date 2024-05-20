using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestApi.Data;
using TestRestApi.Data.Models;

namespace TestRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }
        private readonly AppDbContext _db;

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var cats = await _db.Categories.ToListAsync();
            return Ok(cats);
        }

        [HttpPost]
        public async Task<IActionResult> addCategories(string category)
        {
            Category c = new() { Name = category };
            await _db.Categories.AddAsync(c);
            _db.SaveChanges();
            return Ok(c);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateCategories([FromRoute] int id, string categoryName)
        {
            var c = await _db.Categories.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }
            c.Name = categoryName;
            _db.SaveChanges();
            return Ok(c);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCategories([FromRoute] int id)
        {
            var c = await _db.Categories.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }
            else
            {
                _db.Categories.Remove(c);
            }
            _db.SaveChanges();
            return Ok(true);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> patchCategories([FromRoute] int id, [FromBody] JsonPatchDocument<Category> jsonPatch)
        {
            var c = await _db.Categories.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }
            else
            {
                jsonPatch.ApplyTo(c);
                await _db.SaveChangesAsync();
                return Ok(c);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getCategory([FromRoute] int id)
        {
            var c = await _db.Categories.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(c);
            }
        }



    }
}
