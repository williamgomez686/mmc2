using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ContabilidadModels
{
    public class DiarioXMotivoVM
    {
        public string EMPCOD { get; set; }
        public int DOCUMENTO { get; set; }
        public string TIPO { get; set; }
        public DateTime FECHA { get; set; }
        public int TOTAL { get; set; }
        public long LIQUIDACION { get; set; }
        public string NOMBRE { get; set; }
        public string CLIENTE { get; set; }
        public long PROMESA { get; set; }
        public string MOTIVO_DESC { get; set; }
    }
}
