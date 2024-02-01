using System;
using System.Collections.Generic;

namespace Microservice_VolunTrack.Domain.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; } // Ten cuidado con manejar contraseñas en tu microservicio.
        public DateTime FechaNacimiento { get; set; } // Asegúrate de que el tipo coincide con el formato de fecha de tu API.
        public string Correo { get; set; }
        public string Numero { get; set; }
        public string Direccion { get; set; }
        /*
        // Estas propiedades representan relaciones y pueden ser necesarias si interactúas con ellas.
        // Si no necesitas esta información en tu microservicio, puedes omitirlas.
        public List<EstanteRecompensa> EstanteRecompensas { get; set; }
        public List<OportunidadesVoluntariado> OportunidadesVoluntariados { get; set; }
        public List<RegistroEvento> RegistroEventos { get; set; }
        */
    }
    /*
    // Debes definir estas clases si vas a utilizarlas.
    // Si no, puedes omitirlas o dejar las propiedades como listas vacías.
    public class EstanteRecompensa
    {
        // Define las propiedades aquí según la estructura de tu API.
    }

    public class OportunidadesVoluntariado
    {
        // Define las propiedades aquí según la estructura de tu API.
    }

    public class RegistroEvento
    {
        // Define las propiedades aquí según la estructura de tu API.
    }
    */
}
