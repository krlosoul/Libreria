using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.Collections;
using Spa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Spa.Controllers
{
    [Route("api/Editoriales")]
    [ApiController]
    public class EditorialesController : ControllerBase
    {
        private DataBase db { get; set; }

        public EditorialesController(DataBase db)
        {
            this.db = db;
        }

        [HttpGet]
        public GridModel Get([FromQuery] int skip, int take)
        {
            try
            {
                //Se consulta la información paginandola con los parametros envíados desde el front-end
                List<Editoriales> data = db.editoriales.OrderBy(x => x.Nombre).Skip(skip).Take(take).ToList();

                //Se consulta el total de la información en la base de datos para realizar el paginado
                int count = db.editoriales.Count();

                //Se retorna el modelo predeterminado para el reconocimiento en el front-end
                return new GridModel { Data = data, Count = count };
            }
            catch (Exception ex)
            {
                Exception ex1 = ex;

                //Se recorre la excepción para realizar la búsqueda de las excepciones internas y determinar el mensaje de salida
                while (ex1.InnerException != null)
                {
                    ex1 = ex1.InnerException;
                }

                throw new Exception(ex1.Message);
            }
        }

        [HttpGet("list")]
        public List<Editoriales> List()
        {
            //Se consulta la información para mostrar en el combobox
            return db.editoriales.OrderBy(x => x.Nombre).ToList();
        }

        [HttpPost]
        public async Task<Editoriales> Post([FromBody] Editoriales editorial)
        {
            try
            {
                //Se agrega la entidad a la colección de la base de datos
                db.editoriales.Add(editorial);

                //Se ejecuta el almacenamiento de los campos en la base de datos para su inserción
                await db.SaveChangesAsync();
                return editorial;
            }
            catch (Exception ex)
            {
                Exception ex1 = ex;

                //Se recorre la excepción para realizar la búsqueda de las excepciones internas y determinar el mensaje de salida

                while (ex1.InnerException != null)
                {
                    ex1 = ex1.InnerException;
                }

                throw new Exception(ex1.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<Editoriales> Put(int id, [FromBody] Editoriales editorial)
        {
            try
            {
                //Se realiza la búsqueda de la información a editar
                Editoriales patch = db.editoriales.Find(id);

                if (patch == default(Editoriales))
                {
                    //En caso de no encontrar el registro se genera una excepción para informar al usuario
                    throw new CustomException("El registro que intenta eliminar ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }

                //Se realiza la modificacíon de los datos
                patch.Nombre = editorial.Nombre;
                patch.DireccionCorrespondencia = editorial.DireccionCorrespondencia;
                patch.Telefono = editorial.Telefono;
                patch.Correo = editorial.Correo;
                patch.MaxLibros = editorial.MaxLibros;

                //Se ejecuta el almacenamiento de los campos en la base de datos para su actualización
                await db.SaveChangesAsync();
                return editorial;
            }
            catch (CustomException ex)
            {
                //En caso de encontrar una excepción de tipo "CustomException" se retorna la excepción tal cual para la interpretación del manejador
                throw ex;
            }
            catch (Exception ex)
            {
                Exception ex1 = ex;
                //Se recorre la excepción para realizar la búsqueda de las excepciones internas y determinar el mensaje de salida
                while (ex1.InnerException != null)
                {
                    ex1 = ex1.InnerException;
                }

                throw new Exception(ex1.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<Editoriales> Delete(int id)
        {
            try
            {
                //Se realiza la búsqueda de la información a eliminar
                Editoriales patch = await db.editoriales.Include(x => x.Libros).Where(x => x.Id == id).FirstOrDefaultAsync();

                if (patch == default(Editoriales))
                {
                    //En caso de no encontrar el registro se genera una excepción para informar al usuario
                    throw new CustomException("El registro que intenta eliminar ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }

                //Se valida que no tenga relaciones
                if (patch.Libros.Count > 0)
                {
                    StringBuilder libros = new StringBuilder();

                    //Se mapean los libros relacionados
                    foreach (Libros libro in patch.Libros)
                    {
                        libros.AppendLine("<li>" + libro.Titulo + "</li>");
                    }

                    //Se genera excepción de tipo "Custom Exception" para su debida interpretación por el manejador
                    throw new CustomException("<p>No es posible eliminar la editorial ya que tiene libros relacionados, por favor elimine la relación entre los libros y la editorial seleccionada para continuar. Los libros relacionados son los siguientes:</p><ul>" + libros.ToString() + "</ul>");
                }
                //En caso de pasar la validación se elimina la entidad de la colección
                db.editoriales.Remove(patch);

                //Se ejecuta el almacenamiento de los campos en la base de datos para su eliminación
                await db.SaveChangesAsync();
                return patch;
            }
            catch (CustomException ex)
            {
                //En caso de encontrar una excepción de tipo "CustomException" se retorna la excepción tal cual para la interpretación del manejador
                throw ex;
            }
            catch (Exception ex)
            {
                Exception ex1 = ex;
                //Se recorre la excepción para realizar la búsqueda de las excepciones internas y determinar el mensaje de salida
                while (ex1.InnerException != null)
                {
                    ex1 = ex1.InnerException;
                }

                throw new Exception(ex1.Message);
            }
        }


    }
}
