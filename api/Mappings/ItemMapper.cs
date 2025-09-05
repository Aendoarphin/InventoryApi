using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Item;
using api.Models;

namespace api.Mappings
{
    public static class ItemMapper
    {
        public static ItemDto ToItemDto(this Item itemModel)
        {
            return new ItemDto
            {
                id = itemModel.id,
                description = itemModel.description,
                branch = itemModel.branch,
                office = itemModel.office,
            };
        }

        public static Item ToItemFromCreateDto(this CreateItemRequestDto itemDto)
        {
            return new Item
            {
                id = itemDto.id,
                description = itemDto.description,
                branch = itemDto.branch,
                office = itemDto.office,
            };
        }
    }

}