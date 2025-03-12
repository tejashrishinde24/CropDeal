using System;
using System.Collections.Generic;

namespace CropDealBackend.Models
{

    public partial class Admin
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public virtual ICollection<AddOn> AddOns { get; set; } = new List<AddOn>();
    }
}