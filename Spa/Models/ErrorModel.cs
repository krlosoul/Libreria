using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spa.Models
{
    public class ErrorModel
    {
        /// <summary>
        /// Opciones establecidas para los tipos de error a devolver
        /// </summary>
        public enum ErrorType
        {
            /// <summary>
            /// Mensaje de alerta tipo error
            /// </summary>
            Error = 500,
            /// <summary>
            /// Mensaje de alerta tipo advertencia
            /// </summary>
            Warning = 501
        }

        /// <summary>
        /// Tipo de error a generar al cliente
        /// </summary>
        public ErrorType Type { get; set; }

        /// <summary>
        /// Mensaje que se retornará al cliente
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Construye un objeto de tipo ErrorModel con el mensaje indicado y tipo error(500)
        /// </summary>
        /// <param name="message">Mensaje que se retornará al cliente</param>
        public ErrorModel(string message)
        {
            this.Message = message;
            this.Type = ErrorType.Error;
        }

        /// <summary>
        /// Construye un objeto de tipo ErrorModel con el mensaje y tipo indicados
        /// </summary>
        /// <param name="message">Mensaje que se retornará al cliente</param>
        /// <param name="type">Tipo de error que se retornará al cliente</param>
        public ErrorModel(string message, ErrorType type)
        {
            this.Message = message;
            this.Type = type;
        }
    }
}
