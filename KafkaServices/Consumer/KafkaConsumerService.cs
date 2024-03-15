using Confluent.Kafka;

namespace KafkaServices.Consumer
{
    public class KafkaConsumerService : IMessageConsumerService
    {
        private readonly ConsumerConfig _config;
        private readonly string _topic;

        public KafkaConsumerService(ConsumerConfig config, string topic)
        {
            _config = config;
            _topic = topic;
        }

        public void Listen()
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(_config).Build())
            {
                consumer.Subscribe(_topic);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cts.Token);
                            Console.WriteLine($"Message: {consumeResult.Message.Value} received from {consumeResult.TopicPartitionOffset}");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occurred: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
                finally
                {
                    cts.Dispose();
                }
            }
        }
    }
}
