using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    /// <summary>
    /// Estructura básica para el objeto Editoriales
    /// </summary>
    public interface IEditoriales
    {
        int Id { get; set; }
        string Nombre { get; set; }
        string DireccionCorrespondencia { get; set; }
        string Telefono { get; set; }
        string Correo { get; set; }
        int MaxLibros { get; set; }
    }
}
