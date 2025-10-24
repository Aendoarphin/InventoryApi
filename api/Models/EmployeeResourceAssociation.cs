using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class EmployeeResourceAssociation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required int ResourceId { get; set; }
        public required int EmployeeId { get; set; }
        public required DateTime Created { get; set; }

        public EmployeeResourceAssociation()
        {
            ResourceId = 0;
            EmployeeId = 0;
            Created = DateTime.Now;
        }

        public EmployeeResourceAssociation(int ResourceId, int EmployeeId)
        {
            this.ResourceId = ResourceId;
            this.EmployeeId = EmployeeId;
        }
    }
}