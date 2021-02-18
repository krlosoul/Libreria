using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spa.Models
{
    /// <summary>
    /// Modelo por defecto para el manejo de datos dentro de las grid
    /// </summary>
    public class GridModel
    {
        public int Count { get; set; }
        public object Data{ get; set; }
    }
}
