using Common.Kafka.Interfaces;
using Common.Kafka.Model;
using Confluent.Kafka;
using Library.Application.DTO.Request;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Kafka.Consumer
{
    public class FineCollectionByIdConsumer : BackgroundService
    {
        private readonly IKafkaConsumer<string, IssueRequest> _consumer;
        public FineCollectionByIdConsumer(IKafkaConsumer<string , IssueRequest> kafkaConsumer) 
        {
            _consumer = kafkaConsumer;
        
        }
        ConsumerGroupIdModel model = new ConsumerGroupIdModel()
        {
            GroupId = "business-group",
            EnableAutoCommit = true,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            StatisticsIntervalMs = 5000,
            SessionTimeoutMs = 6000
        };
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _consumer.Consume(KafkaTopic.UserSignUpUpdate, stoppingToken, model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{(int)HttpStatusCode.InternalServerError} ConsumeFailedOnTopic - {KafkaTopic.UserSignUpUpdate}, {ex}");
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();

            base.Dispose();
        }

    }
}
