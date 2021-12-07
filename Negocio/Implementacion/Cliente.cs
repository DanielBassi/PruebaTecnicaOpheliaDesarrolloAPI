using Comun;
using Datos.Contratos;
using Entidades;
using Negocio.Contratos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Comun.Utilidades;

namespace Negocio.Implementacion
{
    public class Cliente : ICliente
    {
        private readonly IAcceso _acceso;

        private Utilidades _utilidades;

        public Cliente( IAcceso acceso )
        {
            _acceso = acceso;
            _utilidades = new Utilidades();
        }

        public List<ClienteDTO> ObtenerClientes(string[,] parametros = null)
        {
            List<ClienteDTO> clientes = new List<ClienteDTO>();
            DataTable tblResultado = (DataTable)_acceso.DevolverDatos(Query.OBTENER_CLIENTES, parametros, Tipo.TABLA);

            if (tblResultado != null)
            {
                foreach (DataRow item in tblResultado.Rows)
                {
                    clientes.Add(
                        new ClienteDTO
                        {
                            Id = (int)item["id"],
                            Nombre = (string)item["nombre"].ToString(),
                            Identificacion = (string)item["identificacion"].ToString(),
                            Direccion = (string)item["direccion"].ToString(),
                            Telefono = (string)item["telefono"].ToString(),
                            Correo = (string)item["correo"].ToString(),
                            CodigoPostal = (string)item["codigoPostal"].ToString(),
                            FechaCreacion = (DateTime)item["fechaCreacion"],
                            Estado = (bool)item["estado"]
                        }
                    );
                }
            }

            return clientes;
        }
        
        public ClienteDTO ObtenerCliente( int id )
        {
            string[,] parametros = new string[1, 2];

            parametros[0, 0] = "@PId";
            parametros[0, 1] = id.ToString();

            ClienteDTO cliente = new ClienteDTO();

            DataRow tblResultado = (DataRow)_acceso.DevolverDatos(Query.OBTENER_CLIENTE, parametros, Tipo.REGISTRO);

            if( tblResultado != null )
            {
                cliente.Id = (int)tblResultado["id"];
                cliente.Nombre = (string)tblResultado["nombre"].ToString();
                cliente.Identificacion = (string)tblResultado["identificacion"].ToString();
                cliente.Direccion = (string)tblResultado["direccion"].ToString();
                cliente.Correo = (string)tblResultado["correo"].ToString();
                cliente.CodigoPostal = (string)tblResultado["codigoPostal"].ToString();
                cliente.FechaCreacion = (DateTime)tblResultado["fechaCreacion"];

                cliente.FechaModificacion = (tblResultado["fechaModificacion"] is not DBNull) ? (DateTime)tblResultado["fechaModificacion"] : null;
                cliente.FechaEliminacion = (tblResultado["fechaEliminacion"] is not DBNull) ? (DateTime)tblResultado["fechaEliminacion"] : null;

                cliente.Estado = (bool)tblResultado["estado"];
            }

            return cliente;
        }

        public ClienteDTO GuardarCliente( ClienteDTO cliente )
        {
            string[,] parametros = new string[6, 2];

            parametros[0, 0] = "@PNombre";
            parametros[0, 1] = cliente.Nombre.ToString();
            parametros[1, 0] = "@PIdentificacion";
            parametros[1, 1] = cliente.Identificacion.ToString();
            parametros[2, 0] = "@PTelefono";
            parametros[2, 1] = cliente.Telefono.ToString();
            parametros[3, 0] = "@PCorreo";
            parametros[3, 1] = cliente.Correo.ToString();
            parametros[4, 0] = "@PCodigoPostal";
            parametros[4, 1] = cliente.CodigoPostal.ToString();
            parametros[5, 0] = "@PDireccion";
            parametros[5, 1] = cliente.Direccion.ToString();

            DataRow registro = (DataRow)_acceso.EjecutarConTransaccion(Query.GUARDAR_CLIENTE, parametros);

            cliente.Id = (int)registro["id"];
            cliente.Nombre = (string)registro["nombre"].ToString();
            cliente.FechaCreacion = (DateTime)registro["fechaCreacion"];
            
            return cliente;
        }

        public ClienteDTO EditarCliente( ClienteDTO cliente )
        {
            string[,] parametros = new string[7, 2];
            parametros[0, 0] = "@PNombre";
            parametros[0, 1] = cliente.Nombre.ToString();
            parametros[1, 0] = "@PIdentificacion";
            parametros[1, 1] = cliente.Identificacion.ToString();
            parametros[2, 0] = "@PTelefono";
            parametros[2, 1] = cliente.Telefono.ToString();
            parametros[3, 0] = "@PCorreo";
            parametros[3, 1] = cliente.Correo.ToString();
            parametros[4, 0] = "@CodigoPostal";
            parametros[4, 1] = cliente.CodigoPostal.ToString();
            parametros[5, 0] = "@PId";
            parametros[5, 1] = cliente.Id.ToString();
            parametros[6, 0] = "@PDireccion";
            parametros[6, 1] = cliente.Direccion.ToString();

            DataRow registro = (DataRow)_acceso.EjecutarConTransaccion(Query.EDITAR_CLIENTE, parametros);
            cliente.FechaModificacion = (DateTime)registro["fechaModificacion"];


            return cliente;
        }

        public ClienteDTO EliminarCliente( int id )
        {
            ClienteDTO cliente = new ClienteDTO();

            string[,] parametros = new string[1, 2];

            parametros[0, 0] = "@PId";
            parametros[0, 1] = id.ToString();

            DataRow registro = (DataRow)_acceso.EjecutarConTransaccion(Query.ELIMINAR_CLIENTE, parametros);

            cliente.Id = (int)registro["id"];
            cliente.Nombre = (string)registro["nombre"].ToString();
            cliente.FechaEliminacion = (DateTime)registro["fechaEliminacion"];

            return cliente;

        }
    }
}
