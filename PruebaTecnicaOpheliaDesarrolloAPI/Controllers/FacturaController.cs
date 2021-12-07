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
    public class FacturaController : ControllerBase
    {
        private readonly IFactura _factura;

        public FacturaController(IFactura factura)
        {
            _factura = factura;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FacturaDTO>> Listar()
        {
            return Ok(_factura.ObtenerFacturas() );
        }

        [HttpGet("{id}")]
        public ActionResult<FacturaDTO> Mostrar( int id )
        {
            return Ok( _factura.ObtenerFactura(id) );
        }

        [HttpPost]
        public ActionResult<FacturaDTO> Guardar( FacturaDTO factura )
        {
            return Ok( _factura.GuardarFactura(factura) );
        }

        [HttpPut()]
        public ActionResult<FacturaDTO> Editar( FacturaDTO factura )
        {
            return _factura.EditarFactura(factura);
        }
        
        [HttpDelete]
        public ActionResult <FacturaDTO> Eliminar( int id )
        {
            return Ok( _factura.EliminarFactura( id ) );
        }
    }
}
