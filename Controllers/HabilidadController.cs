using MandrilAPI.Helpers;
using MandrilAPI.Models;
using MandrilAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MandrilAPI.Controllers
{
    [ApiController]
    [Route("/api/mandril/{mandrilId}/[controller]")]
    public class HabilidadController : ControllerBase
    {
        [HttpGet]

        public ActionResult<IEnumerable<Habilidad>> GetHabilidades(int mandrildId) 
        {
            var mandrilDeseado = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrildId);

            if (mandrilDeseado == null) return NotFound(Mensajes.Mandril.NotFound);

            return Ok(mandrilDeseado.Habilidades);
        }
        

        [HttpGet("{habilidadId}")]

        public ActionResult<Habilidad> GetHabilidad(int mandrildId, int habilidadId)
        {
            var mandrilDeseado = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrildId);

            if (mandrilDeseado == null) return NotFound(Mensajes.Mandril.NotFound);

            var habilidadDeseado = mandrilDeseado.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);

            if (habilidadDeseado == null) return NotFound(Mensajes.Habilidad.NotFound);

            return Ok(habilidadDeseado);

        }

        [HttpPost]

        public ActionResult<Habilidad> PostHabilidad(int mandrilId,HabilidadInsert habilidad) 
        {
            var mandrilDeseado = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);

            if (mandrilDeseado == null) return NotFound(Mensajes.Mandril.NotFound);


            var habilidadExistente = mandrilDeseado.Habilidades?.FirstOrDefault(h => h.Nombre == habilidad.Nombre);

            if (habilidadExistente != null) return BadRequest(Mensajes.Habilidad.NombreExistente);

            var maxIdHabilidad = mandrilDeseado.Habilidades?.Max(h => h.Id);

            var habilidadDeseada = new Habilidad()
            {
                Id = maxIdHabilidad + 1,
                Nombre = habilidad.Nombre,
                Potencia = habilidad.Potencia

            };

            mandrilDeseado.Habilidades?.Add(habilidadDeseada);

            return CreatedAtAction(nameof(GetHabilidad), new {mandrilId =  mandrilId , habilidadId = habilidadDeseada.Id},habilidadDeseada);
        }

        [HttpPut("{habilidadId}")]

        public ActionResult<Habilidad> PutHabilidad(int mandrilId, int habilidadId , HabilidadInsert habilidadInsert)
        {
            var mandrilDeseado = MandrilDataStore.Current.Mandriles.FirstOrDefault(m => m.Id == mandrilId);

            if (mandrilDeseado == null) return NotFound(Mensajes.Mandril.NotFound);


            var habilidadExistente = mandrilDeseado.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);

            if (habilidadExistente != null) return NotFound(Mensajes.Habilidad.NotFound);

            var habilidadMismoNombre = mandrilDeseado.Habilidades?.FirstOrDefault(h => h.Id != habilidadId && h.Nombre == habilidadInsert.Nombre);

            if (habilidadMismoNombre != null) return BadRequest(Mensajes.Habilidad.NombreExistente);

            habilidadExistente.Nombre = habilidadInsert.Nombre;
            habilidadExistente.Potencia = habilidadInsert.Potencia;

            return NoContent();

        }

        [HttpDelete("{habilidadId}")]

        public ActionResult<Habilidad> DeleteHabilidad(int mandrilId,int habilidadId)
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);

            if (mandril == null) return NotFound(Mensajes.Mandril.NotFound);

            var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);

            if (habilidadExistente == null) return NotFound(Mensajes.Habilidad.NotFound);

            mandril.Habilidades?.Remove(habilidadExistente);

            return NoContent();
        
        }
    }
}
