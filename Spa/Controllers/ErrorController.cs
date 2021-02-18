using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spa.Models;

namespace Spa.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Manejador de errores por defecto de la aplicación
        /// </summary>
        /// <returns>Objeto de tipo ErrorModel con la información del error generado en la aplicación</returns>
        [Route("Error")]
        public ErrorModel Error()
        {
            //Se obtiene el contexto de la excepción generada
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            //Se mapea el mensaje del error generado
            string message = context.Error.Message;

            //Se inicializa el tipo como un error
            ErrorModel.ErrorType type = ErrorModel.ErrorType.Error;

            if (context.Error is CustomException)
                //Si el tipo del error es CustomException se le da manejo a la excepción como una advertencia
                type = ErrorModel.ErrorType.Warning;

            //Se indica el tipo de error al cuerpo de respuesta de la aplicación
            Response.StatusCode = ((int)type);

            //Se retorna el modelo de error por defecto para su procesamiento en el front-end
            return new ErrorModel(message, type);
        }
    }
}
