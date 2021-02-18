using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    /// <summary>
    /// Estructura básica para el objeto Autores
    /// </summary>
    public interface IAutores
    {
        int Id { get; set; }
        string NombreCompleto { get; set; }
        DateTime FechaNacimiento { get; set; }
        string Ciudad { get; set; }
        string Correo { get; set; }
    }
}