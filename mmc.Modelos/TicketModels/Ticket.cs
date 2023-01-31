using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mmc.Modelos.TicketModels
{
    public class Ticket:BaseModel
    {
        [Key][Required][MaxLength(50)][Display(Name = "Codigo")]
        public string Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Asunto")]
        public string Asunto { get; set; }
        [Required]
        //[MaxLength(2000)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [MaxLength(400)]
        [Display(Name = "Solucion")]
        public string Solucion { get; set; }
        [Display(Name = "Fecha de Solucion")]
        public DateTime? FechaSolucion { get; set; }
        [MaxLength(50)][Display(Name = "Tecnico")]
        public string Tecnico { get; set; }
        [Display(Name = "Imagen")]
        public string ImagenUrl { get; set; }

        //foreing keys
        [Required][Display(Name = "Estado del Ticket")]
        public int EstadoTKId { get; set; }
        [ForeignKey("EstadoTKId")]
        public EstadosTK EstadosTK { get; set; }
        //Nivel de Urgencia
        [Required]

        [Display(Name = "Urgencia")]
        public int UrgenciaId { get; set; }
        [ForeignKey("UrgenciaId")]
        public UrgenciaTK Urgencia { get; set; }

        //Area a la que pertenece el caso
        [Required][Display(Name = "Area")]
        public int AreaSoporteId { get; set; }
        [ForeignKey("AreaSoporteId")]
        public AreaSoporteTK AreaSoporte { get; set; }

        //Sede a la que pertenece el caso
        [Required][Display(Name = "Sede")]
        public int SedeId { get; set; }
        [ForeignKey("SedeId")]
        public EmpresaTK SedeTK { get; set; }

    }
}
