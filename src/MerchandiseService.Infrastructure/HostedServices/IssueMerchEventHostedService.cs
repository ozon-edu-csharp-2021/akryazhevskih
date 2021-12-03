using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using MediatR;
using MerchandiseService.HttpModels;
using MerchandiseService.Infrastructure.Commands.CreateMerch;
using MerchandiseService.Infrastructure.Configuration.KafkaConfiguration;
using MerchandiseService.Infrastructure.HostedServices.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MerchandiseService.Infrastructure.HostedServices
{
    public class IssueMerchEventHostedService : BackgroundService
    {
        private readonly KafkaOptions _kafkaOptions;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger _logger;

        public IssueMerchEventHostedService(
            IOptions<KafkaOptions> kafkaOptions,
            IServiceScopeFactory scopeFactory,
            ILogger<IssueMerchEventHostedService> logger)
        {
            _kafkaOptions = kafkaOptions == null ? throw new ArgumentNullException(nameof(kafkaOptions), "Cannot be null") : kafkaOptions.Value;
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory), "Cannot be null");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Cannot be null");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _kafkaOptions.GroupId,
                BootstrapServers = _kafkaOptions.BootstrapServers
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe(_kafkaOptions.Topics.IssueMerchTopic);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var stopwatch = new Stopwatch();

                    try
                    {
                        await Task.Yield();
                        stopwatch.Start();

                        var result = consumer.Consume(stoppingToken);
                        if (result is null)
                        {
                            return;
                        }

                        var message = JsonSerializer.Deserialize<IssueMerchNotificationEvent>(result.Message.Value);
                        if (message is null)
                        {
                            return;
                        }

                        var createMerchCommand = new CreateMerchCommand
                        {
                            MerchType = (MerchType)(int)message.Payload.MerchType,
                            Size = (Size)(int)message.Payload.ClothingSize,
                            EmployeeName = message.EmployeeName,
                            EmployeeEmail = message.EmployeeEmail,
                            ManagerName = message.ManagerName,
                            ManagerEmail = message.ManagerEmail
                        };

                        await mediator.Send(createMerchCommand, stoppingToken);
                    }
                    catch
                    {
                        stopwatch.Stop();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error when processing {_kafkaOptions.Topics.StockReplenishedTopic}");
            }
            finally
            {
                consumer.Commit();
                consumer.Close();
            }
        }
    }
}
