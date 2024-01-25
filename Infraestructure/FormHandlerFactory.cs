using Microservice_VolunTrack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice_VolunTrack.Infraestructure
{
    public class FormHandlerFactory : IFormHandlerFactory
    {
        public IFormHandler GetHandler(string formType)
        {
            // Lógica para retornar el manejador de formulario adecuado
            return new Formulario1Handler(); // Ejemplo, ajustar según la lógica
        }
    }
}
