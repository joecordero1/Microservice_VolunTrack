using Microservice_VolunTrack.Domain;
using Microservice_VolunTrack.Infraestructure;
using Microservice_VolunTrack.Infraestructure.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice_VolunTrack.Application
{
    public class MessageProcessor
    {
        private readonly IDatabaseManager _databaseManager;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IFormHandlerFactory _formHandlerFactory;

        public MessageProcessor(IDatabaseManager databaseManager, IMessagePublisher messagePublisher, IFormHandlerFactory formHandlerFactory)
        {
            _databaseManager = databaseManager;
            _messagePublisher = messagePublisher;
            _formHandlerFactory = formHandlerFactory;
        }

        public void ProcessMessage(string message)
        {
            try
            {
                // Dividir el mensaje en partes (formulario, idUsuario)
                var messageParts = message.Split(',');
                if (messageParts.Length != 2)
                {
                    throw new ArgumentException("Mensaje en formato incorrecto");
                }

                var formType = messageParts[0].Trim();
                var userId = int.Parse(messageParts[1].Trim());

                // Obtener el manejador adecuado para el tipo de formulario
                var formHandler = _formHandlerFactory.GetHandler(formType);
                if (formHandler == null)
                {
                    throw new InvalidOperationException($"No se encontró un manejador para el tipo de formulario: {formType}");
                }

                // Procesar el formulario
                var result = formHandler.Handle(userId);

                // Publicar el resultado
                _messagePublisher.PublishMessage($"Resultado procesado para el usuario {userId}: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar el mensaje: {ex.Message}");
                // Aquí podrías manejar errores, como reintentar el procesamiento, registrar errores, etc.
            }
        }
    }
}
