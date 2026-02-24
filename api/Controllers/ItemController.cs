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
    public class ItemController : ControllerBase
    {
        private readonly IDataUtilityRepository _dataUtilRepo;
        public ItemController(IDataUtilityRepository dataUtilRepo)
        {
            _dataUtilRepo = dataUtilRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var items = await _dataUtilRepo.GetAll<Item>();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _dataUtilRepo.GetById<Item>(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("count")]
        public async Task<ActionResult<Item>> GetItemCount()
        {
            var itemCount = await _dataUtilRepo.GetCount<Item>();
            return Ok(itemCount);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemByKeyword(string? keyword)
        {
            var matches = await _dataUtilRepo.Search<Item>(keyword ?? string.Empty);
            return Ok(matches);
        }

        [HttpGet("partial")]
        public async Task<ActionResult> GetPartialItems()
        {
            var partialItems = await _dataUtilRepo.GetPartial<Item>();
            return Ok(partialItems);
        }

        [HttpGet("partial/count")]
        public async Task<ActionResult<int>> GetPartialItemsCount()
        {
            var count = await _dataUtilRepo.GetPartialCount<Item>();
            return count;
        }

        [HttpGet("metrics")]
        public async Task<ActionResult<RecordMetricDto>> GetItemMetrics()
        {
            var metrics = await _dataUtilRepo.GetMetrics<Item>();
            return metrics;
        }

        [HttpPost]
        public async Task<ActionResult> PostItem(Item item)
        {
            var entity = await _dataUtilRepo.Create(item);

            if (entity == null) return BadRequest();

            return CreatedAtAction(nameof(GetItem), new { entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id) return BadRequest();

            var updated = await _dataUtilRepo.Update(id, item);

            if (updated == null) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var item = await _dataUtilRepo.Delete<Item>(id);
            return Ok(item);
        }
    }
}