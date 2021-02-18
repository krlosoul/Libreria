using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Interfaces;

namespace Entities.Collections
{
    //Estructura base para la tabla libros en la base de datos
    [Table("Libros")]
    public class Libros : ILibros
    {
        /// <summary>
        /// Connstructor principal de la clase
        /// </summary>
        public Libros()
        {
            //Inicialización de la relación con autores_has_libros
            this.AutoresLibros = new HashSet<AutoresLibros>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Isbn")]
        public long Isbn { get; set; }

        [StringLength(45)]
        [Required]
        public string Titulo { get; set; }

        [StringLength(45)]
        [Required]
        public string Genero { get; set; }

        [StringLength(45)]
        [Required]
        public string NumeroPaginas { get; set; }

        [Column("EditorialesId")]
        [Required]
        public int EditorialesId { get; set; }

        [ForeignKey("EditorialesId")]
        public Editoriales Editorial { get; set; }
        public ICollection<AutoresLibros> AutoresLibros { get; set; }
    }
}
