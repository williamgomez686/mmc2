using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos
{
    public class Estados
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [MaxLength(20)]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        [Display(Name = "Fecha")]
        public DateTime FechaAlta { get; set; }
        [MaxLength(20)]
        [Display(Name = "Usuario de Alta")]
        public string UsuarioAlta { get; set; }
    }
}
