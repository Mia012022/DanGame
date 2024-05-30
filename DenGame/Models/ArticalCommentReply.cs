using System;
using System.Collections.Generic;

namespace DenGame.Models;

public partial class ArticalCommentReply
{
    public int ReplyId { get; set; }

    public int CommentId { get; set; }

    public int UserId { get; set; }

    public string ReplyContent { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ArticalComment Comment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
