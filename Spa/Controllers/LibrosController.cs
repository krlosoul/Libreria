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
    [Route("api/Libros")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private DataBase db { get; set; }

        public LibrosController(DataBase db)
        {
            this.db = db;
        }

        [HttpGet]
        public GridModel Get([FromQuery] int skip, int take)
        {
            try
            {
                //Se consulta la información paginandola con los parametros envíados desde el front-end
                List<Libros> data = db.libros.Include(x => x.Editorial).OrderBy(x => x.Titulo).Skip(skip).Take(take).Select(x => new Libros
                {
                    //Se fuerza el mapeo de la relación editorial para evitar la sobre carga del json en la lectura.
                    Editorial = new Editoriales
                    {
                        Id = x.Editorial.Id,
                        Nombre = x.Editorial.Nombre,
                        DireccionCorrespondencia = x.Editorial.DireccionCorrespondencia,
                        Telefono = x.Editorial.Telefono,
                        Correo = x.Editorial.Correo,
                        MaxLibros = x.Editorial.MaxLibros,
                        Libros = null,
                    },
                    Isbn = x.Isbn,
                    Titulo = x.Titulo,
                    Genero = x.Genero,
                    NumeroPaginas = x.NumeroPaginas,
                    EditorialesId = x.EditorialesId,
                    AutoresLibros = x.AutoresLibros.Select(y => new AutoresLibros { 
                        AutoresId = y.AutoresId,
                        LibrosIsbn = y.LibrosIsbn,
                        Libro = null,
                        Autor = new Autores
                        {
                            AutoresLibros = null,
                            Ciudad = y.Autor.Ciudad,
                            Correo = y.Autor.Correo,
                            FechaNacimiento = y.Autor.FechaNacimiento,
                            Id = y.Autor.Id,
                            NombreCompleto = y.Autor.NombreCompleto
                        }
                    }).ToList()
            }).ToList();

                //Se consulta el total de la información en la base de datos para realizar el paginado
                int count = db.libros.Count();

                //Se retorna el modelo predeterminado para el reconocimiento en el front-end
                return new GridModel { Data = data, Count = count };
            }
            catch (Exception ex)
            {
                Exception ex1 = ex;
                while (ex1.InnerException != null)
                {
                    ex1 = ex1.InnerException;
                }

                throw new Exception(ex1.Message);
            }
        }

        [HttpPost("{idAutor}")]
        public async Task<Libros> Post(int idAutor, [FromBody] Libros libro)
        {
            try
            {
                //Se realiza validación del registro a almacenar en la base de datos para evitar error que rompa la integridad de la llave primaria
                Libros tmpLibro = db.libros.Find(libro.Isbn);

                //Se evalua el resultado haciendo uso del valor por defecto de la clase
                if (tmpLibro != default(Libros))
                {
                    //Se genera excepción de tipo "Custom Exception" para su debida interpretación por el manejador
                    throw new CustomException("Actualmente existe otro libro con el ISBN ingresado, el titulo de este libro es " + tmpLibro.Titulo);
                }

                int countLibros = db.libros.Where(x => x.EditorialesId == libro.EditorialesId).Count();

                var itemEditorial = db.editoriales.FirstOrDefault(x => x.Id == libro.EditorialesId);
                int countEditorial = itemEditorial.MaxLibros;

                if (countLibros == countEditorial)
                {
                    //Se genera excepción de tipo "Custom Exception" para su debida interpretación por el manejador
                    throw new CustomException("La libreria " + itemEditorial.Nombre +" Alcanzo el maximo de libros registrados.");
                }

                //Se agrega la relacion
                AutoresLibros al = new AutoresLibros { AutoresId = idAutor, LibrosIsbn = libro.Isbn, Libro = null, Autor = null };
                libro.AutoresLibros = new List<AutoresLibros>
                {
                    al
                };

                //Se agrega la entidad a la colección de la base de datos
                db.libros.Add(libro);

                //Se ejecuta el almacenamiento de los campos en la base de datos para su inserción
                await db.SaveChangesAsync();
                return libro;
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

        [HttpPut("{id}/{idAutor}")]
        public async Task<Libros> Put(long id, int idAutor, [FromBody] Libros libro)
        {
            try
            {
                //Se realiza la búsqueda de la información a editar
                Libros patch = db.libros.Find(id);

                if (patch == default(Libros))
                {
                    //En caso de no encontrar el registro se genera una excepción para informar al usuario
                    throw new CustomException("El registro que intenta eliminar ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }

                //Se realiza la modificacíon de los datos
                patch.Titulo = libro.Titulo;
                patch.Genero = libro.Genero;
                patch.NumeroPaginas = libro.NumeroPaginas;
                patch.EditorialesId = libro.EditorialesId;

                //Se realiza la búsqueda de la información a eliminar
                db.autoresHasLibros.RemoveRange(db.autoresHasLibros.Where(x => x.LibrosIsbn == id && x.AutoresId == idAutor));

                //Se agrega la relacion
                AutoresLibros al = new AutoresLibros { AutoresId = idAutor, LibrosIsbn = libro.Isbn, Libro = null, Autor = null };
                patch.AutoresLibros = new List<AutoresLibros>
                {
                    al
                };

                //Se ejecuta el almacenamiento de los campos en la base de datos para su actualización
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

        [HttpDelete("{id}")]
        public async Task<Libros> Delete(long id)
        {
            try
            {
                //Se realiza la búsqueda de la información a eliminar
                Libros patch = db.libros.Find(id);

                if (patch == default(Libros))
                {
                    //En caso de no encontrar el registro se genera una excepción para informar al usuario
                    throw new CustomException("El registro que intenta eliminar ya no se encuentra en la base de datos, por favor refresque la ventana e intentelo de nuevo");
                }

                //En caso de pasar la validación se elimina la entidad de la colección
                db.libros.Remove(patch);

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
