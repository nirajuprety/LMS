using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Interface
{
    public interface IIssueManager 
    {
        Task<ServiceResult<bool>> AddIssue(IssueRequest issueRequest, string Id);
        Task<ServiceResult<bool>> DeleteIssue(int id, string Id);
        Task<ServiceResult<IssueResponse>> GetIssueById(int id);
        Task<ServiceResult<List<IssueResponse>>> GetIssues();
        Task<ServiceResult<bool>> UpdateIssue(IssueRequest issueRequest, string id);
    }
}
