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
    public class EmployeeResourceAssociationController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public EmployeeResourceAssociationController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var employeeResourceAssociations = await _context.EmployeeResourceAssociations.ToListAsync();
            return Ok(employeeResourceAssociations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResourceAssociation>> GetEmployeeResourceAssociation(int id)
        {
            var employeeResourceAssociation = await _context.EmployeeResourceAssociations.FindAsync(id);

            if (employeeResourceAssociation == null) return NotFound();

            return employeeResourceAssociation;
        }

        [HttpPost]
        public async Task<ActionResult> PostEmployeeResourceAssociation(EmployeeResourceAssociation employeeResourceAssociation)
        {
            _context.EmployeeResourceAssociations.Add(employeeResourceAssociation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployeeResourceAssociation), new { employeeResourceAssociation.Id }, employeeResourceAssociation);
        }

        [HttpPut]
        public async Task<ActionResult> PutEmployeeResourceAssociation(int id, EmployeeResourceAssociation employeeResourceAssociation)
        {
            if (id != employeeResourceAssociation.Id) return BadRequest();

            _context.EmployeeResourceAssociations.Entry(employeeResourceAssociation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.EmployeeResourceAssociations.Any(i => i.Id == id))
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
        public async Task<ActionResult> DeleteEmployeeResourceAssociation(int id)
        {
            var employeeResourceAssociation = _context.EmployeeResourceAssociations.Find(id);
            if (employeeResourceAssociation == null) return NotFound();
            _context.EmployeeResourceAssociations.Remove(employeeResourceAssociation);
            await _context.SaveChangesAsync();
            return Ok(employeeResourceAssociation);
        }
    }
}