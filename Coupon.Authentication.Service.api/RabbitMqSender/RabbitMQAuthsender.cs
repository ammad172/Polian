using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Authentication.Service.api.RabbitMqSender
{
    public class RabbitMQAuthsender : IRabbitMQAuthsender
    {

        private readonly string _hostname;
        private readonly string _usermame;
        private readonly string _password;
        private IConnection _connection;

        public RabbitMQAuthsender()
        {
            _hostname = "localhost";
            _usermame = "guest";
            _password = "guest";
        }


        public void SendMessage(object message, string quename)
        {
            try
            {
                if (CheckConnectionStatus())
                {

                    using var _channel = _connection.CreateModel();
                    _channel.QueueDeclare(quename, false, false, false, null);
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    _channel.BasicPublish(exchange: "", routingKey: quename, null, body);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    Password = _password,
                    UserName = _usermame
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {

            }
        }

        private bool CheckConnectionStatus()
        {

            if (_connection == null)
            {
                CreateConnection();
                return true;
            }

            return true;
        }
    }
}
