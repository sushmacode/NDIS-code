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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IConfiguration configuration;
        //private readonly ILogger<ClientController> _logger;

        public CompanyRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<AddCompanyResponse> AddCompanyAsync(CompanyEntity entity)
        {
            entity.CreatedDate = DateTime.Now.ToString();
            //var sql = "Insert into Client(Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@CompanyId", entity.CompanyId);
                queryParameters1.Add("@CompanyName", entity.CompanyName);
                queryParameters1.Add("@CompanyTypeId", entity.CompanyTypeId);
                queryParameters1.Add("@IsActive", entity.IsActive);
                queryParameters1.Add("@Address", entity.Address);
                queryParameters1.Add("@EmailId", entity.EmailId);
                queryParameters1.Add("@ContactNumber", entity.ContactNumber);
                queryParameters1.Add("@UserName", entity.UserName);
                queryParameters1.Add("@Password", entity.Password);
                
                var result = await connection.QueryAsync<AddCompanyResponse>("Admin_AddUpdateCompany", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<List<GetAdminCompanyListResponse>> GetAdminCompanyList(GetAdminCompanyListParams entity)
        {
            //var sql = "SELECT * FROM Client";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var queryParameters1 = new DynamicParameters();
                queryParameters1.Add("@PageSize", entity.pgsize);
                queryParameters1.Add("@PageIndex", entity.pgindex);
                queryParameters1.Add("@SortBy", entity.sortby);
                queryParameters1.Add("@Searchstr", entity.str);
                var result = await connection.QueryAsync<GetAdminCompanyListResponse>("Admin_GetCompaniesList", queryParameters1, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<CompanyTypes>> GetddlCompanyTypes()
        {
            var sql = "SELECT * FROM Companytype";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<CompanyTypes>(sql);
                return result.ToList();
            }
        }
        public async Task<CompanyEntity> CheckCompanyLogin(CompanyEntity entity)
        {
            //entity.CreatedDate = DateTime.Now;
            var sql = "SELECT * FROM Companies WHERE UserName = @Username and Password=@pwd";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<CompanyEntity>(sql, new { Username = entity.UserName, pwd = entity.Password });
                return result;
            }
        }
        Task<int> IGenericRepository<CompanyEntity>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompanyEntity>> GetddlCompany()
        {
            var sql = "SELECT * FROM Companies";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<CompanyEntity>(sql);
                return result.ToList();
            }
        }
        public async Task<IReadOnlyList<CompanyEntity>> GetAllAsync()
        {
            var sql = "SELECT * FROM Companies";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<CompanyEntity>(sql);
                return result.ToList();
            }
        }

        public async Task<CompanyEntity>  GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Companies WHERE COmpanyId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<CompanyEntity>(sql, new { Id = id });
                return result;
            }
        }

        Task<int> IGenericRepository<CompanyEntity>.UpdateAsync(CompanyEntity entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<CompanyEntity>.AddAsync(CompanyEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
