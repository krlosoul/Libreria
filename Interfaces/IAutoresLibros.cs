using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    /// <summary>
    /// Estructura básica para el objeto de relacion AutoresHasLibros
    /// </summary>
    public interface IAutoresLibros
    {
        int AutoresId { get; set; }
        long LibrosIsbn { get; set; }
    }
}
