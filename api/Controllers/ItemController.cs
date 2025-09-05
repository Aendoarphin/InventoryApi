using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Item;
using api.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _context.Items.ToList().Select(x => x.ToItemDto());
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item.ToItemDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateItemRequestDto itemDto)
        {
            var itemModel = itemDto.ToItemFromCreateDto();
            _context.Items.Add(itemModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { itemModel.id }, itemModel.ToItemDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateItemRequestDto updateDto)
        {
            var itemModel = _context.Items.FirstOrDefault(x => x.id == id);

            if (itemModel == null)
            {
                return NotFound();
            }

            itemModel.id = updateDto.id;
            itemModel.description = updateDto.description;
            itemModel.branch = updateDto.branch;
            itemModel.office = updateDto.office;
            _context.SaveChanges();
            return Ok(itemModel.ToItemDto());
        }
    }
}