using Microsoft.AspNetCore.Mvc;
using DenGame.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Mvc.Core;
using X.PagedList;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.IO;
namespace DenGame.Controllers
{
	public class ForumController : Controller
	{
		private readonly IWebHostEnvironment _env;
		private readonly ILogger<ForumController> _logger;
		private readonly DanGameDbContext _context;
		int pageSize = 5;
		public ForumController(IWebHostEnvironment env, ILogger<ForumController> logger, DanGameDbContext context)
		{
			_env = env;
			_logger = logger;
			_context = context;
		}
		//---------------論壇首頁 有做分頁-------------
		public async Task<IActionResult> Index(int? page)
		{
			int pageNumber = (page ?? 1);
			var article = _context.ArticleLists.OrderBy(c => c.ArticalId).ToPagedList(pageNumber, pageSize);
			var images = await _context.ArticleLists.ToListAsync();

			var viewModel = new PageListViewModel
			{
				ArticleList = article
			};
			return View(viewModel);
		}
		//----------------快來發文吧------------------
		public IActionResult Post()
		{
			return View();
		}
		//-----------------文章細節---------------------
		public async Task<IActionResult> Artical(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var article = await _context.ArticleLists
				.Include(a => a.ArticalComments)
				.ThenInclude(c => c.User)
				.ThenInclude(c => c.UserProfile)
				.Include(a => a.ArticalComments)
					.ThenInclude(c => c.ArticalCommentReplies)
					.ThenInclude(c => c.User)
					.ThenInclude(c => c.UserProfile)
				.Include(a => a.ArticalComments)
					.ThenInclude(c => c.ArticalCommentLikes)
				.Include(a => a.ArticalComments)	
				.FirstOrDefaultAsync(x => x.ArticalId == id);
			if (article == null)
			{
				return NotFound();
			}
			var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == article.UserId);
			var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == article.UserId);	
			var likes = await _context.ArticalLikes.Where(l => l.ArticalId == id).ToListAsync();
			var views = await _context.ArticalViews.Where(v => v.ArticalId == id).ToListAsync();
			var commentLikes = await _context.ArticalCommentLikes
				.Where(cl => article.ArticalComments.Select(c => c.CommentId).Contains(cl.CommentId))
				.ToListAsync();
			
			var viewModel = new ArticlePageViewModel
			{
				Article = article,
				User	= user ,
				UserProfile = userProfile,
				Likes = likes,
				Comments = article.ArticalComments.ToList(),
				Replies = article.ArticalComments.SelectMany(c => c.ArticalCommentReplies).ToList(),
				CommentLikes = commentLikes,
				Views = views
			};
			return View(viewModel);
		}
		//------------------個人主頁---------------------
		public async Task<IActionResult> ForumUser(int id)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
			var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.UserId == id);
			var forumuser = await _context.ArticleLists.Where(x => x.UserId == id).ToListAsync();
			var comment = await _context.ArticalComments.Where(x =>x.UserId == id).ToListAsync();
			var like = await _context.ArticalLikes.Where(x => x.UserId == id).ToListAsync();
			var commentlike = await _context.ArticalCommentLikes.Where(x => x.UserId== id).ToListAsync();
			var likedArticles = await _context.ArticalLikes
			.Where(like => like.UserId == id)
			.Select(like => like.Artical)
			.ToListAsync();
			if (user == null)
			{
				return NotFound();
			}
			var viewModel = new UserArticlesViewModel
			{
				User = user,
				UserProfile = userProfile,
				Articles = forumuser,
				Likes = like,
				Comments = comment,
				CommentLikes= commentlike,
				LikedArticles= likedArticles
			};

			return View(viewModel);
		}
		
		//------------------發表上傳文章-----------------
		[HttpGet]
		public IActionResult Upload()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file, string title, string description,string Category)
		{
			if (file != null && file.Length > 0)
			{
				using (var memoryStream = new MemoryStream())
				{
					await file.CopyToAsync(memoryStream);
					var artical = new ArticleList
					{
						UserId = 5,
						ArticalCoverPhoto = memoryStream.ToArray(),
						ArticalTitle = title,
						ArticalContent = description,
						ArticleCategory = Category
					};

					_context.ArticleLists.Add(artical);
					await _context.SaveChangesAsync();
				}

				return RedirectToAction("Index");
			}

			return View();
		}
		//-----------------ckeditor5上傳照片--------------
		[HttpGet]
		public IActionResult UploadImage()
		{
			return View();
		}
		[HttpPost]
		public IActionResult UploadImage(List<IFormFile> files)
		{
			var filepath = "";
			foreach (IFormFile photo in Request.Form.Files)
			{
				string serverMapPath = Path.Combine(_env.WebRootPath, "images", photo.FileName);
				using (var stream = new FileStream(serverMapPath, FileMode.Create))
				{ photo.CopyTo(stream); }
				filepath = "http://localhost:5237/" + "images/" + photo.FileName;
			}
			return Json(new { url = filepath });
		}

		//------------------編輯文章-------------------
		public IActionResult Edit(int id)
		{
			var edit = _context.ArticleLists.Find(id);
			if (edit == null)
			{
				return NotFound();
			}
			return View(edit);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(ArticleList model)
		{
			
				var article = await _context.ArticleLists.FindAsync(model.ArticalId);
				if (article == null)
				{
					return NotFound();
				}

				// 更新文章屬性
				article.ArticalTitle = model.ArticalTitle;
				article.ArticalContent = model.ArticalContent;
				article.ArticalCreateDate = DateTime.Now; 

				// 處理文件上傳
				if (model.File != null && model.File.Length > 0)
				{
					using (var memoryStream = new MemoryStream())
					{
						await model.File.CopyToAsync(memoryStream);
						article.ArticalCoverPhoto = memoryStream.ToArray();
					}
				}

				try
				{
					_context.Update(article);
					await _context.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					// 捕捉錯誤訊息
					ModelState.AddModelError("", $"無法保存更改: {ex.Message}");
					return View(model);
				}

				return RedirectToAction("ForumUser");
			
		}
		//-------------------刪除文章頁面-------------------------
		public IActionResult DeleteArticle(int? id)
		{
			var article = _context.ArticleLists.Find(id);
			return View(article);
		}
		[HttpPost]
		//-------------------刪除文章---------------------------
		public IActionResult DeleteArticle(int ArticalId)
		{
			
			var article = _context.ArticleLists.Find(ArticalId);
			if (article == null)
			{
				return NotFound();
			}
				_context.ArticleLists.Remove(article);
			_context.SaveChanges();
			return Redirect("/Forum/ForumUser");
		}
		
		//-------------------留言------------------------------
		[HttpPost]
		public async Task<IActionResult> AddComment( string comment,int articalId)
		{
			if (ModelState.IsValid)
			{
				var newComment = new ArticalComment
				{
					UserId = 2,
					ArticalId = articalId,
					CommentContent = comment,
					CommentCreateDate = DateTime.Now
				};
				
				_context.ArticalComments.Add(newComment);
				await _context.SaveChangesAsync();
				
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> AddReply(string comment,int commentId)
		{
			if (ModelState.IsValid)
			{
				var replyComment = new ArticalCommentReply
				{
					UserId = 3,
					CommentId = commentId,
					ReplyContent = comment,
					CreatedAt = DateTime.Now
				};
				 
				_context.ArticalCommentReplies.Add(replyComment);
				await _context.SaveChangesAsync();
				
			}
			return RedirectToAction("Index");
		}
	}
}
