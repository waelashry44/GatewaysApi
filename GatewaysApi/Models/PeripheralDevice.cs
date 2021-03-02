using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GatewaysApi.Models
{
    public partial class PeripheralDevice
    {
        public int Id { get; set; }
        public int? Uid { get; set; }
        public string Vendor { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? Status { get; set; }
        public int GatewayId { get; set; }

        public virtual Gateway Gateway { get; set; }
    }
}
