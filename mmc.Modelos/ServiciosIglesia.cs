using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos
{
    public class ServiciosIglesia
    {
        [Key]
        public string id { get; set; }
        [Required]
        [Display(Name = "Servicios")]
        public string Servicio { get; set; }
        [Required]
        [Display(Name = "Horario")]
        public int Horario {get; set;}
    }
}
