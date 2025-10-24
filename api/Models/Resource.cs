using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int CategoryId { get; set; }

        public Resource()
        {
            Name = "";
            CategoryId = 0;
        }
        
        public Resource(string Name, int CategoryId)
        {
            this.Name = Name;
            this.CategoryId = CategoryId;
        }
    }
}