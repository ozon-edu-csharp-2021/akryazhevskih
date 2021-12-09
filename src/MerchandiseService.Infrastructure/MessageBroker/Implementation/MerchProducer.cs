using System;
using System.Text.Json;
using System.Threading;
using Confluent.Kafka;
using CSharpCourse.Core.Lib.Events;
using MerchandiseService.Infrastructure.Configuration.KafkaConfiguration;
using Microsoft.Extensions.Options;

namespace MerchandiseService.Infrastructure.MessageBroker.Implementation
{
    internal class MerchProducer : IMerchProducer
    {
        private readonly KafkaOptions _kafkaOptions;
        private readonly IProducer<string, string> _producer;

        public MerchProducer(IOptions<KafkaOptions> kafkaOptions)
        {
            _kafkaOptions = kafkaOptions == null ? throw new ArgumentNullException(nameof(kafkaOptions), "Cannot be null") : kafkaOptions.Value;

            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaOptions.BootstrapServers
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public void Publish(long merchId, NotificationEvent notificationEvent, CancellationToken cancellationToken = default)
        {
            var message = new Message<string, string>()
            {
                Key = merchId.ToString(),
                Value = JsonSerializer.Serialize(notificationEvent)
            };

            _producer.ProduceAsync(_kafkaOptions.Topics.EmailNotificationTopic, message, cancellationToken);
        }
    }
}
