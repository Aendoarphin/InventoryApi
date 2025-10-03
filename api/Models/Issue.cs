using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Issue
    {
        public required int Id { get; set; }
        public required string Description { get; set; }
        public required int Entity { get; set; }
        public string? Severity { get; set; }
        public string? Status { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public Issue()
        {
            Id = 0;
            Description = "";
            Entity = 0;
            Severity = null;
            Status = null;
            ResolvedAt = null;
            CreatedAt = DateTime.Now;
        }

        public Issue(
            int Id,
            string Description,
            int Entity,
            string? Severity,
            string? Status,
            DateTime? ResolvedAt,
            DateTime CreatedAt
        )
        {
            this.Id = Id;
            this.Description = Description;
            this.Entity = Entity;
            this.Severity = Severity;
            this.Status = Status;
            this.ResolvedAt = ResolvedAt;
            this.CreatedAt = CreatedAt;
        }
    }
}