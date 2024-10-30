using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;

namespace NDISBudget.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork context)
        {
            _logger = logger;
            unitOfWork = context;
        }
        [HttpGet("/Admin/Home/Index")]
        public IActionResult Index()
        {
            return View();
        }



    }
}
