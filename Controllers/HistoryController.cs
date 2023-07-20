using Microsoft.AspNetCore.Mvc;

namespace QFamilyForum.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
