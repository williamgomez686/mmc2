using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mmc.Modelos
{
    public class BaseModel
    {
        [Display(Name = "Usuario de Alta")]
        public string UsuarioAlta { get; set; }
        [Display(Name = "Usuario que Modifico")]
        public string UsuarioModifica { get; set; }
        [Display(Name = "Fecha de Alta")]
        public DateTime? FechaAlta { get; set; }
        [Display(Name = "Fecha Modifica")]
        public DateTime? Fechamodifica { get; set; }
        public bool Estado { get; set; }
    }
}
