using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AccessLevelController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public AccessLevelController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var accessLevels = await _context.AccessLevels.ToListAsync();
            return Ok(accessLevels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccessLevel>> GetAccessLevel(int id)
        {
            var accessLevel = await _context.AccessLevels.FindAsync(id);

            if (accessLevel == null) return NotFound();

            return accessLevel;
        }

        [HttpPost]
        public async Task<ActionResult> PostAccessLevel(AccessLevel accessLevel)
        {
            _context.AccessLevels.Add(accessLevel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAccessLevel), new { accessLevel.Id }, accessLevel);
        }

        [HttpPut]
        public async Task<ActionResult> PutAccessLevel(int id, AccessLevel accessLevel)
        {
            if (id != accessLevel.Id) return BadRequest();

            _context.AccessLevels.Entry(accessLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.AccessLevels.Any(i => i.Id == id))
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
        public async Task<ActionResult> DeleteAccessLevel(int id)
        {
            var accessLevel = _context.AccessLevels.Find(id);
            if (accessLevel == null) return NotFound();
            _context.AccessLevels.Remove(accessLevel);
            await _context.SaveChangesAsync();
            return Ok(accessLevel);
        }
    }
}