using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NDIS.Entities;
using NDISBudget.Entities;
namespace NDIS.BAL.Interfaces
{
    public interface IClientBudgetRepository : IGenericRepository<ClientBudget>
    {
        Task<List<GetAdminClientBudgetListResponse>> GetAdminClientBudgetList(GetAdminClientBudgetListParams p);
        Task<List<GetAdminClientSupportItemsList>> GetAdminClientSupportItemsList(AdminClientSupportItemsParams p);
        Task<List<AddEditAdminClientSupportItems>> GetAdminClientNASupportItemsList(AdminClientSupportItemsParams p);
       
        Task<int> AddClientSupportItem(AddClientSupportItemParam p);
        Task<AddClientSupportItem> GetClientBudgetDetails(int id);
        Task<GetEditClientSupportItem> GetClientSupportItemByIdAsync(int id);
        Task<int> EditClientSupportItem(GetEditClientSupportItem entity);

        Task<GetClientSupportItemScheduleResponse> GetClientSupportItemSchedule(int id);

        Task<List<ClientSupportItemScheduleList>> GetClientSupportItemScheduleList(int id);

        Task<List<SupportCategory>> GetddlSupportCategory();
        Task<List<ClientBudget>> GetddlClientBudget(int id);
        Task<List<ScheduledFrequencies>> GetddlScheduledFrequency();

        Task<int> ConfirmTempSchedule(ConfirmTempScheduleParam p);
        Task<GetSupportItemPriceResponse> GetSupportItemPrice(GetSupportItemPriceParam p);
        Task<List<ClientSupportItemScheduleList>> GetClientBudgetItemsConfirmedScheduleList(ClientSupportItemScheduleParam id);
        Task<BaseResponse> AddClientBudgetAsync(ClientBudget c);
        Task<int> UpdateClientSupportItem(UpdateClientSupportItemParam p);
        Task<List<GetAdminClientSupportItemsList>> GetClientBudgetItemsList(AdminClientSupportItemsParams p);
        Task<GetClientSupportItemScheduleResponse> GetClientBudgetSummary(int id);
        Task<int> AddClientSupportCategory(AddCategoryPriceParam p);
        Task<List<AdminSupportCategoryResponse>> GetClientSupportCategoryBudgetList(AdminClientSupportCategoryParams p);
        
    }
}
