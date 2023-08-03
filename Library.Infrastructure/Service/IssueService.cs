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
            try
            {
                var service = _factory.GetInstance<EIssueTable>();
               
                await service.AddAsync(issue);
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> DeleteIssuedService(int id)
        {
            try
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
            catch (Exception ex) { throw ex; }
        }

        public async Task<EIssueTable> GetIssuedServiceById(int id)
         {
            try
            {
                var service = _factory.GetInstance<EIssueTable>();
                var user = service.ListAsync().Result.FirstOrDefault(x =>  x.Id == id);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<EIssueTable>> GetIssuedServices()
        {
            try
            {
                var service = _factory.GetInstance<EIssueTable>();
                var result = await service.ListAsync();
                return result;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> UpdateIssuedService(EIssueTable issues)
        {
            try
            {
                var service = _factory.GetInstance<EIssueTable>();
                var issue = await service.FindAsync(issues.Id);
                if (issue == null)
                {
                    return false;
                }
                issue.ReturnDate = DateTime.Now.ToUniversalTime();
                issue.StudentId = issues.StudentId;
                issue.StaffId = issues.StaffId;
                issue.IssuedStatus = issues.IssuedStatus;
                issue.IssuedDate = issues.IssuedDate.ToUniversalTime();
                issue.BookId = issues.BookId;

                await service.UpdateAsync(issue);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
