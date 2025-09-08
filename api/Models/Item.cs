using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Item
    {
        public required int id { get; set; }
        public string? serial { get; set; }
        public string? description { get; set; }
        public string? branch { get; set; }
        public string? office { get; set; }
        public string? comments { get; set; }
        public DateTime? purchaseDate { get; set; }
        public decimal? replacementCost { get; set; }

        public Item()
        {
            id = 0;
            serial = null;
            description = null;
            branch = null;
            office = null;
            comments = null;
            purchaseDate = null;
            replacementCost = null;
        }

        public Item(int id, string? serial, string? description, string? branch, string? office, string? comments, DateTime? purchaseDate, decimal? replacementCost)
        {
            this.id = id;
            this.serial = serial;
            this.description = description;
            this.branch = branch;
            this.office = office;
            this.comments = comments;
            this.purchaseDate = purchaseDate;
            this.replacementCost = replacementCost;
        }
    }
}