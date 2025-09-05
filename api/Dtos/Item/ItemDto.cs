using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Item
{
    public class ItemDto
    {
        public int id { get; set; }
        // public string? serial { get; set; }
        public string? description { get; set; }
        public string? branch { get; set; }
        public string? office { get; set; }
        // public string? comments { get; set; }
        // public DateTime? purchaseDate { get; set; }
        // public decimal? replacementCost { get; set; }
    }
}