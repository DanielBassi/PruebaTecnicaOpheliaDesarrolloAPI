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
    public class Producto : IProducto
    {
        private readonly IAcceso _acceso;

        public Producto(IAcceso acceso)
        {
            _acceso = acceso;
        }

        public List<ProductoDTO> ObtenerProductos(string[,] parametros = null)
        {
            List<ProductoDTO> productos = new List<ProductoDTO>();

            DataTable tblResultado = (DataTable)_acceso.DevolverDatos(Query.OBTENER_PRODUCTOS, parametros, Tipo.TABLA);

            if (tblResultado != null)
            {
                foreach (DataRow item in tblResultado.Rows)
                {
                    productos.Add(
                        new ProductoDTO
                        {
                            Id = (int)item["id"],
                            Nombre = (string)item["nombre"].ToString(),
                            Descripcion = (string)item["descripcion"].ToString(),
                            CantidadMinima = (int)item["cantidadMinima"],
                            CantidadMaxima = (int)item["cantidadMaxima"],
                            FechaCreacion = (DateTime)item["fechaCreacion"],
                            Estado = (bool)item["estado"]
                        }
                    );
                }
            }

            return productos;

        }
    }
}
