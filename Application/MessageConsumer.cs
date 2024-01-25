using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Microservice_VolunTrack.Application;
using Microservice_VolunTrack.Infraestructure;
using Microservice_VolunTrack.Domain;

namespace Microservice_VolunTrack.Application
{
    public class MessageConsumer
    {
        private readonly MessageProcessor _messageProcessor;
        private readonly string _hostname = "localhost"; // Ajusta según tu configuración de RabbitMQ
        private readonly string _queueName = "informesQueue"; // Ajusta según tu configuración de RabbitMQ

        public MessageConsumer(MessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received message: {message}");

                    // Procesar el mensaje recibido
                    _messageProcessor.ProcessMessage(message);
                };

                channel.BasicConsume(queue: _queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Consuming messages. Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
