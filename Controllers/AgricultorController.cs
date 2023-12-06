using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using newapi6.models;
using Npgsql;
using Dapper;

namespace newapi6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgricultorController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public AgricultorController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetAgricultores")]
        public IEnumerable<Agricultor> Get()
        {
            var agricultores = _connection.Query<Agricultor>("SELECT * FROM agricultor");
            return agricultores;
        }

        [HttpPost(Name = "PostAgricultor")]
        public IActionResult Post([FromBody] Agricultor agricultor)
        {
            if (agricultor == null)
            {
                return BadRequest("El objeto Agricultor no puede ser nulo");
            }

            // Insertar el agricultor en la base de datos
            var query = "INSERT INTO agricultor (Nombres, Apellidos, Organizacion, Telefono, Correo, Sector) " +
                        "VALUES (@Nombres, @Apellidos, @Organizacion, @Telefono, @Correo, @Sector)";

            _connection.Execute(query, agricultor);

            // Devolver una respuesta exitosa
            return Ok("Agricultor creado exitosamente");
        }

        [HttpPut("{id}", Name = "UpdateAgricultor")]
        public IActionResult Put(int id, [FromBody] Agricultor agricultor)
        {
            if (agricultor == null)
            {
                return BadRequest("El objeto Agricultor no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingAgricultor = _connection.QuerySingleOrDefault<Agricultor>("SELECT * FROM agricultor WHERE Id = @Id", new { Id = id });

            if (existingAgricultor == null)
            {
                return NotFound($"Agricultor con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE agricultor SET Nombres = @Nombres, Apellidos = @Apellidos, Organizacion = @Organizacion, " +
                        "Telefono = @Telefono, Correo = @Correo, Sector = @Sector WHERE Id = @Id";

            _connection.Execute(query, new
            {
                Id = id,
                agricultor.Nombres,
                agricultor.Apellidos,
                agricultor.Organizacion,
                agricultor.Telefono,
                agricultor.Correo,
                agricultor.Sector
            });

            // Devolver una respuesta exitosa
            return Ok($"Agricultor con ID {id} actualizado exitosamente");
        }

        [HttpDelete("{id}", Name = "DeleteAgricultor")]
        public IActionResult Delete(int id)
        {
            // Verificar si el agricultor con el ID proporcionado existe
            var existingAgricultor = _connection.QuerySingleOrDefault<Agricultor>("SELECT * FROM agricultor WHERE Id = @Id", new { Id = id });

            if (existingAgricultor == null)
            {
                return NotFound($"Agricultor con ID {id} no encontrado");
            }

            // Eliminar el agricultor de la base de datos
            var query = "DELETE FROM agricultor WHERE Id = @Id";
            _connection.Execute(query, new { Id = id });

            // Devolver una respuesta exitosa
            return Ok($"Agricultor con ID {id} eliminado exitosamente");
        }


    }
}