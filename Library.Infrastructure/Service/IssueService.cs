using Library.Domain.Entities;
using Library.Domain.Enum;
using Library.Domain.Interface;
using Library.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Service
{
    public class IssueService : IIssuedService
    {
        private readonly IServiceFactory _factory;
        private readonly IConfiguration _configuration;

        public IssueService(IServiceFactory factory/* IConfiguration configuration*/)
        {
            _factory = factory;

        }
        public async Task<bool> AddIssuedService(EIssueTable issue)
        {
            if (issue == null)
            {
                return false;
            }
            else
            {
                var service = _factory.GetInstance<EIssueTable>();
                await service.AddAsync(issue);
                return true;
            }

        }

        public async Task<bool> DeleteIssuedService(int id)
        {

            var service = _factory.GetInstance<EIssueTable>();
            var issue = await service.FindAsync(id);
            if (issue == null)
            {
                return false;
            }

            await service.RemoveAsync(issue);
            return true;

        }

        public async Task<EIssueTable> GetIssuedServiceById(int id)
        {
            var service = _factory.GetInstance<EIssueTable>();
            var user = service.ListAsync().Result.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (user == null)
            {
                return null;
            }
            return user;

        }

        public async Task<List<EIssueTable>> GetIssuedServices()
        {

            var service = _factory.GetInstance<EIssueTable>();
            var result = await service.ListAsync();

            return result;

        }

        public async Task<bool> UpdateIssuedService(EIssueTable issues)
        {

            var service = _factory.GetInstance<EIssueTable>();
            if (issues == null)
            {
                return false;
            }
            var issue = await service.FindAsync(issues.Id);
            
            //issue.ReturnDate = DateTime.Now.ToUniversalTime();
            issue.MemberId = issues.MemberId;
            issue.StaffId = issues.StaffId;
            issue.IssuedStatus = issues.IssuedStatus;
            issue.IssuedDate = issues.IssuedDate.ToUniversalTime();
            issue.BookId = issues.BookId;
            issue.IsDeleted = issues.IsDeleted;
            issue.ReturnDate = issues.ReturnDate;
            issue.FineRate = issues.FineRate;
            issue.FineAmount = issues.FineAmount;
         

            await service.UpdateAsync(issue);
            return true;

        }
    }
}
