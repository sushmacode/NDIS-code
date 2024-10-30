using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.BAL.Interfaces
{
   
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }
        IAdminDetailsRepository AdminDetails { get; }
        ICompanyRepository Companies { get; }
        IClientBudgetRepository ClientBudgets { get; }
        IProviderRepository Providers { get; }
        ISettingsRepository Settings { get; }
    }
}
