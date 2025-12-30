using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int CategoryId { get; set; }
        public required int AccessLevelId { get; set; }
        public int? Active { get; set; }

        public Resource()
        {
            Name = "";
            CategoryId = 0;
            AccessLevelId = 0;
        }

        public Resource(string Name, int CategoryId, int AccessLevelId, int? Active)
        {
            this.Name = Name;
            this.CategoryId = CategoryId;
            this.AccessLevelId = AccessLevelId;
            this.Active = Active;
        }
    }
}