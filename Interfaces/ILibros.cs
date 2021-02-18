using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    /// <summary>
    /// Estructura básica para el objeto Libros
    /// </summary>
    public interface ILibros
    {
        long Isbn { get; set; }
        string Titulo { get; set; }
        string Genero { get; set; }
        string NumeroPaginas { get; set; }
        int EditorialesId { get; set; }

    }
}