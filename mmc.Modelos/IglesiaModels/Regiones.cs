using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos
{
    public class Regiones
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Region")]
        public string Region { get; set; }
        [MaxLength(70)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [Display(Name = "Fecha de Alta")]
        public DateTime FechaAlta { get; set; }
        [Display(Name = "Usuario de Alta")]
        public string   UsuarioAlta { get; set; }
        public bool Estado { get; set; }
        
    }
}
