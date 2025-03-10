using System;
using System.Collections.Generic;

namespace CropDealBackend.Models;

public partial class AddOnType
{
    public int AddOnTypeId { get; set; }

    public string AddOnTypeName { get; set; } = null!;

    public virtual ICollection<AddOn> AddOns { get; set; } = new List<AddOn>();
}
