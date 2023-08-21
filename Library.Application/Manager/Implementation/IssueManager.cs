using AutoMapper;
using AutoMapper.Execution;
using Common.Kafka.Interfaces;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Kafka.Interface;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Manager.Implementation
{
    public class IssueManager : IIssueManager
    {
		//HttpClient 
        Uri baseAddress = new Uri("https://localhost:44375"); //change this url to FineProject URL
        private readonly HttpClient _httpClient;

        private readonly IIssuedService _service;
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;
        private readonly ILogger<IssueManager> _logger;
        private readonly IAddIssueDetailsProducer _producer;


        public IssueManager()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IssueManager(IIssuedService issueService, IMapper mapper, ILogger<IssueManager> logger,IMemberService memberService, IAddIssueDetailsProducer producer)
        {
            _service = issueService;
            _mapper = mapper;
            _logger = logger;
            _memberService = memberService;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
            _producer = producer;

        }

        public async Task<ServiceResult<bool>> AddIssue(IssueRequest issueRequest, string Id)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                int memberType = await GetMemberTypeForMemberId(issueRequest.MemberId);

                if (memberType == 0 || memberType == 1)
                {
                    long? amount = await GetRateForMemberType(issueRequest);
                    double rate = Convert.ToDouble(amount);

                    var issueParser = _mapper.Map<EIssueTable>(issueRequest);
                    int userId = int.Parse(Id);

                    TimeSpan difference = issueRequest.ReturnDate - issueRequest.IssuedDate;
                    int days = difference.Days;

                    issueParser.StaffId = userId;
                    issueParser.FineRate = rate;
                    issueParser.IsDeleted = false;
                    issueParser.FineAmount = rate * days;
                    var result = await _service.AddIssuedService(issueParser);

                    //var parse2 = new EIssueTable()
                    //{
                    //    FineAmount = issueParser.FineAmount,
                    //    BookId = issueParser.BookId,
                    //    FineRate = rate,
                    //    Id = userId
                    //};
                    var issueParser2 = _mapper.Map<IssueProducerRequest>(issueParser);

                    //Kafka producer added
                    await _producer.AddIssue(issueParser2, Id);

					_logger.LogInformation("Book Issues added Successfully" + JsonConvert.SerializeObject(issueParser));

                    serviceResult.Status = result ? StatusType.Success : StatusType.Failure;
                    serviceResult.Message = result ? "Issues added successfully" : "Failed to add Issue";
                    serviceResult.Data = result;
                    return serviceResult;


                }
                else
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Member type can be either 0 or 1";
                    serviceResult.Data = false;

                    return serviceResult;

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Something Went wrong");
                serviceResult.Status = StatusType.Failure;
                serviceResult.Message = "An error occurred while adding the book issues. Exception occured";
                serviceResult.Data = false;
                return serviceResult;
            }
        }

        public async Task<ServiceResult<bool>> DeleteIssue(int id, string staffId)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                var issue = await _service.GetIssuedServiceById(id);
                if (issue == null)
                {
                    serviceResult.Status = StatusType.Failure;
                    serviceResult.Message = "Issues not found";
                    serviceResult.Data = false;

                    return serviceResult;
                }
                int userId = int.Parse(staffId);
                issue.StaffId = userId;
                issue.IsDeleted = true;
                var result = await _service.UpdateIssuedService(issue);

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
                var issue = await _service.GetIssuedServiceById(id);
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
                var nonDeletedIssues = issues.Where(issue => !issue.IsDeleted).ToList();
                var issueResponse = _mapper.Map<List<IssueResponse>>(nonDeletedIssues);

                serviceResult.Status = StatusType.Success;
                serviceResult.Message = "Non-deleted books issues retrieved successfully";
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
                long? amount = await GetRateForMemberType(issueRequest);
                double rate = Convert.ToDouble(amount);

                var issueParser = _mapper.Map<EIssueTable>(issueRequest);

                TimeSpan difference = issueRequest.ReturnDate - issueRequest.IssuedDate;
                int days = difference.Days;


                int userId = int.Parse(Id);
                issueParser.StaffId = userId;

                issueParser.FineRate = rate;
                issueParser.FineAmount = rate * days;

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

        //function to get rate of member from the memberType
        public async Task<long?> GetRateForMemberType(IssueRequest issueRequest)
        {
            int? memberType = await GetMemberTypeForMemberId(issueRequest.MemberId);

            if (memberType == 0 || memberType == 1)
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Fine/GetRateByMemberType?MemberType={memberType}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    long amount = responseObject;
                    return amount;

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public async Task<int> GetMemberTypeForMemberId(int id)
        {
            var issue = await _memberService.GetMemberById(id);
            if (issue == null)
            {
                return 0;
            }
            int memberType = Convert.ToInt16(issue.MemberType);
            return memberType;
        }



    }
}
