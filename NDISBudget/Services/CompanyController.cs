using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;
using NDIS.Entities;

namespace NDISBudget.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public CompanyController(ILogger<CompanyController> logger, IUnitOfWork context)
        {
            _logger = logger;
            unitOfWork = context;
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<GetAdminCompanyListResponse>), 200)]
        [HttpPost("GetAdminCompanyList")]
        public async Task<IActionResult> GetAdminCompanyList(GetAdminCompanyListParams p)
        {
            var data = await unitOfWork.Companies.GetAdminCompanyList(p);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(AddCompanyResponse), 200)]
        [HttpPost("AddCompany")]
        public async Task<IActionResult> GetAdminCompanyList(AddCompanyParams p)
        {
            var data = await unitOfWork.Companies.AddAsync(p);
            return Ok(data);
        }



    }
}
