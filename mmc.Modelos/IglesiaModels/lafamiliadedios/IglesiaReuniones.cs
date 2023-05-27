using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.IglesiaModels.lafamiliadedios
{
    public class IglesiaReuniones :CebBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre de la reunión")]
        public string NombreReunion { get; set; }
        [Display(Name = "Fecha de la reunión")]
        [Required]
        public DateTime ReunionFecha { get; set; }
    }
}
