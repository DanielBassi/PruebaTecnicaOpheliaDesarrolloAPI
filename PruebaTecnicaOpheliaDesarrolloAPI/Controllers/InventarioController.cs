using Entidades.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaOpheliaDesarrolloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IInventario _inventario;

        public InventarioController(IInventario inventario)
        {
            _inventario = inventario;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InventarioDTO>> Listar()
        {
            return Ok( _inventario.ObtenerInvenatrios() );
        }
    }
}
