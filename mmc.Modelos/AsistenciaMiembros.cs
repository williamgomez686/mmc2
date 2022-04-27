using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos
{
    public class AsistenciaMiembros
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Numero de contacto")]
        public int NumeroContacto { get; set; }
        [Required]
        [Display(Name = "Miembros")]
        public string MiembroFamilia { get; set; }
        [Required]
        [Display(Name = "Cantidad de asistencia")]
        public int Cantidad { get; set; }
        [Required]
        [Display(Name = "Servicio")]
        public ServiciosIglesia hora { get; set; }
        //public List<ServiciosIglesia> Servicios { get; set; }
    }
}
