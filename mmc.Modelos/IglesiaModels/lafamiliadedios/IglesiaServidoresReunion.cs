using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.IglesiaModels.lafamiliadedios
{
    public class IglesiaServidoresReunion:CebBase
    {
        [Key]
        public long Id { get; set; }
        [Display(Name ="Acompañantes")]
        public int? Acompañantes { get; set; }
        public bool Asiste { get; set; }
        public int ReunionId { get; set; }
        [ForeignKey("ReunionId")]
        public IglesiaReuniones Reuniones { get; set; }

        public int ServidorId { get; set; }
        [ForeignKey("ServidorId")]
        public IglesiaServidores Servidores { get; set; }
    }
}
