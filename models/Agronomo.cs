using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newapi6.models
{
    public class Agronomo
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int Score { get; set; }
        public string Descripcion { get; set; }

        public Agronomo()
        {
            // Inicializar propiedades aquí
            Nombres = "";
            Apellidos = "";
            Telefono = "";
            Correo = "";
            Score = 0;
            Descripcion = "";
        }
    }
}