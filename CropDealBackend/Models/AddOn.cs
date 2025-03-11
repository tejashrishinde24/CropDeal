using System;
using System.Collections.Generic;

namespace CropDealBackend.Models;

public partial class AddOn
{
    public int AddOnId { get; set; }

    public int AdminId { get; set; }

    public int AddOnTypeId { get; set; }

    public decimal PricePerUnit { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public string? AddOnName { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual AddOnType AddOnType { get; set; } = null!;

    public virtual Admin Admin { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
