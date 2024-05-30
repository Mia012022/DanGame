using System;
using System.Collections.Generic;

namespace DenGame.Models;

public partial class ChatRoom
{
    public int ChatRoomId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime LastMessageTime { get; set; }
}
