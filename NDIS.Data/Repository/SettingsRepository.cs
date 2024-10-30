using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NDIS.BAL.Interfaces;
using NDIS.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Data.Repository
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IConfiguration configuration;

        public SettingsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        Task<int> IGenericRepository<SupportItem>.AddAsync(SupportItem entity)
        {
            throw new NotImplementedException();
        }

        public async Task<AddSupportCategoryResponse> AddSupportCategoryAsync(SupportCategory c)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@SupportCategoryId", c.SupportCategoryId);
                queryParameters1.Add("@SupportCategoryName", c.SupportCategoryName);
                queryParameters1.Add("@IsActive", c.IsActive);
                var result = await connection.QueryAsync<AddSupportCategoryResponse>("Company_AddSupportCategory", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        Task<int> IGenericRepository<SupportItem>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetSupportCategoryResponse>> GetAdminSupportCategoryList(GetSupportCategoryParams p)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@PageSize", p.pgsize);
                queryParameters1.Add("@PageIndex", p.pgindex);
                queryParameters1.Add("@SortBy", p.sortby);
                queryParameters1.Add("@Searchstr", p.str);
                var result = await connection.QueryAsync<GetSupportCategoryResponse>("Company_GetGetSupportCategory", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        Task<IReadOnlyList<SupportItem>> IGenericRepository<SupportItem>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<SupportItem> IGenericRepository<SupportItem>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<SupportItem>.UpdateAsync(SupportItem entity)
        {
            throw new NotImplementedException();
        }

        public async Task<SupportCategory> GetCategoryByIdAsync(int id)
        {
            var sql = "SELECT * FROM [SupportCategory] WHERE [SupportCategoryId] = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<SupportCategory>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<SupportItem> GetSupportItemByIdAsync(int id)
        {
            var sql = "SELECT * FROM [SupportItem] WHERE [SupportItemId] = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<SupportItem>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<SupportItem> AddSupportItemAsync(SupportItem model)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@SupportItemId", model.SupportItemId);
                queryParameters1.Add("@SupportItemName", model.SupportItemName);
                queryParameters1.Add("@CustomName", model.CustomName);
                queryParameters1.Add("@SupportItemNumber", model.SupportItemNumber);
                queryParameters1.Add("@SupportCategoryId", model.SupportCategoryId);
                queryParameters1.Add("@DayMandatoryId", model.DayMandatoryId);
                queryParameters1.Add("@Info", model.Info);
                queryParameters1.Add("@IsActive", model.Status);
               
                var result = await connection.QueryAsync<SupportItem>("Company_AddSupportItem", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<List<GetSupportItemResponse>> GetAdminSupportItemList(GetSupportItemParams p)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@PageSize", p.pgsize);
                queryParameters1.Add("@PageIndex", p.pgindex);
                queryParameters1.Add("@SortBy", p.sortby);
                queryParameters1.Add("@Searchstr", p.str);
                var result = await connection.QueryAsync<GetSupportItemResponse>("Company_GetSupportItem", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            var sql = "update SupportCategory set Status=0 WHERE SupportCategoryId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            var sql = "update SupportItem set Status=0 WHERE SupportItemId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<List<GetSupportItemPriceListResponse>> GetAdminSupportItemPriceList(GetSupportItemPriceListParams p)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@PageSize", p.pgsize);
                queryParameters1.Add("@PageIndex", p.pgindex);
                queryParameters1.Add("@SortBy", p.sortby);
                queryParameters1.Add("@Searchstr", p.str);
                var result = await connection.QueryAsync<GetSupportItemPriceListResponse>("Company_SupportItemPrice", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<AddSupportItemPriceListResponse> AddSupportItemPriceAsync(SupportItemPriceList c)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@SupportItemPriceId", c.SupportItemPriceListId);
                queryParameters1.Add("@SupportItemId", c.SupportItemId);
                queryParameters1.Add("@StateId", c.StateId);
                queryParameters1.Add("@Price", c.Price);
                queryParameters1.Add("@IsActive", c.Status);
                var result = await connection.QueryAsync<AddSupportItemPriceListResponse>("Company_AddSupportItemPrice", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<SupportItemPriceList> GetSupportItemPriceByIdAsync(int id)
        {
            var sql = "SELECT sip.*,si.SupportItemName,s.StateId,s.StateName,s.RegionId,si.SupportCategoryId FROM [SupportItemPriceList] sip inner join [dbo].[SupportItem] si on si.[SupportItemId]=sip.[SupportItemId] inner join [dbo].[AusState] s on s.StateId=sip.StateId  WHERE [SupportItemPriceListId] = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<SupportItemPriceList>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> DeleteItemPriceAsync(int id)
        {
            var sql = "update SupportItemPriceList set Status=0 WHERE SupportItemId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<DashboardData> GetDashboardDataIdAsync(int v)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@CompanyId", v);
                var result = await connection.QueryAsync<DashboardData>("GetDashboardSummaryDetailsByCompany", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task CopyItem(string oldid, int newid)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@item", oldid);
                queryParameters1.Add("@newitem", newid);
                var result = await connection.QueryAsync<DashboardData>("Company_CopyItemPrices", queryParameters1, commandType: CommandType.StoredProcedure);
                
            }
        }
    }
}
