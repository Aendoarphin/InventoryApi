using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Item
    {
        public required int Id { get; set; }
        public string? Serial { get; set; }
        public string? Description { get; set; }
        public string? Branch { get; set; }
        public string? Office { get; set; }
        public string? Comments { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? ReplacementCost { get; set; }

        public Item()
        {
            Id = 0;
            Serial = null;
            Description = null;
            Branch = null;
            Office = null;
            Comments = null;
            PurchaseDate = null;
            ReplacementCost = null;
        }

        public Item(int Id, string? Serial, string? description, string? branch, string? office, string? Comments, DateTime? PurchaseDate, decimal? ReplacementCost)
        {
            this.Id = Id;
            this.Serial = Serial;
            this.Description = Description;
            this.Branch = Branch;
            this.Office = Office;
            this.Comments = Comments;
            this.PurchaseDate = PurchaseDate;
            this.ReplacementCost = ReplacementCost;
        }
    }
}