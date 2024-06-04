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
		int pageSize = 3;
		public ForumController(IWebHostEnvironment env, ILogger<ForumController> logger, DanGameDbContext context)
		{
			_env = env;
			_logger = logger;
			_context = context;
		}
		public async Task<IActionResult> Index(int? page)
		{
			int pageNumber = (page ?? 1);
			var artical = _context.ArticleLists.OrderBy(c => c.ArticalId).ToPagedList(pageNumber, pageSize);
			var images = await _context.ArticleLists.ToListAsync();
			return View(artical);
		}
		public IActionResult Post()
		{
			return View();
		}
		public async Task<IActionResult> Artical(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var artical = await _context.ArticleLists
				.Include(a => a.ArticalComments)
				.ThenInclude(c => c.User)
				.Include(a => a.ArticalComments)
					.ThenInclude(c => c.ArticalCommentReplies)
				.Include(a => a.ArticalComments)
					.ThenInclude(c => c.ArticalCommentLikes)
				.FirstOrDefaultAsync(x => x.ArticalId == id);
			if (artical == null)
			{
				return NotFound();
			}
			var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == artical.UserId);
			var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == artical.UserId);	
			var likes = await _context.ArticalLikes.Where(l => l.ArticalId == id).ToListAsync();
			var views = await _context.ArticalViews.Where(v => v.ArticalId == id).ToListAsync();
			var commentLikes = await _context.ArticalCommentLikes
				.Where(cl => artical.ArticalComments.Select(c => c.CommentId).Contains(cl.CommentId))
				.ToListAsync();
			
			var viewModel = new ArticlePageViewModel
			{

			};
			return View(artical);
		}
		public async Task<IActionResult> ForumUser()
		{

			return View(await _context.ArticleLists.ToListAsync());
		}
		[HttpGet]
		public IActionResult Upload()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file, string title, string description)
		{
			if (file != null && file.Length > 0)
			{
				using (var memoryStream = new MemoryStream())
				{
					await file.CopyToAsync(memoryStream);
					var artical = new ArticleList
					{
						UserId = 1,
						ArticalCoverPhoto = memoryStream.ToArray(),
						ArticalTitle = title,
						ArticalContent = description

					};

					_context.ArticleLists.Add(artical);
					await _context.SaveChangesAsync();
				}

				return RedirectToAction("Index");
			}

			return View();
		}
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
				article.ArticalCreateDate = DateTime.Now; // 或者根據您的邏輯

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
		public IActionResult Delete(int? id)
		{
			var article = _context.ArticleLists.Find(id);
			return View(article);
		}
		[HttpPost]
		public IActionResult Delete(int ArticalId)
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
		[HttpGet]
		public IActionResult AddComment()
		{
			return View();
		}
		[HttpPost]
		
		public async Task<IActionResult> AddComment( string comment)
		{
			if (ModelState.IsValid)
			{
				var newComment = new ArticalComment
				{
					UserId = 3,
					ArticalId = 10001,
					CommentContent = comment,
					CommentCreateDate = DateTime.Now
				};
				
				_context.ArticalComments.Add(newComment);
				await _context.SaveChangesAsync();
				
			}

			return RedirectToAction("Index");
		}
	}
}
