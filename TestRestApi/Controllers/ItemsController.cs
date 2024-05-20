using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestApi.Data;
using TestRestApi.Data.Models;
using TestRestApi.DTO;

namespace TestRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public ItemsController(AppDbContext db)
        {
            _db = db;
        }
        private readonly AppDbContext _db;

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var items = await _db.Items.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne([FromRoute] int id)
        {
            var item = await _db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);

        }

        [HttpPost]
        public async Task<ActionResult> CreateItem([FromForm] CreateItemRequest dto)
        {
            using var stream = new MemoryStream();

            if (dto.Image != null)
            {

                await dto.Image.CopyToAsync(stream);
            }
            var item = new Item
            {
                Name = dto.Name,
                Price = dto.Price,
                Notes = dto.Notes,
                CategoryId = dto.CategoryId,
                Image = stream.ToArray()

            };
            await _db.Items.AddAsync(item);
            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem([FromRoute] int id, [FromForm] CreateItemRequest dto)
        {
            var item = await _db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var isCategoryExists = await _db.Categories.AnyAsync(cat => cat.Id == dto.CategoryId);
            if (!isCategoryExists)
            {
                return NotFound();
            }
            if (dto.Image != null)
            {
                using var stream = new MemoryStream();
                await dto.Image.CopyToAsync(stream);
                item.Image = stream.ToArray();
            }
            item.Name = dto.Name;
            item.Price = dto.Price;
            item.Notes = dto.Notes;
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult> GetAllCategoryItems([FromRoute] int id)
        {
            var items = await _db.Items.Where(tempItem => tempItem.CategoryId == id).ToListAsync();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategories([FromRoute] int id)
        {
            var item = await _db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _db.Items.Remove(item);
                _db.SaveChanges();

            }
            return Ok(true);
        }

    }
}
