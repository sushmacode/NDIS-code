using NDIS.BAL.Interfaces;
using NDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IClientRepository clientRepository, 
            IAdminDetailsRepository adminDetailsRepository, 
            ICompanyRepository companyRepository,
            IClientBudgetRepository clientBudgetRepository,
            IProviderRepository providerRepository, ISettingsRepository settingsRepository
            )
        {
            Clients = clientRepository;
            AdminDetails = adminDetailsRepository;
            AdminDetails = adminDetailsRepository;
            Companies = companyRepository;
            ClientBudgets = clientBudgetRepository;
            Providers = providerRepository;
            Settings = settingsRepository;
        }
        public IClientRepository Clients { get; }
        public IAdminDetailsRepository AdminDetails { get; }
        public ICompanyRepository Companies { get; }
        public IClientBudgetRepository ClientBudgets { get; }
        public IProviderRepository Providers { get; }
        public ISettingsRepository Settings { get; }
    }
}
