using Microsoft.AspNetCore.Mvc;

namespace DenGame.Controllers
{
	public class ForumController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Post()
		{
			return View();
		}
		public IActionResult Artical()
		{
			return View();
		}
		public IActionResult ForumUser()
		{
			return View();
		}
	}
}
