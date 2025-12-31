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
    public class DeviceController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public DeviceController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var devices = await _context.Devices.ToListAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null) return NotFound();

            return device;
        }

        [HttpPost]
        public async Task<ActionResult> PostDevice(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDevice), new { device.Id }, device);
        }

        [HttpPut]
        public async Task<ActionResult> PutDevice(int id, Device device)
        {
            if (id != device.Id) return BadRequest();
            _context.Devices.Entry(device).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Devices.Any(i => i.Id == id))
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
        public async Task<ActionResult> DeleteDevice(int id)
        {
            var device = _context.Devices.Find(id);
            if (device == null) return NotFound();
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return Ok(device);
        }
    }
}