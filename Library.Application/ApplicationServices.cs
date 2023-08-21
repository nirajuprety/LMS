using Common.Kafka.Interfaces;
using Common.Kafka.Producer;
using Confluent.Kafka;
using Library.Application.Kafka.Interface;
using Library.Application.Kafka.Producer;
using Library.Application.Manager.Implementation;
using Library.Application.Manager.Interface;
using Library.Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddInApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
			var clientConfig = new ClientConfig()
			{
				SaslUsername = Configuration["KafkaConfig:SaslUsername"],
				BootstrapServers = Configuration["KafkaConfig:BootstrapServers"],
				SaslPassword = Configuration["KafkaConfig:SaslPassword"],
				SaslMechanism = SaslMechanism.Plain,
				SecurityProtocol = SecurityProtocol.SaslSsl,
				EnableSslCertificateVerification = false // to de force ssl in local 
			};
			services.AddScoped<IStudentManager, StudentManager>();
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<IStaffManager, StaffManager>();
            services.AddScoped<IMemberManager, MemberManager>();

			var producerConfig = new ProducerConfig(clientConfig);
			services.AddSingleton(producerConfig);

			services.AddSingleton(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));
			services.AddScoped<IIssueManager, IssueManager>();
			services.AddScoped<IAddIssueDetailsProducer, AddIssueDetailsProducer>();

            return services;
        }
    }
}
