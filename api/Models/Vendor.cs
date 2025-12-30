using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Vendor
    {
        public required int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? ProductServiceArea { get; set; }
        public string? ContractOnFile { get; set; }
        public string? Critical { get; set; }
        public string? Comments { get; set; }

        public Vendor()
        {
            Id = 0;
            Name = null;
            Address = null;
            City = null;
            Phone = null;
            Fax = null;
            Contact = null;
            Email = null;
            Website = null;
            ProductServiceArea = null;
            ContractOnFile = null;
            Critical = null;
            Comments = null;
        }

        public Vendor(
            int Id,
            string? Name,
            string? Address,
            string? City,
            string? Phone,
            string? Fax,
            string? Contact,
            string? Email,
            string? Website,
            string? ProductServiceArea,
            string? ContractOnFile,
            string? Critical,
            string? Comments
        )
        {
            this.Id = Id;
            this.Name = Name;
            this.Address = Address;
            this.City = City;
            this.Phone = Phone;
            this.Fax = Fax;
            this.Contact = Contact;
            this.Email = Email;
            this.Website = Website;
            this.ProductServiceArea = ProductServiceArea;
            this.ContractOnFile = ContractOnFile;
            this.Critical = Critical;
            this.Comments = Comments;
        }
    }
}