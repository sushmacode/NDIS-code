using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;
using NDIS.Data.Repository;
using NDIS.Entities;
using System.Security.Claims;

namespace NDISBudget.Areas.Company.Controllers
{
    [Area("Company")]
    [Authorize(Roles = "Company")]
    public class ScheduleController : Controller
    {
        private readonly ILogger<ScheduleController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ScheduleController(ILogger<ScheduleController> logger, IUnitOfWork context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            unitOfWork = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            GetClientSupportItemScheduleResponse modal = new GetClientSupportItemScheduleResponse();
            int cbid = 0;
            int sid = 0;
            cbid =Convert.ToInt32(id.Split('_')[0]);
            sid = Convert.ToInt32(id.Split('_')[1]);
            modal = await unitOfWork.ClientBudgets.GetClientSupportItemSchedule(cbid);
            modal.SupportItemId = sid;
            //modal.ClientBudgetId = cbid;
            return View(modal);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmedSchedule(int id = 0)
        {
            ViewBag.Id = id;
            GetClientSupportItemScheduleResponse modal = new GetClientSupportItemScheduleResponse();
            modal = await unitOfWork.ClientBudgets.GetClientSupportItemSchedule(id);
            return View(modal);
        }

        [HttpGet]
        public async Task<IActionResult> BudgetSchedule()
        {
            GetBudgetSchedule model=new GetBudgetSchedule();
            var CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            //model.BudgetSummary= await unitOfWork.ClientBudgets.GetClientSupportItemSchedule(id);
            List<Client> myList2 = new List<Client>();
            myList2 = (from c in await unitOfWork.Clients.GetCompanyClientsAsync(CompanyId)
                       select new Client()
                       {
                           ClientId = c.ClientId,
                           FirstName = c.FirstName + " " + c.LastName,
                           LastName = c.LastName,
                       }).ToList();
            myList2.Insert(0, new Client { ClientId = 0, FirstName = "New Participant" });
            model.Clients = myList2;
            List<ScheduledFrequencies> myList4 = new List<ScheduledFrequencies>();
            myList4 = (from c in await unitOfWork.ClientBudgets.GetddlScheduledFrequency()
                       select new ScheduledFrequencies()
                       {
                           ScheduledFrequencyId = c.ScheduledFrequencyId + "_" + c.IsMultiWeekday,
                           ScheduledFrequency = c.ScheduledFrequency,
                           //LastName = c.LastName,
                       }).ToList();
            myList4.Insert(0, new ScheduledFrequencies { ScheduledFrequencyId = "0", ScheduledFrequency = "Select Frequency" });
            model.scheduledFrequencies = myList4;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> BudgetSchedule1()
        {
            GetBudgetSchedule model = new GetBudgetSchedule();
            var CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            //model.BudgetSummary= await unitOfWork.ClientBudgets.GetClientSupportItemSchedule(id);
            List<Client> myList2 = new List<Client>();
            myList2 = (from c in await unitOfWork.Clients.GetCompanyClientsAsync(CompanyId)
                       select new Client()
                       {
                           ClientId = c.ClientId,
                           FirstName = c.FirstName + " " + c.LastName,
                           LastName = c.LastName,
                       }).ToList();
            myList2.Insert(0, new Client { ClientId = 0, FirstName = "New Participant" });
            model.Clients = myList2;
            List<ScheduledFrequencies> myList4 = new List<ScheduledFrequencies>();
            myList4 = (from c in await unitOfWork.ClientBudgets.GetddlScheduledFrequency()
                       select new ScheduledFrequencies()
                       {
                           ScheduledFrequencyId = c.ScheduledFrequencyId + "_" + c.IsMultiWeekday,
                           ScheduledFrequency = c.ScheduledFrequency,
                           //LastName = c.LastName,
                       }).ToList();
            myList4.Insert(0, new ScheduledFrequencies { ScheduledFrequencyId = "0", ScheduledFrequency = "Select Frequency" });
            model.scheduledFrequencies = myList4;
            return View(model);
        }

    }
}
