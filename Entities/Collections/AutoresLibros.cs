using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Interfaces;

namespace Entities.Collections
{
    //Estructura base para la tabla autores_has_libros en la base de datos
    [Table("AutoresLibros")]
    public class AutoresLibros : IAutoresLibros
    {
        [Column("AutoresId", Order = 1)]
        public int AutoresId { get; set; }

        [Column("LibrosIsbn", Order = 2)]
        public long LibrosIsbn { get; set; }

        [ForeignKey("LibrosIsbn")]
        public Libros Libro { get; set; }

        [ForeignKey("AutoresId")]
        public Autores Autor { get; set; }
    }
}
