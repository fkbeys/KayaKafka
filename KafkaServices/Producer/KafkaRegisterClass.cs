using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaServices.Producer
{
    public static class KafkaRegisterClass
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton(new ProducerConfig { BootstrapServers = "localhost:29092" });
            services.AddSingleton<IMessageProducer, KafkaMessageProducer>();
        }
    }
}
