using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos
{
    class Tickets
    {
        [Key]
        [Required]
        [MaxLength(50)]
        [Display(Name = "PK")]
        public string Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Asunto")]
        public string Asunto { get; set; }
        [Required]
        [MaxLength(2000)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        [MaxLength(400)]
        [Display(Name = "Solucion")]
        public string Solucion { get; set; }
        public DateTime? FechaSolucion { get; set; }
        [MaxLength(50)]
        [Display(Name = "Tecnico")]
        public string Tecnico { get; set; }

        public string ImagenUrl { get; set; }

        //foreing keys
        [Required]
        public int EstadoId { get; set; }
        [ForeignKey("EstadoId")]
        public Estados Estado { get; set; }
    }
}
