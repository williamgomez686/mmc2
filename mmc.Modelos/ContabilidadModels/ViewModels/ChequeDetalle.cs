using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ContabilidadModels.ViewModels
{
    public class ChequeDetalle
    {
        public string CONMOVMAYCARABO { get; set; }
        public string CUEMAYCARABO { get; set; }
        public string CUEAUXCODAUX { get; set; }
        public string DESCRIPTION { get; set; }
        public string TIPAUXCARABO { get; set; }
        public double CONMOVMAYVALDET { get; set; }
    }
}
