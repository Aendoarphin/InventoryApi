using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceCategoryController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ResourceCategoryController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var resourceCategorys = await _context.ResourceCategories.ToListAsync();
            return Ok(resourceCategorys);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceCategory>> GetResourceCategory(int id)
        {
            var resourceCategory = await _context.ResourceCategories.FindAsync(id);

            if (resourceCategory == null) return NotFound();

            return resourceCategory;
        }

        [HttpPost]
        public async Task<ActionResult> PostResourceCategory(ResourceCategory resourceCategory)
        {
            _context.ResourceCategories.Add(resourceCategory);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetResourceCategory), new { resourceCategory.Id }, resourceCategory);
        }

        [HttpPut]
        public async Task<ActionResult> PutResourceCategory(int id, ResourceCategory resourceCategory)
        {
            if (id != resourceCategory.Id) return BadRequest();

            _context.ResourceCategories.Entry(resourceCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ResourceCategories.Any(i => i.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteResourceCategory(int id)
        {
            var resourceCategory = _context.ResourceCategories.Find(id);
            if (resourceCategory == null) return NotFound();
            _context.ResourceCategories.Remove(resourceCategory);
            await _context.SaveChangesAsync();
            return Ok(resourceCategory);
        }
    }
}