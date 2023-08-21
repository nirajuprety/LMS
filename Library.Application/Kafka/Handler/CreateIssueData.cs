using Common.Kafka.Interfaces;
using Library.Application.DTO.Request;
using Library.Application.Manager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Kafka.Handler
{
    public class CreateIssueData : IKafkaHandler<string, IssueRequest>
    {
        private readonly IIssueManager _manager;

        public CreateIssueData(IIssueManager manager)
        {
            _manager = manager;
        }
        public Task HandleAsync(string key, IssueRequest issueRequest) 
        {
            //_manager.AddIssue(issueRequest);
            return Task.CompletedTask;
        }
    }
}
