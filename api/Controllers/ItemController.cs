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
    public class ItemController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        public ItemController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("count")]
        public async Task<ActionResult<Item>> GetItemCount()
        {
            var itemCount = await _context.Items.CountAsync();
            return Ok(itemCount);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Item>> GetItemByKeyword(string keyword)
        {
            var matches = await _context.Items.Where(u =>
                u.id.ToString().ToLower().Contains(keyword) ||
                u.serial!.ToString().ToLower().Contains(keyword) ||
                u.description!.ToString().ToLower().Contains(keyword) ||
                u.branch!.ToString().ToLower().Contains(keyword) ||
                u.office!.ToString().ToLower().Contains(keyword) ||
                u.comments!.ToString().ToLower().Contains(keyword) ||
                u.purchaseDate.ToString()!.ToLower().Contains(keyword) ||
                u.replacementCost.ToString()!.ToLower().Contains(keyword)).ToListAsync();
            return Ok(matches);
        }

        [HttpPost]
        public async Task<IActionResult> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { item.id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }

            _context.Items.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Items.Any(i => i.id == id))
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
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }
    }
}