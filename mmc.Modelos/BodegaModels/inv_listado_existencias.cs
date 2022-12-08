using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.BodegaModels
{
    public class inv_listado_existencias
    {
        public string CODIGO { get; set; }
        public string UNIDADES { get; set; }
        public string DESCRIPCION { get; set; }
        public double EXISTENCIAS { get; set; }
        public int RESERVA { get; set; }
        public double DISPONIBLE { get; set; }
        public string BODEGA { get; set; }
    }
}
