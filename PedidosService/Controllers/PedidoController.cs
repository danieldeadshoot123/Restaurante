using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PedidoDB.Data;
using PedidosService.DTOs;
using PedidosService.Models;
using PedidosService.Services;

namespace PedidosService.Controllers
{
    [ApiController]
    [Route("api/pedidos")]

    public class PedidoController : ControllerBase
    {
        private readonly ActualizarPedidoService _actualizarPedidoService;
        private readonly CreatePedidoService _createPedidoService;
        private readonly EliminarPedidoService _eliminarPedidoService;
        private readonly MostrarPedidoService _mostrarPedidoService ;


        public PedidoController (CreatePedidoService createPedidoService, MostrarPedidoService mostrarPedidoService, ActualizarPedidoService actualizarPedidoService, EliminarPedidoService eliminarPedidoService )
        {
            _createPedidoService = createPedidoService;
            _actualizarPedidoService = actualizarPedidoService;
            _mostrarPedidoService = mostrarPedidoService;
            _eliminarPedidoService = eliminarPedidoService;
        }

       

        [HttpPost]
        public async Task<ActionResult<PedidoReadDTO>> CrearPedido([FromBody] PedidoCreateDTO pedidoCreateDto)
    {
        try
        {
            var pedido = await _createPedidoService.CrearPedido(pedidoCreateDto);
            return CreatedAtAction(nameof(ObtenerPedidoPorId), new { id = pedido.Id }, pedido);
        }
        catch (Exception ex)
        {
        return BadRequest(ex.Message);
        }
    }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoReadDTO>>> ObtenerTodosLosPedidos()
        {
            var pedidos = await _mostrarPedidoService.GetAllPedidosAsync();
            return Ok(pedidos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoReadDTO>>ObtenerPedidoPorId(int id)
        {
            var pedido = await  _mostrarPedidoService.GetPedidoByIdAsync(id);
            if(pedido == null )
                return NotFound();
            return Ok (pedido);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult>ActualizarEstado(int id, [FromBody] string nuevoEstado)
        {
            try{
                await _actualizarPedidoService.ActualizarEstatusPedidoAsync(id,nuevoEstado);
                return NoContent();


            }

            catch (Exception ex)
            {
                return NotFound (new{ message = ex.Message});
            }
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult>EliminarPedido (int id)

        {
            try
            {
                await _eliminarPedidoService.EliminarPedidoAsync(id);
                return NoContent();

            }

            catch(Exception ex)
            {
                return NotFound ( new { message = ex.Message});
            }
        }

        

    }
}