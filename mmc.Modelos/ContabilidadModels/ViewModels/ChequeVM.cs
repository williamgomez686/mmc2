using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ContabilidadModels.ViewModels
{
    public class ChequeVM
    {
        public DateTime FECHA { get; set; }
        public double MONTONUMEROS { get; set; }
        public string MONTOLETRAS { get; set; }
        public string NOMBREDE { get; set; }
        public string NEGOCIABLE { get; set; }
        public string CONCEPTO { get; set; }
        public string NOMBRECUENTA { get; set; }
        public string NOCUENTA { get; set; }
        public string SOLICITADOPOR { get; set; }
        public int NOCHEQUE { get; set; }
        public long POLIZA { get; set; }
        public string CUEAUXCTABANMON { get; set; }
        public string CUEMAYCOD { get; set; }
        public string TIPPOLCOD { get; set; }
        public string TIPAUXCOD { get; set; }
        public string CUEAUXCODAUX { get; set; }
        public List<ChequeDetalle> chequeDetalles { get; set; }

    }
}
