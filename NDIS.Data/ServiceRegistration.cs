using Microsoft.Extensions.DependencyInjection;
using NDIS.BAL.Interfaces;
using NDIS.Data.Repository;

namespace NDIS.Data
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IAdminDetailsRepository, AdminDetailsRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IClientBudgetRepository, ClientBudgetRepository>();
            services.AddTransient<IProviderRepository, ProviderRepository>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
