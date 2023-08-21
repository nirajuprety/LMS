using Library.Application.DTO.Request;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;

namespace Library.Application.Kafka.Interface
{
	public interface IAddIssueDetailsProducer
	{
		Task AddIssue(EIssueTable issueRequest, string Id);
	}
}
