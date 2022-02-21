using equiposWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace equiposWebApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly prestamosContext _contexto;

        public equiposController(prestamosContext miContexto) 
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/equipos")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<equipos> equiposList = from e in _contexto.equipos
                                                   select e;
                if (equiposList.Count() > 0)
                {
                    return Ok(equiposList);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/equipos/{idEquipo}")]
        public IActionResult Get(int idEquipo)
        {
            try
            {
                equipos equipo = (from e in _contexto.equipos
                                  where e.id_equipos == idEquipo
                                  select e
                                ).FirstOrDefault();

                if (equipo != null)
                {
                    return Ok(equipo);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/equipos")]
        public IActionResult guardarEquipo([FromBody] equipos equipoNuevo)
        {
            try
            {
                _contexto.equipos.Add(equipoNuevo);
                _contexto.SaveChanges();
                return Ok(equipoNuevo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/equipos")]
        public IActionResult updateEquipo([FromBody] equipos equipoAModificar)
        {
            try
            {
                equipos equipoExiste = (from e in _contexto.equipos
                                        where e.id_equipos == equipoAModificar.id_equipos
                                        select e).FirstOrDefault();

                if (equipoExiste is null)
                {
                    return NotFound();
                }

                equipoExiste.nombre = equipoAModificar.nombre;
                equipoExiste.descripcion = equipoAModificar.descripcion;

                _contexto.Entry(equipoExiste).State = EntityState.Modified;
                _contexto.SaveChanges();

                return Ok(equipoExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
