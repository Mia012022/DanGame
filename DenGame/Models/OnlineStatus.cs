using System;
using System.Collections.Generic;

namespace DenGame.Models;

public partial class OnlineStatus
{
    public int UserId { get; set; }

    public DateTime OnlineTime { get; set; }

    public DateTime OfflineTime { get; set; }
}
