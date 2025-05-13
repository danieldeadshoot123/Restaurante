using System.Security.Cryptography.X509Certificates;
using Avro;
using Energistics.Datatypes;
using MesasService.DTOs;
using MesasService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesasService.Controllers
{
    [ApiController]
    [Route("api/mesas")]


    public class MesaController : ControllerBase
    {
        private readonly CrearMesaService _crearMesaService;
        private readonly MostrarMesaServices _mostrarMesaServices;
        private readonly ActualizarMesaService _actualizarMesaService;
        private readonly EliminarMesaServices _eliminarMesaServices;


        public MesaController(CrearMesaService crearMesaService, MostrarMesaServices mostrarMesaServices, ActualizarMesaService actualizarMesaService, EliminarMesaServices eliminarMesaServices)
        {
            _crearMesaService = crearMesaService;
            _actualizarMesaService = actualizarMesaService;
            _mostrarMesaServices = mostrarMesaServices;
            _eliminarMesaServices = eliminarMesaServices;
        } 

        [HttpPost]
        public async Task<ActionResult<MesaMostrarDTO>> CrearMesa([FromBody] MesaCrearDTO mesaCrearDTO)
        {
            try{
                var mesa = await _crearMesaService.CrearMesa(mesaCrearDTO);
                return CreatedAtAction(nameof(ObtenerMesaPorId), new {id = mesa.Id},mesa);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<MesaMostrarDTO>>ObtenerMesaPorId(int id)
        {
            var mesa = await _mostrarMesaServices.GetMesaByIdAsync(id);
            if (mesa == null )
                return NotFound();
            return Ok (mesa);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MesaMostrarDTO>>>ObtenerTodasLasMesas()
        {
            var mesa = await _mostrarMesaServices.GetAllMesaAsync();
            return Ok(mesa);
        }


        [HttpPatch("{id}")]
        
            public async Task<ActionResult>ActualizarEstado(int id, [FromBody] string nuevoEstado)
            {
                try
                {
                    await _actualizarMesaService.ActualizarEstadoMesaAsync(id, nuevoEstado);
                    return NoContent();                   
                }
                catch (Exception ex )
                {
                    
                    return NotFound (new {message = ex.Message});
                }
            }
        

        [HttpDelete]
        public async Task<ActionResult>EliminarMesa(int id)
        {
            try
            {
                await _eliminarMesaServices.EliminarMesaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                
                return NotFound (new {message = ex.Message});
            }
        }
    }
}