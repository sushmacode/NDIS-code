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
    internal class ProviderRepository : IProviderRepository
    {
        private readonly IConfiguration configuration;

        public ProviderRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<GetAdminProviderListResponse>> GetAdminProviderList(GetAdminProviderListParams p)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@PageSize", p.pgsize);
                queryParameters1.Add("@PageIndex", p.pgindex);
                queryParameters1.Add("@SortBy", p.sortby);
                queryParameters1.Add("@Searchstr", p.str);
                var result = await connection.QueryAsync<GetAdminProviderListResponse>("Admin_GetProviderList", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<AddProviderResponse> AddProviderAsync(Provider entity)
        {
            //entity.CreatedDate = DateTime.Now.ToString();
            //var sql = "Insert into Client(Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@ProviderId", entity.ProviderId);
                queryParameters1.Add("@ProviderName", entity.ProviderName);
                queryParameters1.Add("@IsActive", entity.IsActive);
                queryParameters1.Add("@Address", entity.Address);
                queryParameters1.Add("@EmailId", entity.EmailId);
                queryParameters1.Add("@ContactNumber", entity.ContactNumber);
                var result = await connection.QueryAsync<AddProviderResponse>("Admin_AddUpdateProvider", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        Task<int> IGenericRepository<Provider>.AddAsync(Provider entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<Provider>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Provider>> IGenericRepository<Provider>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Provider> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Provider WHERE ProviderId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Provider>(sql, new { Id = id });
                return result;
            }
        }

        Task<int> IGenericRepository<Provider>.UpdateAsync(Provider entity)
        {
            throw new NotImplementedException();
        }
    }
}
