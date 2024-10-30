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
using static Dapper.SqlMapper;

namespace NDIS.Data.Repository
{
    public class ClientBudgetRepository : IClientBudgetRepository
    {
        private readonly IConfiguration configuration;
        public ClientBudgetRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<SupportCategory>> GetddlSupportCategory()
        {
            var sql = "SELECT * FROM SupportCategory where status=1";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<SupportCategory>(sql);
                return result.ToList();
            }
        }
        public async Task<List<GetAdminClientBudgetListResponse>> GetAdminClientBudgetList(GetAdminClientBudgetListParams entity)
        {
            //var sql = "SELECT * FROM Client";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@CompanyId", entity.CompanyId);
                queryParameters1.Add("@PageSize", entity.pgsize);
                queryParameters1.Add("@PageIndex", entity.pgindex);
                queryParameters1.Add("@SortBy", entity.sortby);
                queryParameters1.Add("@Searchstr", entity.str);
                var result = await connection.QueryAsync<GetAdminClientBudgetListResponse>("Admin_GetClientsBudgetList", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<GetAdminClientSupportItemsList>> GetAdminClientSupportItemsList(AdminClientSupportItemsParams entity)
        {
            //var sql = "SELECT * FROM Client";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientBudgetId", entity.ClientBudgetId);
                queryParameters1.Add("@PageSize", entity.pgsize);
                queryParameters1.Add("@PageIndex", entity.pgindex);
                queryParameters1.Add("@SortBy", entity.sortby);
                queryParameters1.Add("@Searchstr", entity.str);
                var result = await connection.QueryAsync<GetAdminClientSupportItemsList>("Admin_GetClientsSupportItemsList", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<GetAdminClientSupportItemsList>> GetClientBudgetItemsList(AdminClientSupportItemsParams entity)
        {
            //var sql = "SELECT * FROM Client";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientBudgetId", entity.ClientBudgetId);
                queryParameters1.Add("@PageSize", entity.pgsize);
                queryParameters1.Add("@PageIndex", entity.pgindex);
                queryParameters1.Add("@SortBy", entity.sortby);
                queryParameters1.Add("@Searchstr", entity.str);
                var result = await connection.QueryAsync<GetAdminClientSupportItemsList>("Admin_GetClientBudgetItemsList", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<AddEditAdminClientSupportItems>> GetAdminClientNASupportItemsList(AdminClientSupportItemsParams entity)
        {
            //var sql = "SELECT * FROM Client";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientBudgetId", entity.ClientBudgetId);
                queryParameters1.Add("@SCId", entity.SupportCategoryId);
                queryParameters1.Add("@PageSize", entity.pgsize);
                queryParameters1.Add("@PageIndex", entity.pgindex);
                queryParameters1.Add("@SortBy", entity.sortby);
                queryParameters1.Add("@Searchstr", entity.str);
                var result = await connection.QueryAsync<AddEditAdminClientSupportItems>("Admin_GetNotExistSupportItemsList", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> AddAsync(ClientBudget entity)
        {
            entity.CreatedDate = DateTime.Now.Date;
            //var sql = "Insert into Client(Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientBudgetId", entity.ClientBudgetId);
                queryParameters1.Add("@ClientId", entity.ClientId);
                queryParameters1.Add("@CompanyId", entity.CompanyId);
                queryParameters1.Add("@ProviderId", entity.ProviderId);
                queryParameters1.Add("@IsActive", entity.IsActive);
                queryParameters1.Add("@End", entity.EndDate);
                queryParameters1.Add("@Start", entity.StartDate);
                queryParameters1.Add("@NDISRefno", entity.NDISRefno);
                queryParameters1.Add("@ProposedBudget", entity.ProposedBudget);
                queryParameters1.Add("@StateId", entity.StateId);
                queryParameters1.Add("@Info", entity.Info);
                
                var result = await connection.QueryAsync<int>("AddClientBudget", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<BaseResponse> AddClientBudgetAsync(ClientBudget entity)
        {
            entity.CreatedDate = DateTime.Now.Date;
            //var sql = "Insert into Client(Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientBudgetId", entity.ClientBudgetId);
                queryParameters1.Add("@ClientId", entity.ClientId);
                queryParameters1.Add("@CompanyId", entity.CompanyId);
                queryParameters1.Add("@ProviderId", entity.ProviderId);
                queryParameters1.Add("@IsActive", entity.IsActive);
                queryParameters1.Add("@End", entity.EndDate);
                queryParameters1.Add("@Start", entity.StartDate);
                queryParameters1.Add("@NDISRefno", entity.NDISRefno);
                queryParameters1.Add("@ProposedBudget", entity.ProposedBudget);
                queryParameters1.Add("@StateId", entity.StateId);
                queryParameters1.Add("@Info", entity.Info);
                queryParameters1.Add("@SupportCoordinator", entity.SupportCoordinator);
                queryParameters1.Add("@SCCompany", entity.SCCompany);
                queryParameters1.Add("@SCPhone", entity.SCPhone);
                queryParameters1.Add("@SCEmail", entity.SCEmail);
                queryParameters1.Add("@PlanManager", entity.PlanManager);
                queryParameters1.Add("@PMPhone", entity.PMPhone);
                queryParameters1.Add("@PMEmail", entity.PMEmail);
                var result = await connection.QueryAsync<BaseResponse>("Company_AddClientBudget", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<GetSupportItemPriceResponse> GetSupportItemPrice(GetSupportItemPriceParam p)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@StateId", p.StateId);
                queryParameters1.Add("@SupportItemId", p.SupportItemId);
                var result = await connection.QueryAsync<GetSupportItemPriceResponse>("GetSupportItemPriceList", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
        public async Task<int> AddClientSupportItem(AddClientSupportItemParam entity)
        {
            //entity.CreatedDate = DateTime.Now.Date;
            //var sql = "Insert into Client(Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientSupportItemId", entity.ClientSupportItemId);
                queryParameters1.Add("@ClientBudgetId", entity.ClientBudgetId);
                queryParameters1.Add("@CompanyId", entity.CompanyId);
                queryParameters1.Add("@SupportItemId", entity.SupportItemId);
                queryParameters1.Add("@SupportCategoryId", entity.SupportCategoryId);
                //queryParameters1.Add("@End", entity.EndDate);
                //queryParameters1.Add("@Start", entity.StartDate);
                queryParameters1.Add("@ItemBudget", entity.ItemBudget);
                queryParameters1.Add("@PricePerHour", entity.PricePerHour);

                var result = await connection.QueryAsync<int>("Admin_AddClientSupportItem", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<int> EditClientSupportItem(GetEditClientSupportItem entity)
        {
            //entity.CreatedDate = DateTime.Now.Date;
            //var sql = "Insert into Client(Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientSupportItemId", entity.ClientSupportItemId);
                queryParameters1.Add("@IsActive", entity.IsActive);
                queryParameters1.Add("@ProviderId", entity.ProviderId);
                queryParameters1.Add("@EndDate", entity.EndDate);
                queryParameters1.Add("@StartDate", entity.StartDate);
                queryParameters1.Add("@ItemBudget", entity.ItemBudget);
                queryParameters1.Add("@PricePerHour", entity.PricePerHour);
                queryParameters1.Add("@SupportInfo", entity.SupportInfo);
                queryParameters1.Add("@SpecialConditions", entity.SpecialConditions);
                queryParameters1.Add("@WeekDayId", entity.WeekDayIds);
                queryParameters1.Add("@FrequencyId", entity.FrequencyId);
                queryParameters1.Add("@DayHoursCount", entity.DayHoursCount);
                queryParameters1.Add("@ShiftEndTime", entity.ShiftEndTime);
                queryParameters1.Add("@ShiftStartTime", entity.ShiftStartTime);

                var result = await connection.QueryAsync<int>("Admin_EditClientSupportItem", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "update Client set IsActive=0 WHERE ClientId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<ClientBudget>> GetAllAsync()
        {
            var sql = "SELECT * FROM Client";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ClientBudget>(sql);
                return result.ToList();
            }
        }

        public async Task<ClientBudget> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM ClientBudget WHERE ClientBudgetId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<ClientBudget>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<GetEditClientSupportItem> GetClientSupportItemByIdAsync(int id)
        {
            var sql = "SELECT csi.*,si.SupportItemName,si.DayMandatoryId FROM ClientSupportItem csi inner join SupportItem si on si.SupportItemId=csi.SupportItemId WHERE ClientSupportItemId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<GetEditClientSupportItem>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<AddClientSupportItem> GetClientBudgetDetails(int id)
        {
            //var sql = "SELECT * FROM ClientBudget WHERE ClientBudgetId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientBudgetId", id);
                var result = await connection.QueryAsync<AddClientSupportItem>("Admin_GetClientBudgetDetails", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<int> UpdateAsync(ClientBudget entity)
        {
            //entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<GetClientSupportItemScheduleResponse> GetClientSupportItemSchedule(int id)
        {
            //var sql = "SELECT * FROM ClientBudget WHERE ClientBudgetId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientSupportItemId", id);
                var result = await connection.QueryAsync<GetClientSupportItemScheduleResponse>("Company_GetClientSupportScheduleDetails", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<GetClientSupportItemScheduleResponse> GetClientBudgetSummary(int id)
        {
            //var sql = "SELECT * FROM ClientBudget WHERE ClientBudgetId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientBudgetId", id);
                var result = await connection.QueryAsync<GetClientSupportItemScheduleResponse>("Company_GetClientBudgetSummary", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<List<ClientSupportItemScheduleList>> GetClientBudgetItemsConfirmedScheduleList(ClientSupportItemScheduleParam entity)
        {

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@SupportItemId", entity.SupportItemId);
                queryParameters1.Add("@PageSize", entity.pgsize);
                queryParameters1.Add("@PageIndex", entity.pgindex);
                queryParameters1.Add("@SortBy", entity.sortby);
                queryParameters1.Add("@Searchstr", entity.str);
                var result = await connection.QueryAsync<ClientSupportItemScheduleList>("Company_BudgetSchedule", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<ClientSupportItemScheduleList>> GetClientSupportItemScheduleList(int id)
        {
            //var sql = "SELECT * FROM Client";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientSupportItemId", id);
                queryParameters1.Add("@GetType", 1);
               
                var result = await connection.QueryAsync<ClientSupportItemScheduleList>("Company_GetClientSupportBudget", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> ConfirmTempSchedule(ConfirmTempScheduleParam p)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientBudgetId", p.ClientBudgetId);
                queryParameters1.Add("@SupportItemId", p.SupportItemId);
                var result = await connection.ExecuteAsync("ConfirmTempSchedule", queryParameters1, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<List<ScheduledFrequencies>> GetddlScheduledFrequency()
        {
            var sql = "SELECT * FROM ScheduledFrequency";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ScheduledFrequencies>(sql);
                return result.ToList();
            }
        }

        public async Task<List<ClientBudget>> GetddlClientBudget(int id)
        {
            var sql = "SELECT * FROM ClientBudget WHERE ClientId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ClientBudget>(sql, new { Id = id });
                return result.ToList();
            }
        }

        public async Task<int> UpdateClientSupportItem(UpdateClientSupportItemParam entity)
        {
            //entity.CreatedDate = DateTime.Now.Date;
            //var sql = "Insert into Client(Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientSupportItemId", entity.ClientSupportItemId);
                queryParameters1.Add("@StartDate", entity.StartDate);
                queryParameters1.Add("@EndDate", entity.EndDate);
                queryParameters1.Add("@FrequencyId", entity.FrequencyId);
                queryParameters1.Add("@WeekArray", entity.WeekArray);
                var result = await connection.QueryAsync<int>("Company_UpdateClientSupportItem", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<int> AddClientSupportCategory(AddCategoryPriceParam entity)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@CategoryPrice", entity.CategoryPrice);
                queryParameters1.Add("@SupportCategoryId", entity.SupportCategoryId);
                queryParameters1.Add("@ClientBudgetId", entity.ClientBudgetId);
               
                var result = await connection.QueryAsync<int>("AddClientSupportCategory", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<List<AdminSupportCategoryResponse>> GetClientSupportCategoryBudgetList(AdminClientSupportCategoryParams entity)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientBudgetId", entity.ClientBudgetId);
                queryParameters1.Add("@PageSize", entity.pgsize);
                queryParameters1.Add("@PageIndex", entity.pgindex);
                queryParameters1.Add("@SortBy", entity.sortby);
                queryParameters1.Add("@Searchstr", entity.str);
                var result = await connection.QueryAsync<AdminSupportCategoryResponse>("Admin_GetClientSupportCategoryBudgetList", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        
    }
}
