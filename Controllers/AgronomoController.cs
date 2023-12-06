using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using newapi6.models;
using Npgsql;
using Dapper;

namespace newapi6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgronomoController : Controller
    {
        private readonly NpgsqlConnection _connection;

        public AgronomoController(NpgsqlConnection connection) {
            _connection = connection;
        }

        [HttpGet(Name = "GetAgronomos")]
        public IEnumerable<Agronomo> Get() {
            var agronomos = _connection.Query<Agronomo>("SELECT * FROM agronomo");
            return agronomos;
        }

        [HttpPost(Name = "PostAgronomo")]
        public IActionResult Post([FromBody] Agronomo agronomo) {
            if(agronomo == null) {
                return BadRequest("Los tipos de datos no coinciden");
            }

            var query = "INSERT INTO agronomo (Nombres, Apellidos, Telefono, Correo, Score, Descripcion)" + 
                        "VALUES (@Nombres, @Apellidos, @Telefono, @Correo, @Score, @Descripcion)";

            _connection.Execute(query, agronomo);

            return Ok("Agronomo insertado correctamente!");
        }

        [HttpPut("{id}", Name = "UpdateAgronomo")]
        public IActionResult Put(int id, [FromBody] Agronomo agronomo) {
            if(agronomo == null) {
                return BadRequest("Los tipos de datos no coinciden");
            }

            var findAgronomo = _connection.QuerySingleOrDefault<Agronomo>("SELECT * FROM agronomo WHERE id = @Id", new {Id = id});

            if(findAgronomo == null) {
                return BadRequest("No se ha encontrado a el agronomo");
            }

            var query = "UPDATE agronomo SET nombres = @Nombres, apellidos = @Apellidos, telefono = @Telefono, correo = @Correo, score = @Score, descripcion = @Descripcion" +
                        " WHERE id = @Id";

            _connection.Execute(query, new {
                id, 
                agronomo.Nombres,
                agronomo.Apellidos,
                agronomo.Telefono,
                agronomo.Correo,
                agronomo.Score,
                agronomo.Descripcion
            });

            return Ok($"Agronomo con el ID {id} actualizado exitosamente!");
        }

        [HttpDelete("{id}", Name = "DeleteAgronomo")]
        public IActionResult Delete(int id) {
            var findAgronomo = _connection.QuerySingleOrDefault<Agronomo>("SELECT * FROM agronomo WHERE id = @Id", new {Id = id});

            if(findAgronomo == null) {
                return BadRequest("No se ha encontrado al agronomo");
            }

            var query = "DELETE FROM agronomo WHERE Id = @Id";
            _connection.Execute(query, new {Id = id});

            return Ok($"Agronomo con el ID {id} eliminado exitosamente");
        }
    }
}