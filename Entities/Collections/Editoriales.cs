using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Interfaces;

namespace Entities.Collections
{
    //Estructura base para la tabla editoriales en la base de datos
    [Table("Editoriales")]
    public class Editoriales : IEditoriales
    {
        /// <summary>
        /// Connstructor principal de la clase
        /// </summary>
        public Editoriales()
        {
            this.Libros = new HashSet<Libros>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [StringLength(45)]
        [Required]
        public string Nombre { get; set; }

        [StringLength(45)]
        [Required]
        public string DireccionCorrespondencia { get; set; }

        [StringLength(45)]
        [Required]
        public string Telefono { get; set; }

        [StringLength(45)]
        [Required]
        public string Correo { get; set; }

        [Required]
        public int MaxLibros { get; set; }

        public ICollection<Libros> Libros { get; set; }
    }
}
