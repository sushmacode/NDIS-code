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
    internal class ClientRepository : IClientRepository
    {
        private readonly IConfiguration configuration;
        public ClientRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<List<AusState>> GetddlStates()
        {
            var sql = "SELECT * FROM ausstate";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<AusState>(sql);
                return result.ToList();
            }
        }
        public async Task<List<SupportItem>> GetddlNASupportItemBySCId(int id)
        {
            var sql = "select *from SupportItem where SupportCategoryId=@id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<SupportItem>(sql, new {id=id});
                return result.ToList();
            }
        }
        public async Task<List<Provider>> GetddlProvider()
        {
            var sql = "SELECT * FROM Provider";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Provider>(sql);
                return result.ToList();
            }
        }
        public async Task<List<GetAdminClientsListResponse>> GetAdminClientsList(GetAdminClientsListParams entity)
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
                queryParameters1.Add("@Status", entity.Status);
                var result = await connection.QueryAsync<GetAdminClientsListResponse>("Admin_GetClientsList", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

            public async Task<int> AddAsync(Client entity)
        {
            entity.CreatedDate = DateTime.Now;
            //var sql = "Insert into Client(Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ClientId", entity.ClientId);
                queryParameters1.Add("@CompanyId", entity.CompanyId);
                queryParameters1.Add("@StateId", entity.CompanyId);
                queryParameters1.Add("@FirstName", entity.FirstName);
                queryParameters1.Add("@LastName", entity.LastName);
                queryParameters1.Add("@DateOfBirth", entity.DateOfBirth);
                queryParameters1.Add("@GenderId", entity.GenderId);
                queryParameters1.Add("@MobileNumber", entity.MobileNumber);
                queryParameters1.Add("@EmailId", entity.EmailId);
                queryParameters1.Add("@ClientAddress", entity.ClientAddress);
                queryParameters1.Add("@IsActive", entity.IsActive);
                queryParameters1.Add("@SupportCoordinator", entity.SupportCoordinator);
                queryParameters1.Add("@SCCompany", entity.SCCompany);
                queryParameters1.Add("@SCPhone", entity.SCPhone);
                queryParameters1.Add("@SCEmail", entity.SCEmail);
                queryParameters1.Add("@PlanManager", entity.PlanManager);
                queryParameters1.Add("@PMPhone", entity.PMPhone);
                queryParameters1.Add("@PMEmail", entity.PMEmail);
                var result = await connection.QueryAsync<int>("AddClient", queryParameters1, commandType: CommandType.StoredProcedure);
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

        public async Task<IReadOnlyList<Client>> GetAllAsync()
        {
            var sql = "SELECT * FROM Client";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Client>(sql);
                return result.ToList();
            }
        }

        public async Task<List<Client>> GetCompanyClientsAsync(int? CompanyId)
        {
            var sql = "SELECT c.* FROM Client c where c.companyid=@id and c.clientId in (SELECT distinct [ClientId] FROM [dbo].[ClientBudget] where ClientBudgetId in (SELECT distinct [ClientBudgetId] FROM [dbo].[ClientSupportItemScheduleDetails])) order by c.firstname asc";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Client>(sql, new { Id = CompanyId });

                return result.ToList();
            }
        }

        public async Task<List<Client>> GetCompanyClientsdllAsync(int? CompanyId)
        {
            var sql = "SELECT c.* FROM Client c where c.companyid=@id order by c.firstname asc";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Client>(sql, new { Id = CompanyId });

                return result.ToList();
            }
        }


        public async Task<Client> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Client WHERE ClientId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Client>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Client entity)
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
    }
}
