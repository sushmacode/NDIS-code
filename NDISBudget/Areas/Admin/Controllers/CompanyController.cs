using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;
using NDIS.Entities;
using System.Security.Claims;

namespace NDISBudget.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public CompanyController(ILogger<CompanyController> logger, IUnitOfWork context)
        {
            _logger = logger;
            unitOfWork = context;
        }

        // GET: CompanyController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add(int? id = 0)
        {
            AdminAddCompany model = new AdminAddCompany();
            if (id != 0)
            {
                CompanyEntity c = await unitOfWork.Companies.GetByIdAsync(Convert.ToInt32(id));
                if (c != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        //Configuring Employee and EmployeeDTO
                        cfg.CreateMap<CompanyEntity, AdminAddCompany>();
                        //Any Other Mapping Configuration ....
                    });
                    //Create an Instance of Mapper and return that Instance
                    var mapper = new Mapper(config);
                    AdminAddCompany c1 = mapper.Map<CompanyEntity, AdminAddCompany>(c);
                    model = c1;
                }
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

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AdminAddCompany model)
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
                var res = await unitOfWork.Companies.AddCompanyAsync(c);
                if (res.CompanyId> 0)
                {
                    return RedirectToAction("Index", "Company", new { area = "Admin" });
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
            return View(model);
        }
    }
}
