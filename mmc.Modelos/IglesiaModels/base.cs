using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.IglesiaModels
{
    public class CebBase
    {
        public string Usuario { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime Fechamodifica { get; set; }
        public bool Estado { get; set; }
    }
}
