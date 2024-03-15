using Confluent.Kafka;

namespace KafkaServices.Producer
{
    public class KafkaMessageProducer : IMessageProducer
    {
        private readonly ProducerConfig _producerConfig;

        public KafkaMessageProducer(ProducerConfig producerConfig)
        {
            _producerConfig = producerConfig;
        }

        public async Task SendMessageAsync<T>(string topic, T message)
        {
            using var producer = new ProducerBuilder<Null, T>(_producerConfig).Build();
            try
            {
                var kafkaMessage = new Message<Null, T> { Value = message };
                var deliveryResult = await producer.ProduceAsync(topic, kafkaMessage);
                Console.WriteLine($"Message delivered: {deliveryResult.TopicPartitionOffset}");
            }
            catch (ProduceException<Null, T> e)
            {
                Console.WriteLine($"Failed to deliver message: {e.Error.Reason}");
                throw;
            }
        }
    }

}
