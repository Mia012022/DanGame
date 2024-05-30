using System;
using System.Collections.Generic;

namespace DenGame.Models;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int UserId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool? SubscriptionStatus { get; set; }

    public virtual User User { get; set; } = null!;
}
