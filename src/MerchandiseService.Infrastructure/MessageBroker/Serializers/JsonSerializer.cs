using System;
using System.Text.Json;
using Confluent.Kafka;

namespace MerchandiseService.Infrastructure.MessageBroker.Serializers
{
    internal class JsonSerializer<TMessage> : ISerializer<TMessage>, IDeserializer<TMessage>
    {
        public byte[] Serialize(TMessage data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data);
        }

        public TMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
            {
                return default;
            }

            return JsonSerializer.Deserialize<TMessage>(data);
        }
    }
}
