using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.IglesiaModels.lafamiliadedios
{
    public class IglesiaDepartamentos: CebBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Nombre Departamento")]
        public string Descripcion { get; set; }
    }
}
