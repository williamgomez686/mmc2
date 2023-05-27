using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.IglesiaModels.lafamiliadedios
{
    public class IglesiaServidores:CebBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombres { get; set; }
        public string? Telefono { get; set; }
        public int DepartamentoId { get; set; }
        [ForeignKey("DepartamentoId")]
        public IglesiaDepartamentos Departamentos { get; set; }
    }
}
