using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newapi6.models
{
    public class Cultivo
    {
        public int Id { get; set; }
        public int IdAgricultor { get; set; }
        public string Tipo { get; set; }
        public string Semilla { get; set; }
        public int Germinacion { get; set; }
        public int Crecimiento { get; set; }
        public int Reproduccion { get; set; }
        public int Fructificacion { get; set; }
        public int Cosecha { get; set; }

        public Cultivo()
        {
            Tipo = "";
            Semilla = "";
            Germinacion = 0;
            Crecimiento = 0;
            Reproduccion = 0;
            Fructificacion = 0;
            Cosecha = 0;
        }
    }
}