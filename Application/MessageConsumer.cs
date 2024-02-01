using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Microservice_VolunTrack.Application;

namespace Microservice_VolunTrack.Application
{
    public class MessageConsumer
    {
        private readonly MessageProcessor _messageProcessor;
        private readonly string _hostname = "localhost"; // Ajusta según tu configuración de RabbitMQ
        private readonly string _queueName = "QueueFormulario"; // Ajusta según la cola que desees consumir
        private readonly string _exchangeName = "testTopic"; // Ajusta según tu intercambio
        private readonly string _routingKey = "notificaciones"; // Ajusta según el routing key que desees consumir
        private IConnection _connection;
        private IModel _channel;

        public MessageConsumer(MessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = "guest",
                Password = "guest",
                Port = 5672
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Topic, durable: true);
            _channel.QueueDeclare(_queueName, true, false, false, null);
            _channel.QueueBind(_queueName, _exchangeName, _routingKey);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Mensaje recibido: {0}", message);

                // Procesar el mensaje recibido
                _messageProcessor.ProcessMessage(message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            Console.WriteLine("Consumiendo mensajes. Presiona Enter para salir.");
            Console.ReadLine();

            // Clean up
            _channel.Close();
            _connection.Close();
        }
    }
}
