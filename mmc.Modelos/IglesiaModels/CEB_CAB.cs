using mmc.Modelos.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mmc.Modelos.IglesiaModels
{
    public class CEB_CAB:CebBase
    {
        [Key]
        public int Id { get; set; }
        public string Hora { get; set; }
        public string dia {get; set;}
        public string Foto { get; set; }

        //llave foranea de Tipo *********************************
        [Display(Name = "Tipo")]
        public int TipoCebId { get; set; }
        [ForeignKey("TipoCebId")]
        public TiposCEB TiposCEB { get; set; }

        //llave Foranea de Regional ******************************
        [Display(Name = "Lider")]
        public int MiembrosCEBid { get; set; }
        [ForeignKey("MiembrosCEBid")]
        public MiembrosCEB MiembroLider { get; set; }

        //Llave Foranea de detalle ceb ***************************
        //[Display(Name = "Detalle")]
        //public int Ceb_Detalle { get; set; }
        //[ForeignKey("Ceb_Detalle")]
        //public List<CEB_DET> Detalle { get; set; }
    }
}
