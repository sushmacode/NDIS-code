using NDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.BAL.Interfaces
{
    public interface ISettingsRepository : IGenericRepository<SupportItem>
    {
        Task<List<GetSupportCategoryResponse>> GetAdminSupportCategoryList(GetSupportCategoryParams p);

        Task<AddSupportCategoryResponse> AddSupportCategoryAsync(SupportCategory c);
        Task<SupportCategory> GetCategoryByIdAsync(int c);
        Task<SupportItem> GetSupportItemByIdAsync(int v);
        Task<SupportItem> AddSupportItemAsync(SupportItem model);
        Task<List<GetSupportItemResponse>> GetAdminSupportItemList(GetSupportItemParams p);
        Task<int> DeleteCategoryAsync(int id);
        Task<int> DeleteItemAsync(int id);

        Task<List<GetSupportItemPriceListResponse>> GetAdminSupportItemPriceList(GetSupportItemPriceListParams p);

        Task<AddSupportItemPriceListResponse> AddSupportItemPriceAsync(SupportItemPriceList c);
        Task<SupportItemPriceList> GetSupportItemPriceByIdAsync(int c);
        Task<int> DeleteItemPriceAsync(int id);
        Task<DashboardData> GetDashboardDataIdAsync(int v);
        
        Task CopyItem(string oldid, int newid);
    }
}
