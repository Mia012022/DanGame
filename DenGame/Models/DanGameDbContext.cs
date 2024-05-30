using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DenGame.Models;

public partial class DanGameDbContext : DbContext
{
    public DanGameDbContext()
    {
    }

    public DanGameDbContext(DbContextOptions<DanGameDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<App> Apps { get; set; }

    public virtual DbSet<AppDetail> AppDetails { get; set; }

    public virtual DbSet<AppMedium> AppMedia { get; set; }

    public virtual DbSet<ArticalComment> ArticalComments { get; set; }

    public virtual DbSet<ArticalCommentLike> ArticalCommentLikes { get; set; }

    public virtual DbSet<ArticalCommentReply> ArticalCommentReplies { get; set; }

    public virtual DbSet<ArticalLike> ArticalLikes { get; set; }

    public virtual DbSet<ArticalView> ArticalViews { get; set; }

    public virtual DbSet<ArticleList> ArticleLists { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<ChatRoomMember> ChatRoomMembers { get; set; }

    public virtual DbSet<FriendList> FriendLists { get; set; }

    public virtual DbSet<GenreTag> GenreTags { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<OnlineStatus> OnlineStatuses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=DanGame;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<App>(entity =>
        {
            entity.ToTable("App");

            entity.Property(e => e.AppId).HasColumnName("AppID");
            entity.Property(e => e.AppName).HasMaxLength(128);

            entity.HasMany(d => d.Apps).WithMany(p => p.Dlcapps)
                .UsingEntity<Dictionary<string, object>>(
                    "AppDlc",
                    r => r.HasOne<App>().WithMany()
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppDLC_App"),
                    l => l.HasOne<App>().WithMany()
                        .HasForeignKey("DlcappId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppDLC_App1"),
                    j =>
                    {
                        j.HasKey("AppId", "DlcappId");
                        j.ToTable("AppDLC");
                        j.IndexerProperty<int>("AppId").HasColumnName("AppID");
                        j.IndexerProperty<int>("DlcappId").HasColumnName("DLCAppID");
                    });

            entity.HasMany(d => d.Dlcapps).WithMany(p => p.Apps)
                .UsingEntity<Dictionary<string, object>>(
                    "AppDlc",
                    r => r.HasOne<App>().WithMany()
                        .HasForeignKey("DlcappId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppDLC_App1"),
                    l => l.HasOne<App>().WithMany()
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppDLC_App"),
                    j =>
                    {
                        j.HasKey("AppId", "DlcappId");
                        j.ToTable("AppDLC");
                        j.IndexerProperty<int>("AppId").HasColumnName("AppID");
                        j.IndexerProperty<int>("DlcappId").HasColumnName("DLCAppID");
                    });
        });

        modelBuilder.Entity<AppDetail>(entity =>
        {
            entity.HasKey(e => e.AppId);

            entity.ToTable("AppDetail");

            entity.Property(e => e.AppId)
                .ValueGeneratedNever()
                .HasColumnName("AppID");
            entity.Property(e => e.AppName).HasMaxLength(64);
            entity.Property(e => e.AppType)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.BackgroundImage).HasMaxLength(256);
            entity.Property(e => e.CapsuleImage).HasMaxLength(256);
            entity.Property(e => e.DevloperName).HasMaxLength(32);
            entity.Property(e => e.HeaderImage).HasMaxLength(256);
            entity.Property(e => e.Platform).HasMaxLength(20);
            entity.Property(e => e.ShortDescription).HasMaxLength(512);
            entity.Property(e => e.SupportedLanguage).HasMaxLength(256);
            entity.Property(e => e.Website).HasMaxLength(256);

            entity.HasOne(d => d.App).WithOne(p => p.AppDetail)
                .HasForeignKey<AppDetail>(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppDetail_App");
        });

        modelBuilder.Entity<AppMedium>(entity =>
        {
            entity.HasKey(e => new { e.MediaId, e.AppId, e.MediaType }).HasName("PK__AppMedia__18E7E81F0E09CEC5");

            entity.Property(e => e.MediaId).HasColumnName("MediaID");
            entity.Property(e => e.AppId).HasColumnName("AppID");
            entity.Property(e => e.MediaType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OriginalPath).HasMaxLength(256);
            entity.Property(e => e.ThumbnailPath).HasMaxLength(256);
            entity.Property(e => e.Title).HasMaxLength(128);

            entity.HasOne(d => d.App).WithMany(p => p.AppMedia)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AppMedia__AppID__32AB8735");
        });

        modelBuilder.Entity<ArticalComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK_Comment");

            entity.ToTable("ArticalComment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.ArticalId).HasColumnName("ArticalID");
            entity.Property(e => e.CommentContent).HasMaxLength(100);
            entity.Property(e => e.CommentCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Artical).WithMany(p => p.ArticalComments)
                .HasForeignKey(d => d.ArticalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_ArticleList");

            entity.HasOne(d => d.User).WithMany(p => p.ArticalComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<ArticalCommentLike>(entity =>
        {
            entity.HasKey(e => e.CommentLikeId);

            entity.ToTable("ArticalCommentLike");

            entity.Property(e => e.CommentLikeId).HasColumnName("CommentLikeID");
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Comment).WithMany(p => p.ArticalCommentLikes)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticalCommentLike_ArticalComment");

            entity.HasOne(d => d.User).WithMany(p => p.ArticalCommentLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticalCommentLike_User");
        });

        modelBuilder.Entity<ArticalCommentReply>(entity =>
        {
            entity.HasKey(e => e.ReplyId);

            entity.ToTable("ArticalCommentReply");

            entity.Property(e => e.ReplyId)
                .ValueGeneratedNever()
                .HasColumnName("ReplyID");
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Comment).WithMany(p => p.ArticalCommentReplies)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticalCommentReply_ArticalComment");

            entity.HasOne(d => d.User).WithMany(p => p.ArticalCommentReplies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticalCommentReply_User");
        });

        modelBuilder.Entity<ArticalLike>(entity =>
        {
            entity.ToTable("ArticalLike");

            entity.Property(e => e.ArticalLikeId).HasColumnName("ArticalLikeID");
            entity.Property(e => e.ArticalId).HasColumnName("ArticalID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Artical).WithMany(p => p.ArticalLikes)
                .HasForeignKey(d => d.ArticalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticalLike_ArticleList");

            entity.HasOne(d => d.User).WithMany(p => p.ArticalLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticalLike_User");
        });

        modelBuilder.Entity<ArticalView>(entity =>
        {
            entity.ToTable("ArticalView");

            entity.Property(e => e.ArticalViewId).HasColumnName("ArticalViewID");
            entity.Property(e => e.ArticalId).HasColumnName("ArticalID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Artical).WithMany(p => p.ArticalViews)
                .HasForeignKey(d => d.ArticalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticalView_ArticleList");

            entity.HasOne(d => d.User).WithMany(p => p.ArticalViews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticalView_User");
        });

        modelBuilder.Entity<ArticleList>(entity =>
        {
            entity.HasKey(e => e.ArticalId);

            entity.ToTable("ArticleList");

            entity.Property(e => e.ArticalId).HasColumnName("ArticalID");
            entity.Property(e => e.ArticalCreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ArticalTitle).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ArticleLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleList_User");
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.ToTable("ChatRoom");

            entity.Property(e => e.ChatRoomId)
                .ValueGeneratedNever()
                .HasColumnName("ChatRoomID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LastMessageTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<ChatRoomMember>(entity =>
        {
            entity.HasKey(e => e.ChatRoomId);

            entity.ToTable("ChatRoomMember");

            entity.Property(e => e.ChatRoomId)
                .ValueGeneratedNever()
                .HasColumnName("ChatRoomID");
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<FriendList>(entity =>
        {
            entity.HasKey(e => e.FriendshipId);

            entity.ToTable("FriendList");

            entity.Property(e => e.FriendshipId)
                .ValueGeneratedNever()
                .HasColumnName("FriendshipID");
            entity.Property(e => e.FriendUserId).HasColumnName("FriendUserID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.FriendLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FriendList_User");
        });

        modelBuilder.Entity<GenreTag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK_Tag");

            entity.ToTable("GenreTag");

            entity.Property(e => e.TagId)
                .ValueGeneratedNever()
                .HasColumnName("TagID");
            entity.Property(e => e.TagName).HasMaxLength(32);

            entity.HasMany(d => d.Apps).WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "AppGenre",
                    r => r.HasOne<App>().WithMany()
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppTag_App"),
                    l => l.HasOne<GenreTag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AppTag_Tag"),
                    j =>
                    {
                        j.HasKey("TagId", "AppId").HasName("PK_AppTag");
                        j.ToTable("AppGenre");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                        j.IndexerProperty<int>("AppId").HasColumnName("AppID");
                    });
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Message");

            entity.Property(e => e.ChatRoomId).HasColumnName("ChatRoomID");
            entity.Property(e => e.CreatedTime).HasColumnType("smalldatetime");
            entity.Property(e => e.MessageContent).HasMaxLength(100);
            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<OnlineStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OnlineStatus");

            entity.Property(e => e.OfflineTime).HasColumnType("smalldatetime");
            entity.Property(e => e.OnlineTime).HasColumnType("smalldatetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItem");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.AppId).HasColumnName("AppID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.App).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_App");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("PaymentID");
            entity.Property(e => e.OderId).HasColumnName("OderID");

            entity.HasOne(d => d.Oder).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Order");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.ToTable("Subscription");

            entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.SubscriptionStatus).HasComputedColumnSql("(CONVERT([bit],case when getdate()>[EndDate] then (0) else (1) end))", false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subscription_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.UserId, "email_User").IsUnique();

            entity.HasIndex(e => e.UserId, "username_User").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(75);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(75);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId);

            entity.ToTable("UserProfile");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DateOfbirth).HasColumnName("DateOFBirth");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.ProfilePictureUrl).HasMaxLength(200);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfile_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
