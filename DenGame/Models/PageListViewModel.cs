using X.PagedList;
namespace DenGame.Models
{
	public class PageListViewModel
	{
		public X.PagedList.IPagedList<ArticleList>? ArticleList { get; set; }
		public List<ArticalView>? ArticalViews { get; set; }
		
		public List<ArticalComment>? ArticalComments { get; set;}
		public List<TopUserViewModel>? TopUsers { get; set; }
		
	}
}
