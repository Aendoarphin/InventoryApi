using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{

    public class Vendor
    {
        public int id { get; set; }
        public string name { get; set; }
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

        public Vendor(
            int id,
            string name,
            string? address = null,
            string? city = null,
            string? phone = null,
            string? fax = null,
            string? contact = null,
            string? email = null,
            string? website = null,
            string? productServiceArea = null,
            string? contractOnFile = null,
            string? critical = null,
            string? comments = null
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