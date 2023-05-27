using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels.IglesiaVM
{
    public class VMReuniones
    {
        public int ServidorId { get; set; }
        public string Nombres { get; set; }
        public int? Acompañantes { get; set; }
        public bool Asiste { get; set; }
        public string Departamento { get; set; }
        public string NombreReunion { get; set; }
    }
}
