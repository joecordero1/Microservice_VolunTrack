using Microservice_VolunTrack.Domain;
using Microservice_VolunTrack.Domain.Models;
using Microservice_VolunTrack.Infraestructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Microservice_VolunTrack.Application
{
    public class MessageProcessor
    {
        private readonly IDatabaseManager _databaseManager;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IFormHandlerFactory _formHandlerFactory;
        private readonly ApiGatewayClient _apiGatewayClient;

        public MessageProcessor(
            IDatabaseManager databaseManager,
            IMessagePublisher messagePublisher,
            IFormHandlerFactory formHandlerFactory,
            ApiGatewayClient apiGatewayClient) // Cambio aquí, inyecta la instancia ya creada de ApiGatewayClient
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
            _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
            _formHandlerFactory = formHandlerFactory ?? throw new ArgumentNullException(nameof(formHandlerFactory));
            _apiGatewayClient = apiGatewayClient ?? throw new ArgumentNullException(nameof(apiGatewayClient));
        }

        public async Task ProcessMessage(string message)
        {
            try
            {
                // Dividir el mensaje en partes (tipo de formulario, id de usuario)
                var messageParts = message.Split(',');
                if (messageParts.Length != 2)
                {
                    throw new ArgumentException("Mensaje en formato incorrecto");
                }

                var formType = messageParts[0].Trim();
                var userId = int.Parse(messageParts[1].Trim());

                // Comprobar si el tipo de formulario es "PrimerFormulario" y el ID de usuario es 2
                if (formType == "PrimerFormulario")
                {
                    // Llamar a la API para obtener la lista de usuarios
                    var usuarios = await _apiGatewayClient.GetAsync<List<Usuario>>("listarUsuarios");

                    // Encontrar al usuario con el ID especificado
                    var usuario = usuarios.FirstOrDefault(u => u.IdUsuario == userId);
                    if (usuario != null)
                    {
                        // Aquí tienes el nombre del usuario y puedes hacer lo que necesites con él
                        var nombreUsuario = usuario.Nombre;
                        _messagePublisher.PublishMessage(nombreUsuario);
                        // Por ejemplo, almacenar el nombre en una variable
                        // Esta es una variable local; deberás decidir cómo quieres utilizarla en tu aplicación
                    }
                }
                else
                {
                    // Manejar otros tipos de formularios o IDs de usuario
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar el mensaje: {ex.Message}");
                // Manejo de errores adecuado
            }
        }
    }
}
