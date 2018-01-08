using Microsoft.AspNetCore.Mvc;

namespace Europa.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}