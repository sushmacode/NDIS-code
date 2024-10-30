using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using NDIS.BAL.Interfaces;
using NDIS.Entities;
using StackExchange.Redis;
using System.Configuration.Provider;
using System.Security.Claims;


namespace NDISBudget.Areas.Company.Controllers
{
    [Area("Company")]
    [Authorize(Roles = "Company")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClientController(ILogger<ClientController> logger, IUnitOfWork context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            unitOfWork = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {

            ViewBag.CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View();
        }

        [HttpGet]
        //[Authorize(Policy = "Company")]
        public async Task<IActionResult> Add(int? id=0)
        {
            GetAdminAddClient model = new GetAdminAddClient();
            if (id!=0) {
                Client c = await unitOfWork.Clients.GetByIdAsync(Convert.ToInt32(id));
                if(c!=null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        //Configuring Employee and EmployeeDTO
                        cfg.CreateMap< Client, GetAdminAddClient> ();
                        //Any Other Mapping Configuration ....
                    });
                    //Create an Instance of Mapper and return that Instance
                    var mapper = new Mapper(config);
                    GetAdminAddClient c1 = mapper.Map<Client,GetAdminAddClient>(c);
                    model = c1;
                }
            }
            List<AusState> myList2 = new List<AusState>();
            myList2 = (from c in await unitOfWork.Clients.GetddlStates()
                       select new AusState()
                       {
                           StateId = c.StateId,
                           StateName = c.StateName
                       }).ToList();
            myList2.Insert(0, new AusState { StateId = 0, StateName = "Select State" });
            model.States = myList2;
            model.CompanyId=Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(GetAdminAddClient model)
        {
            ModelState.Remove("Companies");
            ModelState.Remove("States");
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //Configuring Employee and EmployeeDTO
                    cfg.CreateMap<GetAdminAddClient, Client>();
                    //Any Other Mapping Configuration ....
                });
                //Create an Instance of Mapper and return that Instance
                var mapper = new Mapper(config);
                Client c= mapper.Map<GetAdminAddClient, Client>(model);
                var res = await unitOfWork.Clients.AddAsync(c);
                if(res>0)
                {
                    return RedirectToAction("Index", "Client", new { area = "Company" });
                }
                else
                {
                    ViewBag.message = "CLient already exist with mobile number.";
                }
                //return Ok(mapper);
            }
            List<AusState> myList2 = new List<AusState>();
            myList2 = (from c in await unitOfWork.Clients.GetddlStates()
                       select new AusState()
                       {
                           StateId = c.StateId,
                           StateName = c.StateName
                       }).ToList();
            myList2.Insert(0, new AusState { StateId = 0, StateName = "Select State" });
            model.States = myList2;
            model.CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View("Add",model);
        }

        public IActionResult Budget()
        {
            ViewBag.CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddBudget(int? id = 0)
        {
            var CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));

            AddUpdateAdminClientBudget model = new AddUpdateAdminClientBudget();
            if (id != 0)
            {
                ClientBudget c = await unitOfWork.ClientBudgets.GetByIdAsync(Convert.ToInt32(id));
                if (c != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        //Configuring Employee and EmployeeDTO
                        cfg.CreateMap<ClientBudget, AddUpdateAdminClientBudget>();
                        //Any Other Mapping Configuration ....
                    });
                    //Create an Instance of Mapper and return that Instance
                    var mapper = new Mapper(config);
                    AddUpdateAdminClientBudget c1 = mapper.Map<ClientBudget, AddUpdateAdminClientBudget>(c);
                    model = c1;
                    model.ProposedBudget = model.ProposedBudget > 0 ? Math.Round(Convert.ToDecimal(model.ProposedBudget), 2) : Convert.ToDecimal(0.00);
                }
            }
            model.CompanyId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Client> myList2 = new List<Client>();
            myList2 = (from c in await unitOfWork.Clients.GetCompanyClientsAsync(CompanyId)
                      select new Client()
                      {
                          ClientId = c.ClientId,
                          FirstName = c.FirstName,
                          LastName = c.LastName,
                      }).ToList();
            myList2.Insert(0, new Client { ClientId = 0, FirstName = "Select Participant" });
            model.Clients = myList2;

           

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
        public async Task<IActionResult> AddBudget(AddUpdateAdminClientBudget model)
        {
            var CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));

            ModelState.Remove("Clients");
            ModelState.Remove("Providers");
            ModelState.Remove("ProviderId");
            ModelState.Remove("SupportCategories");
            ModelState.Remove("States");
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //Configuring Employee and EmployeeDTO
                    cfg.CreateMap<AddUpdateAdminClientBudget, ClientBudget>();
                    //Any Other Mapping Configuration ....
                });
                //Create an Instance of Mapper and return that Instance
                var mapper = new Mapper(config);
                ClientBudget c = mapper.Map<AddUpdateAdminClientBudget, ClientBudget>(model);
                var res = await unitOfWork.ClientBudgets.AddAsync(c);
                if (res > 0)
                {
                    return RedirectToAction("Budget", "Client", new { area = "Company" });
                }
                //return Ok(mapper);
            }

            model.CompanyId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Client> myList2 = new List<Client>();
            myList2 = (from c in await unitOfWork.Clients.GetCompanyClientsAsync(CompanyId)
                       select new Client()
                       {
                           ClientId = c.ClientId,
                           FirstName = c.FirstName+' '+ c.LastName,
                           //LastName = c.LastName,
                       }).ToList();
            myList2.Insert(0, new Client { ClientId = 0, FirstName = "Select Client" });
            model.Clients = myList2;

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
            return View("AddBudget", model);
        }

        //[HttpGet("AddBudgetItem/{id}")]
        [HttpGet]
        public async Task<IActionResult> AddBudgetItem(int id = 0)
        {
            AddClientSupportItem model = new AddClientSupportItem();
            var res = await unitOfWork.ClientBudgets.GetClientBudgetDetails(id);
            model.ProposedBudget = res.ProposedBudget;
            model.UsedBudget = res.UsedBudget;
            model.RemainingBudget = res.RemainingBudget;
            model.StateId = res.StateId;
            model.StateName = res.StateName;
            model.CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.ClientBudgetId = Convert.ToInt32(id);
            List<SupportCategory> myList2 = new List<SupportCategory>();
            myList2 = (from c in await unitOfWork.ClientBudgets.GetddlSupportCategory()
                       select new SupportCategory()
                       {
                           SupportCategoryId = c.SupportCategoryId,
                           SupportCategoryName = c.SupportCategoryName
                       }).ToList();
            myList2.Insert(0, new SupportCategory { SupportCategoryId = 0, SupportCategoryName = "Select Support Category" });
            model.SupportCategories = myList2;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditBudgetItem(int? id = 0)
        {

             GetEditClientSupportItem model = new GetEditClientSupportItem();
            if (id != 0)
            {
                model = await unitOfWork.ClientBudgets.GetClientSupportItemByIdAsync(Convert.ToInt32(id));
                model.ItemBudget = model.ItemBudget > 0 ? Math.Round(Convert.ToDecimal(model.ItemBudget), 2) : Convert.ToDecimal(0.00);
                model.PricePerHour = model.PricePerHour > 0 ? Math.Round(Convert.ToDecimal(model.PricePerHour), 2) : Convert.ToDecimal(0.00);
                model.DayHoursCount = model.DayHoursCount > 0 ? model.DayHoursCount : 1;
                if (model.WeekDayIds != null)
                {
                    string[] weekarray = model.WeekDayIds.Split(',');
                    model.WeekArray = weekarray;
                }
                
            }

            // model.CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Provider> myList3 = new List<Provider>();
            myList3 = (from c in await unitOfWork.Clients.GetddlProvider()
                       select new Provider()
                       {
                           ProviderId = c.ProviderId,
                           ProviderName = c.ProviderName,
                           //LastName = c.LastName,
                       }).ToList();
            myList3.Insert(0, new Provider { ProviderId = 0, ProviderName = "Select Provider" });
            model.Providers = myList3;

            List<ScheduledFrequencies> myList4 = new List<ScheduledFrequencies>();
            myList4 = (from c in await unitOfWork.ClientBudgets.GetddlScheduledFrequency()
                       select new ScheduledFrequencies()
                       {
                           ScheduledFrequencyId = c.ScheduledFrequencyId + "_" + c.IsMultiWeekday,
                           ScheduledFrequency = c.ScheduledFrequency,
                           //LastName = c.LastName,
                       }).ToList();
            myList4.Insert(0, new ScheduledFrequencies { ScheduledFrequencyId = "0", ScheduledFrequency = "Select Frequency" });
            model.ScheduledFrequencies = myList4;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBudgetItem(GetEditClientSupportItem model)
        {
            ModelState.Remove("CreatedDate");
            ModelState.Remove("SupportItemName");
            ModelState.Remove("Providers");
            ModelState.Remove("ProviderId");
            ModelState.Remove("WeekArray");
            ModelState.Remove("ScheduledFrequencies");
            if (ModelState.IsValid)
            {
               int id = await unitOfWork.ClientBudgets.EditClientSupportItem(model);
                if(id==1)
                ViewBag.Message = "Support Item Updated Successfully.";
                return RedirectToAction("AddClientBudget", "Client", new { id = model.ClientBudgetId });
            }
           
            model = await unitOfWork.ClientBudgets.GetClientSupportItemByIdAsync(Convert.ToInt32(model.ClientSupportItemId));
            if (model.WeekDayIds != null)
            {
                string[] weekarray = model.WeekDayIds.Split(',');
                model.WeekArray = weekarray;
            }
            List<Provider> myList3 = new List<Provider>();
            myList3 = (from c in await unitOfWork.Clients.GetddlProvider()
                       select new Provider()
                       {
                           ProviderId = c.ProviderId,
                           ProviderName = c.ProviderName,
                           //LastName = c.LastName,
                       }).ToList();
            myList3.Insert(0, new Provider { ProviderId = 0, ProviderName = "Select Provider" });
            model.Providers = myList3;

            List<ScheduledFrequencies> myList4 = new List<ScheduledFrequencies>();
            myList4 = (from c in await unitOfWork.ClientBudgets.GetddlScheduledFrequency()
                       select new ScheduledFrequencies()
                       {
                           ScheduledFrequencyId = c.ScheduledFrequencyId+"_"+c.IsMultiWeekday,
                           ScheduledFrequency = c.ScheduledFrequency,
                           //LastName = c.LastName,
                       }).ToList();
            myList4.Insert(0, new ScheduledFrequencies { ScheduledFrequencyId = "0", ScheduledFrequency = "Select Frequency" });
            model.ScheduledFrequencies = myList4;

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddClientBudget(int? id = 0)
        {
            var CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));

            AddUpdateAdminClientBudget model = new AddUpdateAdminClientBudget();
            model.ClientId = 0;
            if (id != 0)
            {
                ClientBudget c = await unitOfWork.ClientBudgets.GetByIdAsync(Convert.ToInt32(id));
                if (c != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        //Configuring Employee and EmployeeDTO
                        cfg.CreateMap<ClientBudget, AddUpdateAdminClientBudget>();
                        //Any Other Mapping Configuration ....
                    });
                    //Create an Instance of Mapper and return that Instance
                    var mapper = new Mapper(config);
                    AddUpdateAdminClientBudget c1 = mapper.Map<ClientBudget, AddUpdateAdminClientBudget>(c);
                    model = c1;
                    model.ClientId=c1.ClientId;
                    model.ProposedBudget = model.ProposedBudget > 0 ? Math.Round(Convert.ToDecimal(model.ProposedBudget), 2) : Convert.ToDecimal(0.00);
                }
            }
            model.CompanyId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Client> myList2 = new List<Client>();
            myList2 = (from c in await unitOfWork.Clients.GetCompanyClientsdllAsync(CompanyId)
                       select new Client()
                       {
                           ClientId = c.ClientId,
                           FirstName = c.FirstName+" "+ c.LastName,
                           LastName = c.LastName,
                       }).ToList();
            myList2.Insert(0, new Client { ClientId = 0, FirstName = "New Participant" });
            model.Clients = myList2;



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
            model.States = myList4;


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddClientBudget(AddUpdateAdminClientBudget model)
        {
            var CompanyId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));

            ModelState.Remove("Clients");
            ModelState.Remove("Providers");
            ModelState.Remove("ProviderId");
            ModelState.Remove("SupportCategories");
            ModelState.Remove("States");
            if (model.ClientId != 0) 
            {
                ModelState.Remove("LastName");
                ModelState.Remove("FirstName");
                ModelState.Remove("MobileNumber");

            }

            var res1 = 0;
            if (ModelState.IsValid)
            {
                if (model.ClientId == 0)
                {
                    var config1 = new MapperConfiguration(cfg =>
                    {
                        //Configuring Employee and EmployeeDTO
                        cfg.CreateMap<AddUpdateAdminClientBudget, Client>();
                        //Any Other Mapping Configuration ....
                    });
                    //Create an Instance of Mapper and return that Instance
                    var mapper1 = new Mapper(config1);
                    Client c1 = mapper1.Map<AddUpdateAdminClientBudget, Client>(model);
                    res1 = await unitOfWork.Clients.AddAsync(c1);
                    
                    //return Ok(mapper);
                }

                var config = new MapperConfiguration(cfg =>
                {
                    //Configuring Employee and EmployeeDTO
                    cfg.CreateMap<AddUpdateAdminClientBudget, ClientBudget>();
                    //Any Other Mapping Configuration ....
                });
                //Create an Instance of Mapper and return that Instance
                var mapper = new Mapper(config);
                ClientBudget c = mapper.Map<AddUpdateAdminClientBudget, ClientBudget>(model);
                if (res1 > 0)
                {
                    c.ClientId = Convert.ToInt32(res1);
                }
                var res = await unitOfWork.ClientBudgets.AddClientBudgetAsync(c);
                if (res.Result > 0)
                {
                    return RedirectToAction("AddClientBudget", "Client", new { area = "Company", id = res.Result });
                }
                //return Ok(mapper);
            }

            model.CompanyId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Client> myList2 = new List<Client>();
            myList2 = (from c in await unitOfWork.Clients.GetCompanyClientsdllAsync(CompanyId)
                       select new Client()
                       {
                           ClientId = c.ClientId,
                           FirstName = c.FirstName + ' ' + c.LastName,
                           //LastName = c.LastName,
                       }).ToList();
            myList2.Insert(0, new Client { ClientId = 0, FirstName = "New Participant" });
            model.Clients = myList2;

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
            model.States = myList4;
            return View("AddClientBudget", model);
        }



    }
}
