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
        public bool Asistencia { get; set; }
        public int Cantidad { get; set; }
    }
}
