using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenGame.Migrations
{
    /// <inheritdoc />
    public partial class AddArticleCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    AppID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.AppID);
                });

            migrationBuilder.CreateTable(
                name: "ChatRoom",
                columns: table => new
                {
                    ChatRoomID = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastMessageTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoom", x => x.ChatRoomID);
                });

            migrationBuilder.CreateTable(
                name: "ChatRoomMember",
                columns: table => new
                {
                    ChatRoomID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomMember", x => x.ChatRoomID);
                });

            migrationBuilder.CreateTable(
                name: "GenreTag",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "int", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false),
                    ChatRoomID = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MessageContent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "OnlineStatus",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OnlineTime = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    OfflineTime = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "AppDetail",
                columns: table => new
                {
                    AppID = table.Column<int>(type: "int", nullable: false),
                    AppName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AppType = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    DevloperName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SupportedLanguage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HeaderImage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    BackgroundImage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CapsuleImage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SourceFile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDetail", x => x.AppID);
                    table.ForeignKey(
                        name: "FK_AppDetail_App",
                        column: x => x.AppID,
                        principalTable: "App",
                        principalColumn: "AppID");
                });

            migrationBuilder.CreateTable(
                name: "AppDLC",
                columns: table => new
                {
                    AppID = table.Column<int>(type: "int", nullable: false),
                    DLCAppID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDLC", x => new { x.AppID, x.DLCAppID });
                    table.ForeignKey(
                        name: "FK_AppDLC_App",
                        column: x => x.AppID,
                        principalTable: "App",
                        principalColumn: "AppID");
                    table.ForeignKey(
                        name: "FK_AppDLC_App1",
                        column: x => x.DLCAppID,
                        principalTable: "App",
                        principalColumn: "AppID");
                });

            migrationBuilder.CreateTable(
                name: "AppMedia",
                columns: table => new
                {
                    MediaID = table.Column<int>(type: "int", nullable: false),
                    AppID = table.Column<int>(type: "int", nullable: false),
                    MediaType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ThumbnailPath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    OriginalPath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AppMedia__18E7E81F0E09CEC5", x => new { x.MediaID, x.AppID, x.MediaType });
                    table.ForeignKey(
                        name: "FK__AppMedia__AppID__32AB8735",
                        column: x => x.AppID,
                        principalTable: "App",
                        principalColumn: "AppID");
                });

            migrationBuilder.CreateTable(
                name: "AppGenre",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "int", nullable: false),
                    AppID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTag", x => new { x.TagID, x.AppID });
                    table.ForeignKey(
                        name: "FK_AppTag_App",
                        column: x => x.AppID,
                        principalTable: "App",
                        principalColumn: "AppID");
                    table.ForeignKey(
                        name: "FK_AppTag_Tag",
                        column: x => x.TagID,
                        principalTable: "GenreTag",
                        principalColumn: "TagID");
                });

            migrationBuilder.CreateTable(
                name: "ArticleList",
                columns: table => new
                {
                    ArticalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ArticalTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArticalContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticalCreateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ArticalCoverPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ArticleCategory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleList", x => x.ArticalID);
                    table.ForeignKey(
                        name: "FK_ArticleList_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "FriendList",
                columns: table => new
                {
                    FriendshipID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FriendUserID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendList", x => x.FriendshipID);
                    table.ForeignKey(
                        name: "FK_FriendList_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    SubscriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubscriptionStatus = table.Column<bool>(type: "bit", nullable: true, computedColumnSql: "(CONVERT([bit],case when getdate()>[EndDate] then (0) else (1) end))", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.SubscriptionID);
                    table.ForeignKey(
                        name: "FK_Subscription_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    ProfileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOFBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.ProfileID);
                    table.ForeignKey(
                        name: "FK_UserProfile_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ArticalComment",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticalID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CommentCreateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comment_ArticleList",
                        column: x => x.ArticalID,
                        principalTable: "ArticleList",
                        principalColumn: "ArticalID");
                    table.ForeignKey(
                        name: "FK_Comment_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ArticalLike",
                columns: table => new
                {
                    ArticalLikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticalID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticalLike", x => x.ArticalLikeID);
                    table.ForeignKey(
                        name: "FK_ArticalLike_ArticleList",
                        column: x => x.ArticalID,
                        principalTable: "ArticleList",
                        principalColumn: "ArticalID");
                    table.ForeignKey(
                        name: "FK_ArticalLike_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ArticalView",
                columns: table => new
                {
                    ArticalViewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticalID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticalView", x => x.ArticalViewID);
                    table.ForeignKey(
                        name: "FK_ArticalView_ArticleList",
                        column: x => x.ArticalID,
                        principalTable: "ArticleList",
                        principalColumn: "ArticalID");
                    table.ForeignKey(
                        name: "FK_ArticalView_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    AppID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItem_App",
                        column: x => x.AppID,
                        principalTable: "App",
                        principalColumn: "AppID");
                    table.ForeignKey(
                        name: "FK_OrderItem_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    OderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payment_Order",
                        column: x => x.OderID,
                        principalTable: "Order",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "ArticalCommentLike",
                columns: table => new
                {
                    CommentLikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticalCommentLike", x => x.CommentLikeID);
                    table.ForeignKey(
                        name: "FK_ArticalCommentLike_ArticalComment",
                        column: x => x.CommentID,
                        principalTable: "ArticalComment",
                        principalColumn: "CommentID");
                    table.ForeignKey(
                        name: "FK_ArticalCommentLike_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ArticalCommentReply",
                columns: table => new
                {
                    ReplyID = table.Column<int>(type: "int", nullable: false),
                    CommentID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ReplyContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticalCommentReply", x => x.ReplyID);
                    table.ForeignKey(
                        name: "FK_ArticalCommentReply_ArticalComment",
                        column: x => x.CommentID,
                        principalTable: "ArticalComment",
                        principalColumn: "CommentID");
                    table.ForeignKey(
                        name: "FK_ArticalCommentReply_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDLC_DLCAppID",
                table: "AppDLC",
                column: "DLCAppID");

            migrationBuilder.CreateIndex(
                name: "IX_AppGenre_AppID",
                table: "AppGenre",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "IX_AppMedia_AppID",
                table: "AppMedia",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalComment_ArticalID",
                table: "ArticalComment",
                column: "ArticalID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalComment_UserID",
                table: "ArticalComment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalCommentLike_CommentID",
                table: "ArticalCommentLike",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalCommentLike_UserID",
                table: "ArticalCommentLike",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalCommentReply_CommentID",
                table: "ArticalCommentReply",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalCommentReply_UserID",
                table: "ArticalCommentReply",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalLike_ArticalID",
                table: "ArticalLike",
                column: "ArticalID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalLike_UserID",
                table: "ArticalLike",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalView_ArticalID",
                table: "ArticalView",
                column: "ArticalID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticalView_UserID",
                table: "ArticalView",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleList_UserID",
                table: "ArticleList",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_FriendList_UserID",
                table: "FriendList",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserID",
                table: "Order",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_AppID",
                table: "OrderItem",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderID",
                table: "OrderItem",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OderID",
                table: "Payment",
                column: "OderID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserID",
                table: "Subscription",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "email_User",
                table: "User",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "username_User",
                table: "User",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserID",
                table: "UserProfile",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDetail");

            migrationBuilder.DropTable(
                name: "AppDLC");

            migrationBuilder.DropTable(
                name: "AppGenre");

            migrationBuilder.DropTable(
                name: "AppMedia");

            migrationBuilder.DropTable(
                name: "ArticalCommentLike");

            migrationBuilder.DropTable(
                name: "ArticalCommentReply");

            migrationBuilder.DropTable(
                name: "ArticalLike");

            migrationBuilder.DropTable(
                name: "ArticalView");

            migrationBuilder.DropTable(
                name: "ChatRoom");

            migrationBuilder.DropTable(
                name: "ChatRoomMember");

            migrationBuilder.DropTable(
                name: "FriendList");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "OnlineStatus");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "GenreTag");

            migrationBuilder.DropTable(
                name: "ArticalComment");

            migrationBuilder.DropTable(
                name: "App");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ArticleList");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
