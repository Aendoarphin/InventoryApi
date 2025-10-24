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
    public class ResourceController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ResourceController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var resources = await _context.Resources.ToListAsync();
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(int id)
        {
            var resource = await _context.Resources.FindAsync(id);

            if (resource == null) return NotFound();

            return resource;
        }

        [HttpPost]
        public async Task<ActionResult> PostResource(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetResource), new { resource.Id }, resource);
        }

        [HttpPut]
        public async Task<ActionResult> PutResource(int id, Resource resource)
        {
                if (id != resource.Id) return BadRequest();

                _context.Resources.Entry(resource).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Resources.Any(i => i.Id == id))
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
        public async Task<ActionResult> DeleteResource(int id)
        {
            var resource = _context.Resources.Find(id);
            if (resource == null) return NotFound();
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return Ok(resource);
        }
    }
}