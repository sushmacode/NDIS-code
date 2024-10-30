using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;
using NDIS.Entities;

namespace NDISBudget.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ILogger<ProviderController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public ProviderController(ILogger<ProviderController> logger, IUnitOfWork context)
        {
            _logger = logger;
            unitOfWork = context;
        }

        //[ProducesResponseType(400)]
        //[ProducesResponseType(500)]
        //[ProducesResponseType(typeof(List<GetAdminProviderListResponse>), 200)]
        //[HttpPost("GetAdminProviderList")]
        //public async Task<IActionResult> GetAdminProviderList(GetAdminProviderListParams p)
        //{
        //    var data = await unitOfWork.Providers.GetAdminProviderList(p);
        //    return Ok(data);
        //}
    }
}
