using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newapi6.models
{
    public class Agricultor
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Organizacion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Sector { get; set; }

        public Agricultor()
        {
            Nombres = "";
            Apellidos = "";
            Telefono = "";
            Correo = "";
            Organizacion = "";
            Sector = "";
        }
    }
}