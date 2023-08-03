using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Implementation
{
    public class IssueManager : IIssueManager
    {
        private readonly IIssuedService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<IssueManager> _logger;


        public IssueManager(IIssuedService issueService, IMapper mapper, ILogger<IssueManager> logger)
        {
            _service = issueService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<bool>> AddIssue(IssueRequest issueRequest, string Id)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                //var parse = new EBook()
                //{
                //    Id = bookRequest.Id,
                //    Title = bookRequest.Title,
                //    Author = bookRequest.Author,
                //    ISBN = bookRequest.ISBN,
                //    PublicationDate = bookRequest.PublicationDate,
                //    UpdatedBy = bookRequest.UpdatedBy
                //};
                var issueParser =  _mapper.Map<EIssueTable>(issueRequest);
                int userId = int.Parse(Id);
                issueParser.StaffId = userId;
                var result = await _service.AddIssuedService(issueParser);

                _logger.LogInformation("Book Issues added Successfully" + JsonConvert.SerializeObject(issueParser));

                serviceResult.Status = result ? StatusType.Success : StatusType.Failure;
                serviceResult.Message = result ? "Issues added successfully" : "Failed to add Issue";
                serviceResult.Data = result;
                return serviceResult;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Something Went wrong");
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while adding the book issues";
                serviceResult.Data = false;
                return serviceResult;
            }
        }

        public async Task<ServiceResult<bool>> DeleteIssue(int id, string staffId)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                var issue= await _service.GetIssuedServiceById(id);
                if (issue == null)
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Issues not found";
                    serviceResult.Data = false;

                    return serviceResult;
                }
                int userId = int.Parse(staffId);
                issue.StaffId = userId;
                var result = await _service.DeleteIssuedService(id);

                serviceResult.Status = result ? StatusType.Success : StatusType.Failure;
                serviceResult.Message = result ? "Issue deleted successfully" : "Failed to delete issue";
                serviceResult.Data = result;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while deleting the book issues";
                serviceResult.Data = false;

                return serviceResult;
            }
        }

        public async Task<ServiceResult<IssueResponse>> GetIssueById(int id)
        {
            var serviceResult = new ServiceResult<IssueResponse>();

            try
            {
                var issue= await _service.GetIssuedServiceById(id);
                if (issue == null)
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Book issues not found";
                    serviceResult.Data = null;

                    return serviceResult;
                }

                var issueResponse = _mapper.Map<IssueResponse>(issue);

                serviceResult.Status = StatusType.Success;
                serviceResult.Message = "Book retrieved successfully";
                serviceResult.Data = issueResponse;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while retrieving the book issues";
                serviceResult.Data = null;

                return serviceResult;
            }
        }

        public async Task<ServiceResult<List<IssueResponse>>> GetIssues()
        {
            var serviceResult = new ServiceResult<List<IssueResponse>>();

            try
            {
                var issues = await _service.GetIssuedServices();
                var issueResponse = _mapper.Map<List<IssueResponse>>(issues);

                serviceResult.Status = StatusType.Success;
                serviceResult.Message = "Books issues retrieved successfully";
                serviceResult.Data = issueResponse;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while retrieving the b ooks issues";
                serviceResult.Data = null;

                return serviceResult;
            }
        }

        public async Task<ServiceResult<bool>> UpdateIssue(IssueRequest issueRequest, string Id)
        {
            var serviceResult = new ServiceResult<bool>();

            try
             {
                var issue = await _service.GetIssuedServiceById(issueRequest.Id);
                if (issue == null)
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Book issue not found";
                    serviceResult.Data = false;

                    return serviceResult;
                }
                var issueParser = _mapper.Map<EIssueTable>(issueRequest);


                int userId = int.Parse(Id);
                issueParser.StaffId = userId;
                var result = await _service.UpdateIssuedService(issueParser);

                serviceResult.Status = result ? StatusType.Success : StatusType.Failure;
                serviceResult.Message = result ? "Book issue updated successfully" : "Failed to update book issue ";
                serviceResult.Data = result;

                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while updating the book issue ";
                serviceResult.Data = false;

                return serviceResult;
            }
        }
    }
}
