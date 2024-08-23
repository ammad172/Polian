using Email.Services.Api.Model;
using Email.Services.Api.Services.IService;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Email.Services.Api.Messaging
{
    public class RabbitMQConsmerService : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        public RabbitMQConsmerService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare("PolainQue", false, false, false, null);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                stoppingToken.ThrowIfCancellationRequested();
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (ch, ea) =>
                {
                    var cont = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var emil = JsonConvert.DeserializeObject<string>(cont);
                    HandleMessage(emil).GetAwaiter().GetResult();
                    _channel.BasicAck(ea.DeliveryTag, false);
                };
                _channel.BasicConsume("PolainQue", false, consumer);

            }
            catch (Exception ex)
            {
            }

            return Task.CompletedTask;
        }

        private async Task HandleMessage(string email)
        {
            var message = new Message(new string[] { email },
                            "Test email async",
                             "This is the content from our async email.",
                                    null);


        }
    }
}
