using Common.Kafka.Interfaces;
using Library.Application.DTO.Request;
using Library.Application.Kafka.Interface;
using Library.Application.Kafka.Topic;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Kafka.Producer
{
	public class AddIssueDetailsProducer : IAddIssueDetailsProducer
	{
		private readonly IKafkaProducer<string, EIssueTable> _kafkaProducer;
		public AddIssueDetailsProducer(IKafkaProducer<string, EIssueTable> kafkaProducer)
		{
			_kafkaProducer = kafkaProducer;
		}

		public async Task AddIssue(EIssueTable eIssueRequest,string Id)
		{
			await _kafkaProducer.ProduceAsync(KafkaTopic.FineCollection, null , eIssueRequest);
		}
	}
}
