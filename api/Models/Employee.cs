using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string First { get; set; }
        public required string Last { get; set; }
        public required string Branch { get; set; }
        public required string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required DateTime Created { get; set; }

        public Employee()
        {
            First = "";
            Last = "";
            Branch = "";
            JobTitle = "";
            StartDate = DateTime.Now;
            EndDate = null;
            Created = DateTime.Now;
        }

        public Employee(string First, string Last, string Branch, string JobTitle, DateTime StartDate, DateTime? EndDate)
        {
            this.First = First;
            this.Last = Last;
            this.Branch = Branch;
            this.JobTitle = JobTitle;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            Created = DateTime.Now;
        }
    }
}