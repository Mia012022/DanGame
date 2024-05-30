using System;
using System.Collections.Generic;

namespace DenGame.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int OderId { get; set; }

    public virtual Order Oder { get; set; } = null!;
}
