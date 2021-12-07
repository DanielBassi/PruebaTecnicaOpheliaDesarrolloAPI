using Entidades;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaOpheliaDesarrolloAPI.Controllers
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ICliente _cliente;

        public ClienteController( ICliente cliente )
        {
            _cliente = cliente;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> Listar()
        {
            return Ok( _cliente.ObtenerClientes() );
        }

        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> Mostrar( int id )
        {
            return Ok( _cliente.ObtenerCliente( id ) );
        }

        [HttpPost]
        public ActionResult<ClienteDTO> Guardar( ClienteDTO cliente )
        {
            if( ModelState.IsValid )
            {
                return Ok( _cliente.GuardarCliente(cliente) );
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public ActionResult<ClienteDTO> Editar( ClienteDTO cliente)
        {
            if( ModelState.IsValid )
            {
                return Ok(_cliente.EditarCliente( cliente ) );
            }

            return BadRequest( ModelState );
        }

        [HttpDelete("{id}")]
        public ActionResult<ClienteDTO> Eliminar( int id )
        {
            return Ok( _cliente.EliminarCliente( id ) );
        }
    }
}
