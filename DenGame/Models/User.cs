using System;
using System.Collections.Generic;

namespace DenGame.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ArticalCommentLike> ArticalCommentLikes { get; set; } = new List<ArticalCommentLike>();

    public virtual ICollection<ArticalCommentReply> ArticalCommentReplies { get; set; } = new List<ArticalCommentReply>();

    public virtual ICollection<ArticalComment> ArticalComments { get; set; } = new List<ArticalComment>();

    public virtual ICollection<ArticalLike> ArticalLikes { get; set; } = new List<ArticalLike>();

    public virtual ICollection<ArticalView> ArticalViews { get; set; } = new List<ArticalView>();

    public virtual ICollection<ArticleList> ArticleLists { get; set; } = new List<ArticleList>();

    public virtual ICollection<FriendList> FriendLists { get; set; } = new List<FriendList>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
