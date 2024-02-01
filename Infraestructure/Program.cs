using Microservice_VolunTrack.Application;
using Microservice_VolunTrack.Domain;
using Microservice_VolunTrack.Infraestructure.RabbitMQ;
using Microservice_VolunTrack.Infraestructure;

namespace Microservice_VolunTrack.Infrastructure
{
    public class Program
    {
        static void Main(string[] args)
        {
            IDatabaseManager databaseManager = new DatabaseManager();
            IMessagePublisher messagePublisher = new MessagePublisher();
            IFormHandlerFactory formHandlerFactory = new FormHandlerFactory();

            // Crear la instancia del cliente API con la URL de la API Gateway
            var apiGatewayClient = new ApiGatewayClient("http://localhost:3001");

            // Pasar el cliente API al constructor de MessageProcessor
            MessageProcessor messageProcessor = new MessageProcessor(
                databaseManager, messagePublisher, formHandlerFactory, apiGatewayClient);

            // Inicializar y empezar a consumir mensajes con el MessageConsumer
            MessageConsumer messageConsumer = new MessageConsumer(messageProcessor);
            messageConsumer.StartConsuming();
        }
    }
}
