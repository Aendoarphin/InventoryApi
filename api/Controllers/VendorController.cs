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
    public class VendorController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public VendorController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendors = await _context.Vendors.ToListAsync();
            return Ok(vendors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return vendor;
        }

        [HttpGet("count")]
        public async Task<ActionResult<Vendor>> GetVendorCount()
        {
            var vendorCount = await _context.Vendors.CountAsync();
            return Ok(vendorCount);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Vendor>> GetItemByKeyword(string keyword)
        {
            var matches = await _context.Vendors.Where(u =>
                u.Id.ToString().ToLower().Contains(keyword) ||
                u.Name!.ToLower().Contains(keyword) ||
                u.Address!.ToLower().Contains(keyword) ||
                u.City!.ToLower().Contains(keyword) ||
                u.Phone!.ToLower().Contains(keyword) ||
                u.Fax!.ToLower().Contains(keyword) ||
                u.Contact!.ToString()!.ToLower().Contains(keyword) ||
                u.Email!.ToString()!.ToLower().Contains(keyword) ||
                u.Website!.ToString()!.ToLower().Contains(keyword) ||
                u.ProductServiceArea!.ToString()!.ToLower().Contains(keyword) ||
                u.ContractOnFile!.ToString()!.ToLower().Contains(keyword) ||
                u.Critical!.ToString()!.ToLower().Contains(keyword) ||
                u.Comments!.ToString()!.ToLower().Contains(keyword)).ToListAsync();
            return Ok(matches);
        }

        [HttpPost]
        public async Task<IActionResult> PostVendor(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVendor), new { vendor.Id }, vendor);
        }

        [HttpPut]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }
            _context.Vendors.Entry(vendor).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Vendors.Any(v => v.Id == id))
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
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendor = _context.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return Ok(vendor);
        }
    }
}