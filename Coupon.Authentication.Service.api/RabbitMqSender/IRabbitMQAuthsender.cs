namespace Authentication.Service.api.RabbitMqSender
{
    public interface IRabbitMQAuthsender
    {
        public void SendMessage(object message, string quename);
    }
}
