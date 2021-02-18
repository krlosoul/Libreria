using Microsoft.EntityFrameworkCore;
using Entities.Collections;

namespace Entities
{
    public class DataBase : DbContext
    {
        /// <summary>
        /// Constructor principal de la clase Entities
        /// </summary>
        public DataBase(DbContextOptions options) : base(options)
        {
        }
        
        /// <summary>
        /// Acceso a la información de la tabla autores
        /// </summary>
        public DbSet<Autores> autores { get; set; }
        
        /// <summary>
        /// Acceso a la información dela tabla autores_has_libros
        /// </summary>
        public DbSet<AutoresLibros> autoresHasLibros { get; set; }
       
        /// <summary>
        /// Acceso a la información de la tabla editoriales
        /// </summary>
        public DbSet<Editoriales> editoriales { get; set; }

        /// <summary>
        /// Acceso a la información de la tabla libros
        /// </summary>
        public DbSet<Libros> libros { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Se modifican las convenciones para evitar la eliminación en cascada.
            builder.Entity<Autores>().HasMany(c => c.AutoresLibros).WithOne(e => e.Autor).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Libros>().HasMany(c => c.AutoresLibros).WithOne(e => e.Libro).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Editoriales>().HasMany(c => c.Libros).WithOne(e => e.Editorial).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AutoresLibros>().HasKey(p => new { p.AutoresId, p.LibrosIsbn });
        }
    }
}
