using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{

    public class Item
    {
        // Properties
        public int id { get; set; }
        public string? serial { get; set; }
        public string? description { get; set; }
        public string? branch { get; set; }
        public string? office { get; set; }
        public string? comments { get; set; }
        public DateTime? purchaseDate { get; set; }
        public decimal? replacementCost { get; set; }

        // Constructors
        public Item()
        {
            id = 0;
            serial = "";
            description = "";
            branch = "";
            office = "";
            comments = "";
            purchaseDate = new DateTime();
            replacementCost = 0.00m;
        }

        public Item(
            int id,
            string? serial = null,
            string? description = null,
            string? branch = null,
            string? office = null,
            string? comments = null,
            DateTime? purchaseDate = null,
            decimal? replacementCost = null
        )
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
