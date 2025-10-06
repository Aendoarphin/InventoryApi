using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var matches = await _context.Items.Where(i =>
                i.Id.ToString().ToLower().Contains(keyword) ||
                i.Serial!.ToString().ToLower().Contains(keyword) ||
                i.Description!.ToString().ToLower().Contains(keyword) ||
                i.Branch!.ToString().ToLower().Contains(keyword) ||
                i.Office!.ToString().ToLower().Contains(keyword) ||
                i.Comments!.ToString().ToLower().Contains(keyword) ||
                i.PurchaseDate.ToString()!.ToLower().Contains(keyword) ||
                i.ReplacementCost.ToString()!.ToLower().Contains(keyword)).ToListAsync();
            return Ok(matches);
        }

        [HttpPost]
        public async Task<IActionResult> PostItem(Item item)
        {
            _context.Items.Add(item);

            

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id)
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
                if (!_context.Items.Any(i => i.Id == id))
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