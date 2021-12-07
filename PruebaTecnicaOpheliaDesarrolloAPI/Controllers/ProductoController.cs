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
    public class ProductoController : ControllerBase
    {
        private readonly IProducto _producto;

        public ProductoController( IProducto producto )
        {
            _producto = producto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductoDTO>> Listar()
        {
            return Ok( _producto.ObtenerProductos() );
        }
    }
}
