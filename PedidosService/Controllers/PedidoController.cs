using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PedidoDB.Data;
using PedidosService.DTOs;
using PedidosService.Models;
using PedidosService.Repository;
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

        private readonly IPedidoRepository _pedidoRepository;
        private readonly MesasService   _mesasService;


        public PedidoController(CreatePedidoService createPedidoService, MostrarPedidoService mostrarPedidoService, ActualizarPedidoService actualizarPedidoService, EliminarPedidoService eliminarPedidoService, IPedidoRepository pedidoRepository, MesasService MesasService)
        {
            _createPedidoService = createPedidoService;
            _actualizarPedidoService = actualizarPedidoService;
            _mostrarPedidoService = mostrarPedidoService;
            _eliminarPedidoService = eliminarPedidoService;
            _pedidoRepository = pedidoRepository;
            _mesasService = MesasService;
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


        [HttpPut("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null) return NotFound("Pedido no encontrado");

            pedido.Estatus = nuevoEstado;
            await _pedidoRepository.SaveChangesAsync();

            // Si el estado es "entregado", liberar la mesa
            if (nuevoEstado.ToLower() == "entregado")
            {
                var liberarResult = await _mesasService.LiberarMesaAsync(pedido.MesaId);
                if (!liberarResult)
                {
                    return StatusCode(500, "Error al liberar la mesa");
                }
            }

            return NoContent();
        }

        

    }
}