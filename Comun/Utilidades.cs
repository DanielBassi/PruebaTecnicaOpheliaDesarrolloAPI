using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public class Utilidades
    {
        public enum Tipo 
        { 
            REGISTRO = 1,
            TABLA = 2,
            TABLAS = 3
        };


        public object convertDataROwToObject(DataRow dataRow)
        {
            object obj = new object();

            try
            {
                string tblJson = JsonConvert.SerializeObject(dataRow);

                obj = JsonConvert.DeserializeObject<object>(tblJson);
            }
            catch (Exception ex)
            {
                string exStr = ex.Message;
            }

            return obj;
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            
            // Obtener todas las propiedades
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            foreach (PropertyInfo prop in Props)
            {
                //Establecer nombres de columna como nombres de propiedad
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //Insertar valores de propiedad en filas de la tabla de datos
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
