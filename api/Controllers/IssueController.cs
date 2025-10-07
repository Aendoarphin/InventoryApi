using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        public IssueController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var issues = await _context.Issues.ToListAsync();
            return Ok(issues);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Issue>> GetIssue(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null) return NotFound();
            return issue;
        }

        // Get all issues by severity level
        [HttpGet("severity")]
        public async Task<ActionResult<Issue>> GetIssueBySeverity(string severityLevel)
        {
            string[] severities = ["low", "medium", "high", "critical"];
            foreach (string s in severities)
            {
                if (s == severityLevel.ToLower())
                {
                    var issues = await _context.Issues.Where(i => i.Severity!.ToLower() == severityLevel.ToLower()).ToListAsync();
                    return Ok(issues);
                }
            }
            return NotFound();
        }

        [HttpGet("count")]
        public async Task<ActionResult<Issue>> GetCount()
        {
            var totalIssues = await _context.Issues.CountAsync();
            return Ok(totalIssues);
        }

        [HttpPost]
        public async Task<ActionResult<Issue>> PostIssue(Issue issue)
        {
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIssue), new { issue.Id }, issue);
        }

        [HttpPut]
        public async Task<ActionResult> PutIssue(int id, Issue issue)
        {
            if (id != issue.Id)
            {
                return BadRequest();
            }

            _context.Issues.Entry(issue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Issues.Any(i => i.Id == id))
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
        public async Task<ActionResult> DeleteVendor(int id)
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