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
    public class CEB_DET:CebBase
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Fecha")]
        public DateTime FechaCEB { get; set; }
        [Display(Name = "Cristianos")]
        public int Cristianos { get; set; }
        [Display(Name = "Inconversos")]
        public int NoCistianos { get; set; }
        [Display(Name = "Niños")]
        public int Ninios { get; set; }
        public int Total { get; set; }

        public int Convertidos { get; set; }
        [Display(Name = "Reconciliados")]
        public int Reconcilia { get; set; }

        public double Ofrenda { get; set; }
        public string Foto { get; set; }
        //llaves

        [Display(Name = "Cabecera")]
        public string CEBid { get; set; }
        [ForeignKey("CEBid")]
        public CEB_CAB Cabecera { get; set; }
    }
}
