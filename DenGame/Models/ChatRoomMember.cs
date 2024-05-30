using System;
using System.Collections.Generic;

namespace DenGame.Models;

public partial class ChatRoomMember
{
    public int ChatRoomId { get; set; }

    public int UserId { get; set; }

    public DateTime JoinDate { get; set; }
}
