using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ContabilidadModels.ViewModels
{
    public class ChequeDetallesVM
    {
        public ChequeVM cabecera { get; set; }
        public ChequeDetalle Detalle { get; set; }
    }
}
