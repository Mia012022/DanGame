﻿using System;
using System.Collections.Generic;

namespace DenGame.Models;

public partial class PaymentRecord
{
    public int PaymentId { get; set; }

    public int OrderId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public int Amount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}
