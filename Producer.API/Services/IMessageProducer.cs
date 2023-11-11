namespace Producer.API.Services
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
