using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmc.Modelos.ContabilidadModels;

namespace mmc.Modelos.OracleModels.RRHH
{
    public class RHEMPLEADOSRELACIONLAB:mmc
    {
        public string EMPLEMPCOD { get; set; }
        public string EMPLEMPNOM { get; set; }
        public string EMPLEMPAPE { get; set; }
        public string EMPLEMPSEXO { get; set; }
        public DateTime EMPLEMPFCHANAC { get; set; }
        public string EMPLEMPDIRECCION { get; set; }
        public string EMPLEMPTELRESI { get; set; }
        public string EMPLEMPTELMOVIL { get; set; }
        public long DPI { get; set; }
        public string EMPLEMPNIT { get; set; }
        public string EMPLEMPFOTO { get; set; }
    }
}
