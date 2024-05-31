using Microsoft.AspNetCore.Mvc;
using DenGame.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Mvc.Core;
using X.PagedList;
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
		public async  Task<IActionResult> Index(int? page)
		{
			int pageNumber = (page ?? 1);
			var artical =  _context.ArticleLists.OrderBy(c => c.ArticalId).ToPagedList(pageNumber, pageSize);
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
			var articallist = await _context.ArticleLists
				.FirstOrDefaultAsync(x => x.ArticalId==id);
			if(articallist == null)
			{
				return NotFound();
			}
			return View(articallist);
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
						UserId = 2,
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
	}
}
