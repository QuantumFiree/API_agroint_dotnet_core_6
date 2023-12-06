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
    //[ApiController]
    [Route("api/[controller]")]
    public class ConsejoController : Controller
    {
        private readonly NpgsqlConnection _connection;

        public ConsejoController(NpgsqlConnection connection) {
            _connection = connection;
        }

        [HttpGet(Name = "GetConsejo")]
        public IEnumerable<Consejo> Get() {
            var consejos = _connection.Query<Consejo>("SELECT * FROM consejos");
            return consejos;
        }

        [HttpPost(Name = "PostConsejo")]
        public IActionResult Post([FromBody] Consejo consejo) {
            if (consejo == null) {
                return BadRequest("Los datos suministrados no coinciden con los datos esperados");
            }

            var query = "INSERT INTO consejos(Tipo, Titulo, Descripcion)" +
                        "VALUES(@Tipo, @Titulo, @Descripcion)";

            _connection.Execute(query, consejo);

            return Ok("Se ha agregado un consejo exitosamente");
        }

        [HttpPut("{id}", Name = "PutConsejo")]
        public IActionResult Put(int id, [FromBody] Consejo consejo) {
            if(consejo == null){
                return BadRequest("Se necesitan los datos necesaeios para actualizar el registro");
            }

            var findConsejo = _connection.QuerySingleOrDefault<Consejo>("SELECT * FROM consejos WHERE Id = @id", new {Id = id});

            if(findConsejo == null) {
                return BadRequest($"No se ha encontrado el registro con ID {id}");
            }

            var query = "UPDATE consejos SET Tipo = @Tipo, Titulo = @Titulo, Descripcion = @Descripcion " +
                        "WHERE Id = @Id";

            _connection.Execute(query, new {
                Id = id,
                consejo.Tipo,
                consejo.Titulo,
                consejo.Descripcion
            });

            return Ok($"El registro con ID {id} ha sido actualizado exitosamente");
        }

        [HttpDelete("{id}", Name = "DeleteConsejo")]
        public IActionResult Delete(int id) {
            var findConsejo = _connection.QuerySingleOrDefault<ConsejoController>("SELECT * FROM consejos WHERE id = @Id", new {Id = id});

            if(findConsejo == null) {
                return BadRequest("No se ha encontrado el registro requerido");
            }

            var query = "DELETE FROM consejos WHERE id = @Id";

            _connection.Execute(query, new {Id = id});

            return Ok($"El Consejo con ID {id} se ha eliminado exitosamente");
        }
    }
}