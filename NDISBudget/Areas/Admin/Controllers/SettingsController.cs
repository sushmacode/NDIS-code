using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;
using NDIS.Data.Repository;
using NDIS.Entities;
using System.Security.Claims;

namespace NDISBudget.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SettingsController(ILogger<SettingsController> logger, IUnitOfWork context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            unitOfWork = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Items()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddItems(int? id = 0)
        {
            SupportItem model = new SupportItem();
            if (id != 0)
            {
                SupportItem c = await unitOfWork.Settings.GetSupportItemByIdAsync(Convert.ToInt32(id));
                model = c;
                if (HttpContext.Request.Query["Copy"].ToString()=="1")
                {
                    model.SupportItemId = 0;
                }
            }
            List<SupportCategory> myList3 = new List<SupportCategory>();
            myList3 = (from c1 in await unitOfWork.ClientBudgets.GetddlSupportCategory()
                       select new SupportCategory()
                       {
                           SupportCategoryId = c1.SupportCategoryId,
                           SupportCategoryName = c1.SupportCategoryName
                       }).ToList();
            myList3.Insert(0, new SupportCategory { SupportCategoryId = 0, SupportCategoryName = "Select Support Category" });
            model.SupportCategories = myList3;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddItems(SupportItem model)
        {
            ModelState.Remove("ItemPrice");
            ModelState.Remove("Usage");
            ModelState.Remove("Info");
            ModelState.Remove("SupportCategories");
            if (ModelState.IsValid)
            {
                var res = await unitOfWork.Settings.AddSupportItemAsync(model);
                if (res.SupportItemId > 0)
                {
                    if (HttpContext.Request.Query["Copy"].ToString() == "1")
                    {
                        var oldid = HttpContext.Request.Query["id"].ToString();
                        var newid = res.SupportItemId;
                        await unitOfWork.Settings.CopyItem(oldid, newid);
                    }
                    return RedirectToAction("Items", "Settings", new { area = "Admin" });
                }
            }
            List<SupportCategory> myList3 = new List<SupportCategory>();
            myList3 = (from c1 in await unitOfWork.ClientBudgets.GetddlSupportCategory()
                       select new SupportCategory()
                       {
                           SupportCategoryId = c1.SupportCategoryId,
                           SupportCategoryName = c1.SupportCategoryName
                       }).ToList();
            myList3.Insert(0, new SupportCategory { SupportCategoryId = 0, SupportCategoryName = "Select Support Category" });
            model.SupportCategories = myList3;
            return View(model);
        }

        public IActionResult Categories()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddCategories(int? id = 0)
        {
            SupportCategory model = new SupportCategory();
            model.SupportCategoryId = id;
            if (id != 0)
            {
                SupportCategory c = await unitOfWork.Settings.GetCategoryByIdAsync(Convert.ToInt32(id));
                model = c;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategories(SupportCategory model)
        {
            ModelState.Remove("TotalRecords");
            ModelState.Remove("CreatedDateStr");
            ModelState.Remove("Status");
            ModelState.Remove("CreatedDate");
            if (ModelState.IsValid)
            {
                var res = await unitOfWork.Settings.AddSupportCategoryAsync(model);
                if (res.SupportCategoryId > 0)
                {
                    return RedirectToAction("Categories", "Settings", new { area = "Admin" });
                }
            }
            return View(model);
        }

        public IActionResult Prices()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddPrices(int id = 0)
        {
            AddSupportItemPriceListParams model = new AddSupportItemPriceListParams();
            model.SupportItemPriceListId = id;
            if (id != 0)
            {
                SupportItemPriceList c = await unitOfWork.Settings.GetSupportItemPriceByIdAsync(Convert.ToInt32(id));
                if (c != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        //Configuring Employee and EmployeeDTO
                        cfg.CreateMap<SupportItemPriceList, AddSupportItemPriceListParams>();
                        //Any Other Mapping Configuration ....
                    });
                    //Create an Instance of Mapper and return that Instance
                    var mapper = new Mapper(config);
                    AddSupportItemPriceListParams c1 = mapper.Map<SupportItemPriceList, AddSupportItemPriceListParams>(c);
                    model = c1;
                    model.Price = model.Price > 0 ? Math.Round(Convert.ToDecimal(model.Price), 2) : Convert.ToDecimal(0.00);
                }
            }

            List<SupportCategory> myList3 = new List<SupportCategory>();
            myList3 = (from c1 in await unitOfWork.ClientBudgets.GetddlSupportCategory()
                       select new SupportCategory()
                       {
                           SupportCategoryId = c1.SupportCategoryId,
                           SupportCategoryName = c1.SupportCategoryName
                       }).ToList();
            myList3.Insert(0, new SupportCategory { SupportCategoryId = 0, SupportCategoryName = "Select Support Category" });
            model.SupportCategories = myList3;
            //return View(model);
            List<AusState> myList4 = new List<AusState>();
            myList4 = (from c in await unitOfWork.Clients.GetddlStates()
                       select new AusState()
                       {
                           StateId = c.StateId,
                           StateName = c.StateName
                       }).ToList();
            myList4.Insert(0, new AusState { StateId = 0, StateName = "Select State" });
            model.States = myList4;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPrices(AddSupportItemPriceListParams model)
        {
            ModelState.Remove("SupportCategories");
            ModelState.Remove("States");
            ModelState.Remove("SupportItemName");
            ModelState.Remove("SupportItemName");
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //Configuring Employee and EmployeeDTO
                    cfg.CreateMap<AddSupportItemPriceListParams, SupportItemPriceList>();
                    //Any Other Mapping Configuration ....
                });
                //Create an Instance of Mapper and return that Instance
                var mapper = new Mapper(config);
                SupportItemPriceList c = mapper.Map<AddSupportItemPriceListParams, SupportItemPriceList>(model);
                var res = await unitOfWork.Settings.AddSupportItemPriceAsync(c);
                if (res.SupportItemPriceListId > 0)
                {
                    return RedirectToAction("Prices", "Settings", new { area = "Admin" });
                }
                
            }

            List<SupportCategory> myList3 = new List<SupportCategory>();
            myList3 = (from c1 in await unitOfWork.ClientBudgets.GetddlSupportCategory()
                       select new SupportCategory()
                       {
                           SupportCategoryId = c1.SupportCategoryId,
                           SupportCategoryName = c1.SupportCategoryName
                       }).ToList();
            myList3.Insert(0, new SupportCategory { SupportCategoryId = 0, SupportCategoryName = "Select Support Category" });
            model.SupportCategories = myList3;
            //return View(model);
            List<AusState> myList4 = new List<AusState>();
            myList4 = (from c in await unitOfWork.Clients.GetddlStates()
                       select new AusState()
                       {
                           StateId = c.StateId,
                           StateName = c.StateName
                       }).ToList();
            myList4.Insert(0, new AusState { StateId = 0, StateName = "Select State" });
            model.States = myList4;

            return View(model);
        }

    }
}
