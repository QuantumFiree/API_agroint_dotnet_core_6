using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newapi6.models
{
    public class Consejo
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public Consejo()
    {
        // Inicializar propiedades aquí
        Tipo = "";
        Titulo = "";
        Descripcion = "";
    }
    }
}