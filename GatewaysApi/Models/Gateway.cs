using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GatewaysApi.Models
{
    public partial class Gateway
    {
        public Gateway()
        {
            PeripheralDevice = new HashSet<PeripheralDevice>();
        }

        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$",
         ErrorMessage = "it's invalid")]
        public string Ipv4address { get; set; }

        public virtual ICollection<PeripheralDevice> PeripheralDevice { get; set; }
    }
}
