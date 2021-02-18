using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spa.Models
{
    /// <summary>
    /// Excepción utilizada para la generación de mensajes de advertencia desde el servidor
    /// </summary>
    public class CustomException : Exception
    {
        /// <summary>
        /// Constructor del tipo CustomException
        /// </summary>
        /// <param name="message">Mensaje que se desea retornar al cliente</param>
        public CustomException(string message) : base(message)
        {

        }
    }
}
