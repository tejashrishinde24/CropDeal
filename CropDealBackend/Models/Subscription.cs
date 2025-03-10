using System;
using System.Collections.Generic;

namespace CropDealBackend.Models;

public partial class Subscription
{
    public int Id { get; set; }

    public int DealerId { get; set; }

    public int CropId { get; set; }

    public DateTime SubscriptionDate { get; set; }

    public bool IsNotificationEnabled { get; set; }

    public virtual CropDetail Crop { get; set; } = null!;

    public virtual UserDetail Dealer { get; set; } = null!;
}
