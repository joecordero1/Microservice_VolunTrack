using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microservice_VolunTrack.Application;
using Microservice_VolunTrack.Domain;
using Microservice_VolunTrack.Infraestructure.RabbitMQ;

namespace Microservice_VolunTrack.Infraestructure
{
    public class Program
    {
        static void Main(string[] args)
        {
            IDatabaseManager databaseManager = new DatabaseManager();
            IMessagePublisher messagePublisher = new MessagePublisher();
            IFormHandlerFactory formHandlerFactory = new FormHandlerFactory();
            MessageProcessor messageProcessor = new MessageProcessor(databaseManager, messagePublisher, formHandlerFactory);
            MessageConsumer messageConsumer = new MessageConsumer(messageProcessor);

            messageConsumer.StartConsuming();
        }
    }
}