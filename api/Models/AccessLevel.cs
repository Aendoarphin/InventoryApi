using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class AccessLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Active { get; set; }

        public AccessLevel()
        {
            Name = "";
            Active = 1;
        }
        
        public AccessLevel(string Name, int Active)
        {
            this.Name = Name;
            this.Active = Active;
        }
    }
}