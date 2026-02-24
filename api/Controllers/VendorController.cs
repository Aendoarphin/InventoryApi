using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos;
using Api.Interfaces;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly IDataUtilityRepository _dataUtilRepo;
        public VendorController(IDataUtilityRepository dataUtilRepo)
        {
            _dataUtilRepo = dataUtilRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var vendors = await _dataUtilRepo.GetAll<Vendor>();
            return Ok(vendors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _dataUtilRepo.GetById<Vendor>(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        [HttpGet("count")]
        public async Task<ActionResult<Vendor>> GetVendorCount()
        {
            var vendorCount = await _dataUtilRepo.GetCount<Vendor>();
            return Ok(vendorCount);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendorByKeyword(string? keyword)
        {
            var matches = await _dataUtilRepo.Search<Vendor>(keyword ?? string.Empty);
            return Ok(matches);
        }

        [HttpGet("partial")]
        public async Task<ActionResult> GetPartialVendors()
        {
            var partialVendors = await _dataUtilRepo.GetPartial<Vendor>();
            return Ok(partialVendors);
        }

        [HttpGet("partial/count")]
        public async Task<ActionResult<int>> GetPartialVendorsCount()
        {
            var count = await _dataUtilRepo.GetPartialCount<Vendor>();
            return count;
        }

        [HttpGet("metrics")]
        public async Task<ActionResult<RecordMetricDto>> GetVendorMetrics()
        {
            var metrics = await _dataUtilRepo.GetMetrics<Vendor>();
            return metrics;
        }

        [HttpPost]
        public async Task<ActionResult> PostVendor(Vendor vendor)
        {
            var entity = await _dataUtilRepo.Create(vendor);

            if (entity == null) return BadRequest();

            return CreatedAtAction(nameof(GetVendor), new { entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id) return BadRequest();

            var updated = await _dataUtilRepo.Update(id, vendor);

            if (updated == null) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteVendor(int id)
        {
            var vendor = await _dataUtilRepo.Delete<Vendor>(id);
            return Ok(vendor);
        }
    }
}