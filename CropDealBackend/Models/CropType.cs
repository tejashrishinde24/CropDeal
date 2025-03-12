using System;
using System.Collections.Generic;

namespace CropDealBackend.Models
{

    public partial class CropType
    {
        public int CropTypeId { get; set; }

        public string CropTypeName { get; set; } = null!;

        public virtual ICollection<CropDetail> CropDetails { get; set; } = new List<CropDetail>();
    }
}
