using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ClienteDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es de caracter obligatorio.")]
        [Display(Name = "Nombre del cliente")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La {0} es de caracter obligatorio.")]
        [Display(Name = "Identificación del cliente")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "La {0} es de caracter obligatorio.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El {0} es de caracter obligatorio.")]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El {0} es de caracter obligatorio.")]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaCumpleanos { get; set; }

        [Required(ErrorMessage = "El {0} es de caracter obligatorio.")]
        [Display(Name = "Código postal")]
        public string CodigoPostal { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public DateTime? FechaEliminacion { get; set; }

        public bool Estado { get; set; }

    }
}
