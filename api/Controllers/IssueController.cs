using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using api.Data;
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

        // Get all issues
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var issues = await _context.Issues.ToListAsync();
            return Ok(issues);
        }

        // Get issue by ID
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

        // Get total issues  
        [HttpGet("count")]
        public async Task<ActionResult<Issue>> GetCount()
        {
            var totalIssues = await _context.Issues.CountAsync();
            return Ok(totalIssues);
        }

        // Create new issue
        [HttpPost]
        public async Task<ActionResult<Issue>> PostIssue(Issue issue)
        {
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIssue), new { issue.Id }, issue);
        }

        // Update issue
        [HttpPut]
        public async Task<IActionResult> PutIssue(int id, Issue issue)
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
    }
}