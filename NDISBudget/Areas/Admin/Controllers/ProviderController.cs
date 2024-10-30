using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;
using NDIS.Entities;

namespace NDISBudget.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProviderController : Controller
    {
        private readonly ILogger<ProviderController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public ProviderController(ILogger<ProviderController> logger, IUnitOfWork context)
        {
            _logger = logger;
            unitOfWork = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //public async Task<IActionResult> Add(int? id = 0)
        //{
        //    AdminAddProvider model = new AdminAddProvider();
        //    if (id != 0)
        //    {
        //        Provider c = await unitOfWork.Providers.GetByIdAsync(Convert.ToInt32(id));
        //        if (c != null)
        //        {
        //            var config = new MapperConfiguration(cfg =>
        //            {
        //                //Configuring Employee and EmployeeDTO
        //                cfg.CreateMap<Provider, AdminAddProvider>();
        //                //Any Other Mapping Configuration ....
        //            });
        //            //Create an Instance of Mapper and return that Instance
        //            var mapper = new Mapper(config);
        //            AdminAddProvider c1 = mapper.Map<Provider, AdminAddProvider>(c);
        //            model = c1;
        //        }
        //    }
           

        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Add(AdminAddProvider model)
        //{
            
        //    if (ModelState.IsValid)
        //    {
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            //Configuring Employee and EmployeeDTO
        //            cfg.CreateMap<AdminAddProvider, Provider>();
        //            //Any Other Mapping Configuration ....
        //        });
        //        //Create an Instance of Mapper and return that Instance
        //        var mapper = new Mapper(config);
        //        Provider c = mapper.Map<AdminAddProvider, Provider>(model);
        //        var res = await unitOfWork.Providers.AddProviderAsync(c);
        //        if (res.ProviderId > 0)
        //        {
        //            return RedirectToAction("Index", "Provider", new { area = "Admin" });
        //        }
        //        //return Ok(mapper);
        //    }
        //    return View("Add", model);
        //}

    }
}
