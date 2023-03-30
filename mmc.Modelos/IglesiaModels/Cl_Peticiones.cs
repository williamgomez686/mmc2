using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mmc.Modelos.IglesiaModels
{
    public class Cl_Peticiones
    {
        [Key]
        public int Id { get; set; }
        [MinLength(8)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Debe ingresar el Nombre")]
        [Display(Name = "Nombre")]
        public string Nombres { get; set; }
        [Display(Name = "Código")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Debe ingresar su Petición")]
        [Display(Name = "Petición")]
        public string Peticion { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }
    }
}
