using Confluent.Kafka;
using KafkaServices.Consumer;

namespace ConsumerConsoleApp
{
    internal class Program
    {
        protected Program()
        {

        }

        static void Main(string[] args)
        {
            string bootstrapServers = "localhost:29092";
            string topic = "topic";
            string groupId = "lamax";

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            IMessageConsumerService consumerService = new KafkaConsumerService(config, topic);
            Console.WriteLine("Consumer started. Press Ctrl+C to stop.");
            consumerService.Listen();
        }
    }
}
