namespace KafkaServices.Producer
{
    public interface IMessageProducer
    {
        Task SendMessageAsync<T>(string topic, T message);
    }
}
