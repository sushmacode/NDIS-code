using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;
using NDIS.Entities;
using NDISBudget.Controllers;

namespace NDISBudget.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public MainController(ILogger<MainController> logger, IUnitOfWork context)
        {
            _logger = logger;
            unitOfWork = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AdminAddCompany modal = new AdminAddCompany();
            List<CompanyTypes> myList2 = new List<CompanyTypes>();
            myList2 = (from c in await unitOfWork.Companies.GetddlCompanyTypes()
                       select new CompanyTypes()
                       {
                           CompanyTypeId = c.CompanyTypeId,
                           CompanyType = c.CompanyType
                       }).ToList();
            myList2.Insert(0, new CompanyTypes { CompanyTypeId = 0, CompanyType = "Select Company Type" });
            modal.CompanyTypes = myList2;
            return View(modal);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdminAddCompany model)
        {
            ModelState.Remove("CompanyTypes");
            ModelState.Remove("CompanyTypes");
            if (ModelState.IsValid)
            {

                var config = new MapperConfiguration(cfg =>
                {
                    //Configuring Employee and EmployeeDTO
                    cfg.CreateMap<AdminAddCompany, CompanyEntity>();
                    //Any Other Mapping Configuration ....
                });
                //Create an Instance of Mapper and return that Instance
                var mapper = new Mapper(config);
                CompanyEntity c = mapper.Map<AdminAddCompany, CompanyEntity>(model);
                c.IsActive = 0;
                var res = await unitOfWork.Companies.AddCompanyAsync(c);
                if (res.CompanyId > 0)
                {
                    ViewBag.Message = "Company Created successfully, We Connect you soon..";
                    return RedirectToAction("Index", "Main");
                }
                //return Ok(mapper);
            }
            List<CompanyTypes> myList2 = new List<CompanyTypes>();
            myList2 = (from c in await unitOfWork.Companies.GetddlCompanyTypes()
                       select new CompanyTypes()
                       {
                           CompanyTypeId = c.CompanyTypeId,
                           CompanyType = c.CompanyType
                       }).ToList();
            myList2.Insert(0, new CompanyTypes { CompanyTypeId = 0, CompanyType = "Select Company Type" });
            model.CompanyTypes = myList2;
            return View("Index", model);
        }
        public IActionResult landingpage()
        {
            return View();
        }
    }
}
