using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;
using NDIS.Entities;
using System.Security.Claims;

namespace NDISBudget.Areas.Company.Controllers
{
    [Area("Company")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            unitOfWork = context;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("/Company/Home/Index")]
        public async Task<IActionResult> Index()
        {
            DashboardData model = new DashboardData();
            int id= Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            DashboardData c = await unitOfWork.Settings.GetDashboardDataIdAsync(Convert.ToInt32(id));
                model = c;
           
            return View(model);
        }



    }
}
