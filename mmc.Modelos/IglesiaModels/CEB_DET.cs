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
        public int Id { get; set; }
        public int Cistianos { get; set; }
        public int NoCistianos { get; set; }
        public int Ninios { get; set; }
        public int Total { get; set; }
        public int Convertidos { get; set; }
        public int Reconcilia { get; set; }
        public float Ofrenda { get; set; }
        public string Foto { get; set; }
        //llaves

        [Display(Name = "Cabecera")]
        public int CEBid { get; set; }
        [ForeignKey("CEBid")]
        public CEB_CAB Cabecera { get; set; }
    }
}
