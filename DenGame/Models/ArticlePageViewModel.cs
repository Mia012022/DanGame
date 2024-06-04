namespace DenGame.Models
{
	public class ArticlePageViewModel
	{
		public User User { get; set; }
		public UserProfile Profile { get; set; }
		public ArticleList Article { get; set; }
		public List<ArticalComment> Comments { get; set; }
		public List<ArticalCommentReply> Replies { get; set; }
		public List<ArticalLike> Likes { get; set; }
		public List<ArticalView> Views { get; set; }
		public List<ArticalCommentLike> CommentLikes { get; set; }
	}
}
