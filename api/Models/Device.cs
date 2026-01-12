using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Device
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Ipv4 { get; set; }

        public Device()
        {
            Id = 0;
            Name = "";
            Ipv4 = "";
        }

        public Device( string Name, string Ipv4)
        {
            this.Name = Name;
            this.Ipv4 = Ipv4;
        }
    }
}