using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.Modelos.ContabilidadModels.ViewModels
{
    public class ActivoFijoUpdateVW
    {
        [Display(Name = "Id de Activo Fijo")]
        public string IDCONTA { get; set; }
        [Display(Name = "Costo del activo")]
        public double PRECIO { get; set; }
        [Display(Name = "Marca")]
        public string MARCA { get; set; }
        [Display(Name = "Modelo")]
        public string MODELO { get; set; }
        [Display(Name = "Serie")]
        public string SERIE { get; set; }
        [Display(Name = "Descripción del activo fijo")]
        public string DESCRIPCION { get; set; }
        [Display(Name = "Proyecto")]
        public string PROYECTO { get; set; }
        [Display(Name = "Tipo de Activo")]
        public string TIPOACTIVO { get; set; }
        [Display(Name = "Sub Tipo de activo")]
        public string SUBTIPOACTIVO { get; set; }
        [Display(Name = "Nombre Empleado")]
        public string EMPLEADO { get; set; }
        [Display(Name = "Apellido Empleado")]
        public string APELLIDO { get; set; }
        [Display(Name = "Proveedor")]
        public string PROVEEDOR { get; set; }
        [Display(Name = "Numero de Factura")]
        public string NOFACTURA { get; set; }
        [Display(Name = "Observaciones")]
        public string ACFIOBS { get; set; }
        [Display(Name = "Foto")]
        public string ACFIFOTO { get; set; }
    }
}
