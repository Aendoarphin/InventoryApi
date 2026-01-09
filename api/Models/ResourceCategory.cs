using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ResourceCategory
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Active { get; set; }

        public ResourceCategory()
        {
            Name = "";
            Active = 1;
        }

        public ResourceCategory(string Name, int Active)
        {
            this.Name = Name;
            this.Active = Active;
        }
    }
}