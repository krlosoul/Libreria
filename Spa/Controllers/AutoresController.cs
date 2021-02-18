using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.Collections;
using Spa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spa.Controllers
{
    [Route("api/Autores")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private DataBase db {get; set; }

        public AutoresController(DataBase db)
        {
            this.db = db;
        }

        [HttpGet]
        public GridModel Get([FromQuery] int skip, int take)
        {
            try
            {
                //Se consulta la información paginandola con los parametros envíados desde el front-end
                List<Autores> data = db.autores.OrderBy(x => x.NombreCompleto).Skip(skip).Take(take).ToList();

                //Se consulta el total de la información en la base de datos para realizar el paginado
                int count = db.autores.Count();

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
        public List<Autores> List()
        {
            //Se consulta la información para mostrar en el combobox
            return db.autores.OrderBy(x => x.NombreCompleto).ToList();
        }


        [HttpPost]
        public async Task<Autores> Post([FromBody] Autores autor)
        {
            try
            {
                //Se agrega la entidad a la colección de la base de datos
                db.autores.Add(autor);
                //Se ejecuta el almacenamiento de los campos en la base de datos para su inserción
                await db.SaveChangesAsync();
                return autor;
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
        public async Task<Autores> Put(int id, [FromBody] Autores autor)
        {
            try
            {
                //Se realiza la búsqueda de la información a editar
                Autores patch = db.autores.Find(id);

                if (patch == default(Autores))
                {
                    //En caso de no encontrar el registro se genera una excepción para informar al usuario
                    throw new CustomException("El registro que intenta eliminar ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }
                //Se realiza la modificacíon de los datos
                patch.NombreCompleto = autor.NombreCompleto;
                patch.FechaNacimiento = autor.FechaNacimiento;
                patch.Ciudad= autor.Ciudad;
                patch.Correo = autor.Correo;

                //Se ejecuta el almacenamiento de los campos en la base de datos para su actualización
                await db.SaveChangesAsync();
                return autor;
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
        public async Task<Autores> Delete(int id)
        {
            try
            {
                //Se realiza la búsqueda de la información a eliminar
                Autores patch = db.autores.Find(id);

                if (patch == default(Autores))
                {
                    //En caso de no encontrar el registro se genera una excepción para informar al usuario
                    throw new CustomException("El registro que intenta eliminar ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }
                //En caso de pasar la validación se elimina la entidad de la colección
                db.autores.Remove(patch);
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
