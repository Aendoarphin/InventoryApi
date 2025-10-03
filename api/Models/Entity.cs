using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Entity
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        public Entity()
        {
            Id = 0;
            Name = "";
        }

        public Entity(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}