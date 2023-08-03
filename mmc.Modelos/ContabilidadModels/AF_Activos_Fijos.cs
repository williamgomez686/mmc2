using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ContabilidadModels
{
    public class AF_Activos_Fijos:mmc
    {
        [Required(ErrorMessage ="Este campo es Obligatorio")]
        [Display(Name = "Código Activo Fijo")]
        public string CODIGOACTIVO { get; set; }
        public string ACFIDSC { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public string ACFIFOTO { get; set; }
        public string ACFITIPCOD { get; set; }
        public string ACFISUBTIPCOD { get; set; }
        public DateTime ACFIFCHCMP { get; set; }
        public double ACFIMONLOC { get; set; }
        public string ACFINUMCHQ { get; set; }
        public string CONDEPCOD { get; set; }
        public string CODIGOEMPLEADO { get; set; }
        public string ACFIUBICOD { get; set; }
        public string ACFIOBS { get; set; }
        public DateTime ACFIFCHBAJ { get; set; }
        public string NOFACTURA { get; set; }
        public string NITPROVEEDOR { get; set; }
        public string ACFIDOCREF { get; set; }
        public string EstadoDescripcion { get; set; }
        public string EstadoCodigo { get; set; }
    }
}
