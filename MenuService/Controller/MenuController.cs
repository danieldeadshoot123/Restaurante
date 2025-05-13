using MenuService.DTOs;
using MenuService.Models;
using MenuService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuService.Controller
{
    [ApiController]
    [Route("api/menu")]

    public class MenuController : ControllerBase
    {
        private readonly CrearMenuService _crearMenuService;
        private readonly MostrarMenuService _mostrarMenuService;

        private readonly ActualizarMenuService _actualizarMenuService;
        private readonly ELiminarMenuService _eLiminarMenuService;


        public MenuController (CrearMenuService crearMenuService, MostrarMenuService  mostrarMenuService, ActualizarMenuService actualizarMenuService, ELiminarMenuService eLiminarMenuService)
        {
            _crearMenuService = crearMenuService;
            _mostrarMenuService = mostrarMenuService;
            _actualizarMenuService = actualizarMenuService;
            _eLiminarMenuService = eLiminarMenuService;
        }


        [HttpPost]

        public async Task<ActionResult<MenuMostrarDTO>> CrearMenu([FromBody]MenuCrearDTO menuCrearDTO)
        {
            try{
                var menu = await _crearMenuService.CrearMenu(menuCrearDTO);
                return CreatedAtAction(nameof(ObtenerMenuPorId), new {Id = menu.id},menu);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuMostrarDTO>>ObtenerMenuPorId(int id)
        {
            var menu = await _mostrarMenuService.GetMenuByIdAsync(id);
            if(menu == null)
                return NotFound();
            return Ok (menu);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuMostrarDTO>>>ObtenerTodosMenu()
        {
            var menu = await _mostrarMenuService.GetAllMenuAsync();
            return Ok (menu);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarMenu(int id, [FromBody] MenuActualizarDTO dto)
        {
            var actualizado = await _actualizarMenuService.ActualizarMenuAsync(id, dto.NombreComida, dto.Precio);

            if (!actualizado)
                return NotFound($"No se encontró el menú con id {id}");

            return Ok("Menú actualizado correctamente");
        }

        [HttpDelete]
        public async Task<ActionResult>EliminarMenu(int id)
        {
            try{
                await _eLiminarMenuService.ELiminarMenuAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound (new {message = ex.Message});
            }
        }






    }
}