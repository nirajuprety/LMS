using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Interface
{
    public interface IIssuedService
    {
        Task<bool> AddIssuedService(EIssueTable issue);
        Task<List<EIssueTable>> GetIssuedServices();
        Task<EIssueTable> GetIssuedServiceById(int id);
        Task<bool> UpdateIssuedService(EIssueTable eBook);
        Task<bool> DeleteIssuedService(int id);
      
    }
}
