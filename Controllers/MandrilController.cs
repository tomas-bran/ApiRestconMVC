using MandrilAPI.Helpers;
using MandrilAPI.Models;
using MandrilAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MandrilAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MandrilController : ControllerBase
    {
        [HttpGet] 
        public ActionResult<IEnumerable<Mandril>> GetMandriles()
        {
            return Ok(MandrilDataStore.Current.Mandriles);
        }

        [HttpGet("{mandrilId}")]
        //Si no hubiese declarado la etiqueta de [ApiController], en los parametros del action habria que poner 
        //[FromRoute], dejando clara que el mandrilId viene de la ruta  (una ventaja de poner [ApiController])
        public ActionResult<Mandril> GetMandril(int mandrilId)
        {
            var mandrilResponse = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);

            if(mandrilResponse == null)
            {
                return NotFound(Mensajes.Mandril.NotFound);
            }

            return Ok(mandrilResponse);
        }

        [HttpPost]
        public ActionResult<Mandril> PostMandril (MandrilInsert mandrilInsert)
        {
            var maxMandrilId = MandrilDataStore.Current.Mandriles.Max(x => x.Id);

            var mandrilNuevo = new Mandril()
            {
                Id = maxMandrilId + 1,
                Nombre = mandrilInsert.Nombre,
                Apellido = mandrilInsert.Apellido,
            };

            MandrilDataStore.Current.Mandriles.Add(mandrilNuevo);

            return CreatedAtAction(nameof(GetMandril),
                new { mandrilId = mandrilNuevo.Id },
                mandrilNuevo);

            //este return lo que devuelve es la ruta a la cual vos podes hacerle un GET del
            //mandril nuevo que acabas de crear
        }

        [HttpPut("{mandrilId}")]
        public ActionResult<Mandril> PutMandril([FromRoute] int mandrilId, [FromBody] MandrilInsert mandrilInsert)
        {
            var mandrilDeseado = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);

            if (mandrilDeseado == null) return NotFound(Mensajes.Mandril.NotFound);

           
            mandrilDeseado.Nombre = mandrilInsert.Nombre;
            mandrilDeseado.Apellido = mandrilInsert.Apellido;

            return NoContent();
            
        }

        [HttpDelete("{mandrilId}")]

        public ActionResult<Mandril> PutMandril(int mandrilId)
        {
            var mandrilDeseado = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);

            if (mandrilDeseado == null) return NotFound(Mensajes.Mandril.NotFound);

            MandrilDataStore.Current.Mandriles.Remove(mandrilDeseado);

            return NoContent();
        }
    }
}
