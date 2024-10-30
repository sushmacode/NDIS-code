using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDIS.BAL.Interfaces;
using NDIS.Entities;
using System.Threading.Tasks;

namespace NDISBudget.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public ClientController(ILogger<ClientController> logger, IUnitOfWork context)
        {
            _logger = logger;
            unitOfWork = context;
        }


        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.Clients.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("GetClientById")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var data = await unitOfWork.Clients.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Clients.DeleteAsync(id);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<GetAdminClientsListResponse>), 200)]
        [HttpPost("GetAdminClientsList")]
        public async Task<IActionResult> GetAdminClientsList(GetAdminClientsListParams p)
        {
            var data = await unitOfWork.Clients.GetAdminClientsList(p);
            return Ok(data);
        }

        [HttpGet("DeleteBudget")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var data = await unitOfWork.ClientBudgets.DeleteAsync(id);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<GetAdminClientsListResponse>), 200)]
        [HttpPost("GetAdminClientBudgetsList")]
        public async Task<IActionResult> GetAdminClientBudgetsList(GetAdminClientBudgetListParams p)
        {
            var data = await unitOfWork.ClientBudgets.GetAdminClientBudgetList(p);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<GetAdminClientsListResponse>), 200)]
        [HttpPost("GetAdminClientSupportItemsList")]
        public async Task<IActionResult> GetAdminClientSupportItemsList(AdminClientSupportItemsParams p)
        {
            var data = await unitOfWork.ClientBudgets.GetAdminClientSupportItemsList(p);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<GetAdminClientsListResponse>), 200)]
        [HttpPost("GetClientBudgetItemsList")]
        public async Task<IActionResult> GetClientBudgetItemsList(AdminClientSupportItemsParams p)
        {
            var data = await unitOfWork.ClientBudgets.GetClientBudgetItemsList(p);
            return Ok(data);
        }

        

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<GetAdminClientsListResponse>), 200)]
        [HttpPost("GetAdminClientNASupportItemsList")]
        public async Task<IActionResult> GetAdminClientNASupportItemsList(AdminClientSupportItemsParams p)
        {
            var data = await unitOfWork.ClientBudgets.GetAdminClientNASupportItemsList(p);
            return Ok(data);
        }



        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpPost("AddClientSupportItem")]
        public async Task<IActionResult> AddClientSupportItem(AddClientSupportItemParam p)
        {
            var data = await unitOfWork.ClientBudgets.AddClientSupportItem(p);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpPost("UpdateClientSupportItem")]
        public async Task<IActionResult> UpdateClientSupportItem(UpdateClientSupportItemParam p)
        {
            var data = await unitOfWork.ClientBudgets.UpdateClientSupportItem(p);
            return Ok(data);
        }


        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpGet("GetClientBudgetDetails")]
        public async Task<IActionResult> GetClientBudgetDetails(int id=0)
        {
            var data = await unitOfWork.ClientBudgets.GetClientBudgetDetails(id);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpGet("GetClientSupportItemScheduleList")]
        public async Task<IActionResult> GetClientSupportItemScheduleList(int id = 0)
        {
           var data = await unitOfWork.ClientBudgets.GetClientSupportItemScheduleList(id);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpPost("GetClientBudgetItemsConfirmedScheduleList")]
        public async Task<IActionResult> GetClientBudgetItemsConfirmedScheduleList(ClientSupportItemScheduleParam id)
        {
            var data = await unitOfWork.ClientBudgets.GetClientBudgetItemsConfirmedScheduleList(id);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpGet("GetddlNASupportItemBySCId")]
        public async Task<IActionResult> GetddlNASupportItemBySCId(int id = 0)
        {
            var data = await unitOfWork.Clients.GetddlNASupportItemBySCId(id);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpPost("ConfirmTempSchedule")]
        public async Task<IActionResult> ConfirmTempSchedule(ConfirmTempScheduleParam p)
        {
            var data = await unitOfWork.ClientBudgets.ConfirmTempSchedule(p);
            return Ok(data);
        }
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpPost("GetSupportItemPrice")]
        public async Task<IActionResult> GetSupportItemPrice(GetSupportItemPriceParam p)
        {
            var data = await unitOfWork.ClientBudgets.GetSupportItemPrice(p);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpPost("AddClientSupportCategory")]
        public async Task<IActionResult> AddClientSupportCategory(AddCategoryPriceParam p)
        {
            var data = await unitOfWork.ClientBudgets.AddClientSupportCategory(p);
            return Ok(data);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(List<AdminSupportCategoryResponse>), 200)]
        [HttpPost("GetClientSupportCategoryBudgetList")]
        public async Task<IActionResult> GetClientSupportCategoryBudgetList(AdminClientSupportCategoryParams p)
        {
            var data = await unitOfWork.ClientBudgets.GetClientSupportCategoryBudgetList(p);
            return Ok(data);
        }



        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpGet("GetddlClientBudgetById")]
        public async Task<IActionResult> GetddlClientBudgetById(int id = 0)
        {
            var data = await unitOfWork.ClientBudgets.GetddlClientBudget(id);
            return Ok(data);
        }

       

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(int), 200)]
        [HttpGet("GetClientBudgetSummary")]
        public async Task<IActionResult> GetClientBudgetSummary(int id = 0)
        {
            var data = await unitOfWork.ClientBudgets.GetClientBudgetSummary(id);
            return Ok(data);
        }

    }
}
