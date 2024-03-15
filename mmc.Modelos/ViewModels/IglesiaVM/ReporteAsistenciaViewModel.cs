using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ViewModels.IglesiaVM
{
    public class ReporteAsistenciaViewModel
    {
        public string NombreReunion { get; set; }
        public int Asistencia { get; set; }
        public int noasiste { get; set; }
        public int Acompañantes { get; set; }
        public int Total { get; set; }
    }
}
