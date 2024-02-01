using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice_VolunTrack.Infraestructure.RabbitMQ
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly string _hostname = "localhost"; // Ajusta según tu configuración de RabbitMQ
        private readonly string _exchangeName = "testTopic"; // Ajusta según tu configuración de RabbitMQ
        private readonly string _routingKey = "resultados"; // Ajusta según tu configuración de RabbitMQ

        public void PublishMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _exchangeName, type: "topic", durable: true);


                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: _exchangeName,
                                     routingKey: _routingKey,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"Mensaje publicado: {message}");
            }
        }
    }
}
