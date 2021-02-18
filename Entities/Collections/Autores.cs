using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Interfaces;

namespace Entities.Collections
{
    //Estructura base para la tabla autores en la base de datos
    [Table("Autores")]
    public class Autores : IAutores
    {

        /// <summary>
        /// Connstructor principal de la clase
        /// </summary>
        public Autores()
        {
            //Inicialización de la relación con autores_has_libros
            this.AutoresLibros = new HashSet<AutoresLibros>();
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(45)]
        [Required]
        public string NombreCompleto { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }
        
        [StringLength(45)]
        [Required]
        public string Ciudad { get; set; }
        
        [StringLength(45)]
        [Required]
        public string Correo { get; set; }

        public ICollection<AutoresLibros> AutoresLibros { get; set; }

    }
}
