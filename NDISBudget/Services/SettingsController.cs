using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NDIS.BAL.Interfaces;
using NDIS.Entities;

namespace NDISBudget.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public SettingsController(ILogger<SettingsController> logger, IUnitOfWork context)
        {
            _logger = logger;
            unitOfWork = context;
        }

        

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<GetSupportCategoryResponse>), 200)]
        [HttpPost("GetAdminCategoriyList")]
        public async Task<IActionResult> GetAdminCategoriyList(GetSupportCategoryParams p)
        {
            var data = await unitOfWork.Settings.GetAdminSupportCategoryList(p);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<GetSupportItemResponse>), 200)]
        [HttpPost("GetAdminSupportItemList")]
        public async Task<IActionResult> GetAdminSupportItemList(GetSupportItemParams p)
        {
            var data = await unitOfWork.Settings.GetAdminSupportItemList(p);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<GetSupportItemPriceListResponse>), 200)]
        [HttpPost("GetAdminSupportItemPriceList")]
        public async Task<IActionResult> GetAdminSupportItemPriceList(GetSupportItemPriceListParams p)
        {
            var data = await unitOfWork.Settings.GetAdminSupportItemPriceList(p);
            return Ok(data);
        }

        [HttpGet("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var data = await unitOfWork.Settings.DeleteCategoryAsync(id);
            return Ok(data);
        }

        [HttpGet("DeleteItem")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var data = await unitOfWork.Settings.DeleteCategoryAsync(id);
            return Ok(data);
        }

        

    }
}
