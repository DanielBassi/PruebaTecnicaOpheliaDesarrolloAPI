using Comun;
using Datos.Contratos;
using Entidades.DTO;
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
    public class Inventario : IInventario
    {
        private readonly IAcceso _acceso;

        public Inventario( IAcceso acceso )
        {
            _acceso = acceso;
        }

        public List<InventarioDTO> ObtenerInvenatrios(string[,] parametros = null)
        {
            List<InventarioDTO> inventarios = new List<InventarioDTO>();
            DataTable tblResultado = (DataTable)_acceso.DevolverDatos(Query.OBTENER_INVENTARIO, parametros, Tipo.TABLA);

            if (tblResultado != null)
            {
                foreach (DataRow item in tblResultado.Rows)
                {
                    inventarios.Add(
                        new InventarioDTO
                        {
                            Id = (int)item["id"],
                            IdProducto = (int)item["idProducto"],
                            PrecioCompra = (decimal)item["precioCompra"],
                            PrecioVenta = (decimal)item["precioVenta"],
                            Cantidad = (int)item["cantidad"],
                            FechaCreacion = (DateTime)item["fechaCreacion"],
                            Estado = (bool)item["estado"]
                        }
                    );
                }
            }

            return inventarios;
        }
    }
}
