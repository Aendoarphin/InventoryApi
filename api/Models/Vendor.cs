using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Vendor
    {
        public required int id { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? phone { get; set; }
        public string? fax { get; set; }
        public string? contact { get; set; }
        public string? email { get; set; }
        public string? website { get; set; }
        public string? productServiceArea { get; set; }
        public string? contractOnFile { get; set; }
        public string? critical { get; set; }
        public string? comments { get; set; }

        public Vendor()
        {
            id = 0;
            name = null;
            address = null;
            city = null;
            phone = null;
            fax = null;
            contact = null;
            email = null;
            website = null;
            productServiceArea = null;
            contractOnFile = null;
            critical = null;
            comments = null;
        }

        public Vendor(
            int id,
            string? name,
            string? address,
            string? city,
            string? phone,
            string? fax,
            string? contact,
            string? email,
            string? website,
            string? productServiceArea,
            string? contractOnFile,
            string? critical,
            string? comments
        )
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.city = city;
            this.phone = phone;
            this.fax = fax;
            this.contact = contact;
            this.email = email;
            this.website = website;
            this.productServiceArea = productServiceArea;
            this.contractOnFile = contractOnFile;
            this.critical = critical;
            this.comments = comments;
        }
    }
}