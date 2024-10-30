using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NDIS.BAL.Interfaces;
using NDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Data.Repository
{
    internal class AdminDetailsRepository : IAdminDetailsRepository
    {
        private readonly IConfiguration configuration;
        public AdminDetailsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<AdminDetails> CheckAdminLogin(AdminDetails entity)
        {
            entity.CreatedDate = DateTime.Now;
            var sql = "SELECT * FROM AdminDetails WHERE Username = @Username and Pwd=@pwd";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<AdminDetails>(sql, new { Username = entity.Username, pwd =entity.Pwd});
                return result;
            }
        }

        Task<int> IGenericRepository<AdminDetails>.AddAsync(AdminDetails entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<AdminDetails>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<AdminDetails>> IGenericRepository<AdminDetails>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<AdminDetails> IGenericRepository<AdminDetails>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<AdminDetails>.UpdateAsync(AdminDetails entity)
        {
            throw new NotImplementedException();
        }
    }
}
